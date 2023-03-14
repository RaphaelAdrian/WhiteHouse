using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInteractable : Interactable
{
    public bool isOpen;
    public float duration = 0.5f;
    public Vector3 moveOffset = Vector3.zero;

    private Vector3 initPos;
    private Vector3 targetPos;
    
    public AudioClip audioActivate;
    public AudioClip audioDeactivate;

    // Start is called before the first frame update
    public override void Start(){
        base.Start();
        initPos = transform.localPosition;
        targetPos = initPos;
    }

    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        SetOpen(activate);
        StartCoroutine(Animate());

        AudioClip clip = activate ? audioActivate : audioDeactivate;
        if (clip) {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator Animate()
    {
        float timeElapsed = 0;
        float duration = this.duration;
        Vector3 initialPosition = transform.localPosition;
        while(timeElapsed < duration) {
            timeElapsed += Time.deltaTime;

            float time = Easing.Cubic.InOut(timeElapsed / duration);
            transform.localPosition = Vector3.Lerp(initialPosition, targetPos, time);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = targetPos;
    }

    public void SetOpen(bool open) {
        isOpen = open;
        targetPos = isOpen 
        ? initPos + moveOffset
        : initPos;
    }
}
