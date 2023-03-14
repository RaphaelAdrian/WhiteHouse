using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerTutorialControls : MonoBehaviour
{

    public TimelineManager timelineManager;
    public Player actualPlayer;
    public GameObject manananggalHolding;
    public GameObject actualManananggal;
    public Animator animator;
    public float holdSecondsToBreakOut = 3f;
    public Transform ground;

    bool isBreakingOut;
    bool isBrokeOut;
    bool isLanded;
    float currentBreakOutSeconds;
    float distToGround;

    [Header("Cinemachine Cameras")]
    public CinemachineVirtualCamera vCamBreakingOut;
    public CinemachineVirtualCamera vCamFallingDown;
    public CinemachineVirtualCamera vCamLanded;

    void Start(){
        vCamBreakingOut.gameObject.SetActive(false);
        distToGround = Vector3.Distance(transform.position, ground.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBrokeOut) {     
            // BREAKING OUT
            // If key is hold, then play break out animation
            if (Input.GetKeyDown(KeyCode.F)) {
                isBreakingOut = true;
                animator.SetBool("isBreakingOut", true);
                vCamBreakingOut.gameObject.SetActive(true);
            }

            // reset if let go
            if (Input.GetKeyUp(KeyCode.F)) {
                isBreakingOut = false;
                animator.SetBool("isBreakingOut", false);
                vCamBreakingOut.gameObject.SetActive(false);
                currentBreakOutSeconds = 0;
            }

            if (isBreakingOut) {
                currentBreakOutSeconds += Time.deltaTime;
                if (currentBreakOutSeconds >= holdSecondsToBreakOut) {
                    // Falling Down
                    GetComponent<CopyTransform>().enabled = false;
                    animator.SetBool("isBreakingOut", false);
                    animator.SetBool("isFallingDown", true);
                    vCamFallingDown.gameObject.SetActive(true);

                    Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    isBreakingOut = false;
                    isBrokeOut = true;
                }
            }
        } else if (!isLanded){
            // LANDING
            if (Physics.Raycast(transform.position, Vector3.down, distToGround)) {
                animator.SetBool("isFallingDown", false);
                animator.SetBool("isLanded", true);
                vCamLanded.gameObject.SetActive(true);
                isLanded = true;

                StartCoroutine(SwitchToPlayer());
            }
        }
    }

    public IEnumerator SwitchToPlayer(){
        yield return new WaitForSeconds(2f);
        actualManananggal.transform.position = manananggalHolding.transform.position;
        actualManananggal.SetActive(true);
        timelineManager.EndAllTimelines();
        actualPlayer.gameObject.transform.position = transform.position;
        actualPlayer.gameObject.transform.rotation = transform.rotation;
        actualPlayer.gameObject.SetActive(true);
    }
}
