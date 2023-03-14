using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Wait : MonoBehaviour
{
    // Start is called before the first frame update
    public float waitingTime;

    void Start()
    {
        StartCoroutine(WaitNextScene(waitingTime)) ;    
    }

    // Update is called once per frame



    IEnumerator WaitNextScene(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene(1);
    }
}
