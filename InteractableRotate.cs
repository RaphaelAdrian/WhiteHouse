using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRotate : Interactable
{
    // Start is called before the first frame update
    public Transform rotateObject;
    public Vector3 targetRotationOffset;
    private Vector3 initRotation;
    private Vector3 targetRotation;

    public AudioClip audioActivate;
    public AudioClip audioDeactivate;


    public override void Awake() {
        if (rotateObject == null) rotateObject = this.transform;
        initRotation = rotateObject.localEulerAngles;
        base.Awake();
    }
    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        StartCoroutine(AnimateRotate(activate));

        AudioClip clip = activate ? audioActivate : audioDeactivate;
        if (clip) {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
        }
    }
    private IEnumerator AnimateRotate(bool isActivate)
    {
        targetRotation = isActivate ? initRotation + targetRotationOffset : initRotation;
        float timeElapsed = 0;
        float duration = 0.3f;
        Vector3 initialRot = rotateObject.localEulerAngles;
        while(timeElapsed < duration) {
            timeElapsed += Time.deltaTime;

            float time = Easing.Cubic.InOut(timeElapsed / duration);
            rotateObject.localEulerAngles = Vector3.Lerp(initialRot, targetRotation, time);
            yield return new WaitForEndOfFrame();
        }
        rotateObject.localEulerAngles = targetRotation;
    }
}
