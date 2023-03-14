using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapreFlashback : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject flashBack;

    void Start()
    {
        flashBack.SetActive(true);
        StartCoroutine(Wait(35f));
    }
    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        flashBack.SetActive(false);

    }
}
