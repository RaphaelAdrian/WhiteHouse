using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SantelmoTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject santelmo;
    public GameObject flashBack;
    

    private void OnTriggerEnter(Collider other)
    {
        flashBack.SetActive(true);
        StartCoroutine(Wait(25f));
    }
    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        santelmo.SetActive(false);

        flashBack.SetActive(false);
       
    }
}
