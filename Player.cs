using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum CharacterState {
        Idle, Walking, Running, Crouching, isKnockedOut
    }
    public CharacterState characterState = CharacterState.Idle;
    public float walkSpeed = 2;
    public float runSpeed = 5;
    public float lookSpeed = 3;
    public Camera playerCamera;
    private Vector2 rotation = Vector2.zero;
    CharacterController controller;
    float speed;
    public Animator animator;
    public Animator cameraAnimator;

    [Header("Run Animation")]
    public Vector3 runOffset;
    public float runFOV = 60f;
    public KeyCode runKey = KeyCode.LeftShift;
    float runLerp;

    [Header("Crouch Animation")]
    public Vector3 crouchOffset;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Raycasts")]
    public Transform raycastPos;

    bool isDisableCameraLook;
    bool isDisableMovement;
    Vector3 initCamPosition;
    float initialFOV;
    CharacterState lastCharacterState;
    int animatorVelocityX;
    int animatorVelocityZ;

    public Transform attackPos;

    [Header("Footsteps")]
    int footstepHash;
    float footstepValue;
    float footstepTimer;
    private FootstepCollection currentFootstep;

    float delayTimer = 0.5f;
    float currentDelayTimer = 0;

    Audio myAudio;

    bool isRunEnabled = true;

    void Awake() {
        GameManager.instance.player = this;
        myAudio = GetComponent<Audio>();
        controller = GetComponent<CharacterController>();

        // set init rotations
        rotation.x = playerCamera.transform.localRotation.x;
        rotation.y = transform.eulerAngles.y;

        
        SlotData slotData = Methods.GetCurrentSlot();

        // if slot is not empty, then set the player to position
        if (!slotData.isEmpty) {           
            transform.position = new Vector3(slotData.savePosition[0], 
            slotData.savePosition[1], slotData.savePosition[2]);
        } else {
            Debug.Log("Slot data is empty");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        initCamPosition = playerCamera.transform.localPosition;
        initialFOV = playerCamera.fieldOfView;

        // hash animators to avoid lookup everyframe
        animatorVelocityX = Animator.StringToHash("VelocityX");
        animatorVelocityZ = Animator.StringToHash("VelocityZ");
        footstepHash = Animator.StringToHash("FootStep");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDelayTimer < delayTimer) {
            currentDelayTimer += Time.deltaTime;
            return;
        }
        if (characterState == CharacterState.isKnockedOut) return;
        if (!isDisableMovement)
        {
            CharacterControl();
        }
    }

    void LateUpdate(){
        if (characterState == CharacterState.isKnockedOut) return;
        MouseLook();
    }

    private void MouseLook()
    {
        if (isDisableCameraLook) return;
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -45f, 45f);
        transform.eulerAngles = new Vector2(0, rotation.y);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }

    private void CharacterControl(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Detect Crouch First
        if (Input.GetKey(crouchKey)) {
            characterState = CharacterState.Crouching;
        } else if (x != 0 || z != 0) {
            characterState = CharacterState.Walking;
            // Detect Run
            if (Input.GetKey(runKey) && isRunEnabled) {
                characterState = CharacterState.Running;
            }
        } else {
            characterState = CharacterState.Idle;
        }
        UpdateMovement(x, z);
        lastCharacterState = characterState;
    }

    private void UpdateMovement(float xMove, float zMove)
    {
        Vector3 movement = Vector3.zero;
        switch (characterState) {
            case CharacterState.Walking:
                animator.SetFloat(animatorVelocityX, xMove);
                animator.SetFloat(animatorVelocityZ, zMove);
                movement = Vector3.ClampMagnitude(transform.forward * zMove + transform.right * xMove, 1) * walkSpeed;
                break;
            case CharacterState.Running:
                runLerp = Mathf.Lerp(runLerp, 2, 0.05f);
                animator.SetFloat(animatorVelocityX, xMove * runLerp);
                animator.SetFloat(animatorVelocityZ, zMove * runLerp);
                movement = Vector3.ClampMagnitude(transform.forward * zMove + transform.right * xMove, 1) * runSpeed;
                break;
        }

        // ONE TIME CALL EFFECTS
        if (lastCharacterState != characterState) {
            // FOV EFFECT WHEN STATE CHANGED
            StartCoroutine(FOVEffect(characterState));
            HandleAnimations(characterState, lastCharacterState); // Handle animations once
        }

        // Add gravity
        if (!controller.isGrounded) {
            movement += Physics.gravity;
        }

        // Update based on inputs
        controller.Move(movement * Time.deltaTime);
        speed = controller.velocity.magnitude;

        float newFootstepValue =  animator.GetFloat(footstepHash);

        //Update Footstep
        if (isFootStepReady(newFootstepValue)) {
            footstepValue = newFootstepValue;
            PlayFootstep();
        }
    }

    private bool isFootStepReady(float newFootstepValue)
    {
        return !((footstepValue < 0) == (newFootstepValue < 0)) && speed > 0.2f;
    }


    private void HandleAnimations(CharacterState state, CharacterState lastState)
    {
        // Enable animations
        switch (characterState) {
            case CharacterState.Crouching:
                animator.SetBool("isCrouching", true);
                break;
            case CharacterState.Idle:
                animator.SetFloat(animatorVelocityX, 0);
                animator.SetFloat(animatorVelocityZ, 0);
                break;
        }
        // Disable Animations
        // Disable once after state changed, such as animator bools
        switch (lastState) {
            case CharacterState.Crouching:
                animator.SetBool("isCrouching", false);
                break;
        }

        // Lerps
        runLerp = 0;
    }

    private IEnumerator FOVEffect(CharacterState state)
    {
        float timePassed = 0;
        while (state == characterState) {
            Vector3 offset = initCamPosition;
            float FOV = initialFOV;
            switch (characterState) {
                case CharacterState.Running:
                    offset = initCamPosition + runOffset;
                    FOV = runFOV;
                    break;
                case CharacterState.Crouching:
                    offset = initCamPosition + crouchOffset;
                    break;
            }

            // Moves the camera in desired offset
            playerCamera.transform.localPosition = 
                Vector3.Lerp(playerCamera.transform.localPosition, offset, 0.05f);

            // Sets the FOV, eg.run to Running FOV if running, and normal FOV if walking
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, FOV, 0.05f);
            yield return null;
            // After 1 second, stop executing since all of the effects might
            // already been applied
            timePassed += Time.deltaTime;
            if (timePassed > 1f) yield break;

        }
    }

    public void KnockOut(Transform knockOrigin, MonoBehaviour componentToDisable) {
        myAudio.Stop();
        // change character state
        characterState = CharacterState.isKnockedOut;
        
        // Death pose & disable flashlight
        animator.SetFloat(animatorVelocityX, 0);
        animator.SetFloat(animatorVelocityZ, 0);
        animator.SetBool("isCrouching", false);
        playerCamera.transform.localPosition = Vector3.zero;
        
        // Disable objects to restrict control
        GetComponent<FlashLightController>().DisableFlashlight();
        controller.enabled = false;
        StartCoroutine(AnimateKnock(knockOrigin, componentToDisable));
    }

    // THIS IS WHERE ALL IT GETS MESSY, BUT WILL FIX LATER IF POSSIBLE
    // STILL THINKING IF I WOULD CREATE A SEPARATE SCRIPT FOR THESE ANIMATIONS
    // FOR SPECIFIC ENEMIES
    private IEnumerator AnimateKnock(Transform knockOrigin, MonoBehaviour componentToDisable)
    {
        float timer = 0;

        // Activate Timeline
        GetComponent<TimelineActivator>().PlayTimeline(0);

        // Face knocking enemy
        Vector3 rotationBody = transform.eulerAngles;
        Vector3 directionBody = (knockOrigin.position - transform.transform.position).normalized;
        rotationBody.y = Quaternion.LookRotation(directionBody).eulerAngles.y;
        componentToDisable.enabled = false;
       
        // Set camera rotation to zero 
        Quaternion targetCameraRot = Quaternion.identity * Quaternion.Euler(-40f, 0, 0);
        while(timer < 0.5f) {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotationBody, 0.1f);
            playerCamera.transform.localRotation = Quaternion.Slerp(
                playerCamera.transform.localRotation, targetCameraRot, 0.1f);
            timer += Time.deltaTime;
            yield return null;
        }

        // Enable Physics
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        StartCoroutine(ForceFaceUp(knockOrigin));


        knockOrigin.parent = this.transform;

        // Set camera rotation to zero 
        timer = 0;
        // Vector3 initKnockPos = knockOrigin.localPosition;
        while(timer < 3f) {
            knockOrigin.localPosition = Vector3.Lerp(knockOrigin.localPosition, 
                attackPos.localPosition, 0.1f);
            knockOrigin.localRotation = Quaternion.Slerp(knockOrigin.localRotation, 
            attackPos.localRotation, 0.1f);
            playerCamera.transform.localRotation = Quaternion.Slerp(
                playerCamera.transform.localRotation, Quaternion.identity, 0.2f);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void DisableRunning(bool isDisable) {
        isRunEnabled = !isDisable;
    }

    private IEnumerator ForceFaceUp(Transform knockOrigin)
    {
        float sensitivity = 50f;
        float timer = 0;
        Vector3 initRotation = transform.eulerAngles;
        while(true) {
            timer += Time.deltaTime;
            GetComponent<Rigidbody>().AddForce((transform.position - knockOrigin.position).normalized * sensitivity);
            yield return new WaitForFixedUpdate();
        }

        // Knockout a little bit
        
    }

    public void ForceLookAt(Transform target){
        StartCoroutine(AnimateLookAt(target));
    }

    private IEnumerator AnimateLookAt(Transform target)
    {
        // player
        Quaternion playerInitRot = transform.rotation;
        Quaternion playerTargetRot = playerInitRot;

        Vector3 playerDirection = target.position - transform.position;
        Quaternion playerLookRot = Quaternion.LookRotation(playerDirection, Vector3.up);

        playerTargetRot.w = playerLookRot.w;
        playerTargetRot.y = playerLookRot.y;

        // camera
        Quaternion initCameraRot = playerCamera.transform.rotation;
        
        Vector3 cameraDirection = target.position - playerCamera.transform.position;
        Quaternion cameraLookRot = Quaternion.LookRotation(cameraDirection, Vector3.up);
        
        float timePassed = 0;

        while(timePassed < 1) {
            transform.rotation = Quaternion.Lerp(playerInitRot, playerTargetRot, timePassed);
            playerCamera.transform.rotation = Quaternion.Lerp(initCameraRot, cameraLookRot, timePassed);
            timePassed += Time.unscaledDeltaTime * 3;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
        // make sure the rotation is retained after animating
        rotation.y = transform.eulerAngles.y;
    }

    public void DisableCameraLook(bool disable){
        isDisableCameraLook = disable;
    }

    public void DisableMovement(bool disable) {
        isDisableMovement = disable;
    }

    public void PlayIntenseSFX(bool isEnable){
        if (isEnable) {
            FXSoundSystem.Instance.PlayRandom(FXAudioSelection.JUMPSCARES, true);
            FXSoundSystem.Instance.PlayRandom(FXAudioSelection.CREEPY, true);
            BGSoundSystem.Instance.PlayRandom(BGAudioSelection.HORROR, false);
            BGSoundSystem.Instance.PlayRandom(BGAudioSelection.RISERS, true,0.5f);
            // Breathing
            myAudio.PlayRandomLooped(0, 0.5f);
            // Heartbeat
            FXSoundSystem.Instance.PlayAudioClip(myAudio.audioClips[0].clip);
        } else {
            myAudio.Stop();
            BGSoundSystem.Instance.Stop();
            FXSoundSystem.Instance.Stop();
            BGSoundSystem.Instance.ResetClip();
            // Heartbeat end clip
            FXSoundSystem.Instance.PlayAudioClip(myAudio.audioClips[1].clip);
            // Breathing Relief
            myAudio.PlayRandom(1, true, 0.5f);
        }
    }

    private void PlayFootstep()
    {
        GameManager.instance.footstepSwapper.CheckLayers();
        myAudio.PlayRandomFromCollection(currentFootstep.footstepSounds, 0.15f);
    }
    public void SwapFootSteps(FootstepCollection footstepCollection) { 
        currentFootstep = footstepCollection;
    }
}
