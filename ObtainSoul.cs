using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainSoul : Interactable
{
    private TikbalangQuest quest;
    public TikbalangAI tikbalang;
    public GameObject player;
    private KeyCode keyCode = KeyCode.F;
    public Vector3 initialPos;
    private Vector3 targetPos;
    private bool isDetect;
    private float speed = 0.5f; 
    public override void Start()
    {
        base.Start();
        initialPos = transform.position;
        quest = GameManager.instance.GetComponent<TikbalangQuest>();
        targetPos = player.transform.position;
    }

    public override void ActivateInteractable(bool activate)
    {

        base.ActivateInteractable(activate);
        tikbalang.OnPlayerCollect();
        StartCoroutine(Animate());
    }


    private IEnumerator Animate()
    {
        float timeElapsed = 0;
        float duration = 0.5f;
        Vector3 initialPosition = transform.position;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float time = Easing.Cubic.InOut(timeElapsed / duration);
            transform.position = Vector3.Lerp(initialPosition, targetPos, time);
            yield return new WaitForEndOfFrame();
        }
        transform.position = targetPos;
        quest.isPressed = false;
        quest.IncrementSouls();
        
        this.gameObject.SetActive(false);
    }


    public void OnTakeSoul()
    {
        Debug.Log("OnTakeSoul");
/*        if(isDetect)
        { quest.isPressed = Input.GetKey(keyCode) ? true : false; }*/
        
        if (this.transform.position != initialPos && !quest.isPressed)
        {
         transform.position = Vector3.Lerp(transform.position, initialPos, speed * Time.deltaTime);
         tikbalang.OnPlayerCollect();
        }
    }
/*    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>())
        {

            isDetect = true;
            if (quest.isPressed)
            {
                transform.position = Vector3.Lerp(this.transform.position, other.transform.position, 0.1f * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, other.transform.position) <= 2)
                {
                    quest.isPressed = false;
                    quest.soulCount++;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isDetect = false;
    }*/
}
