using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RatScare : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 15;
    void Start(){
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        float timer = 5f;
        gameObject.SetActive(true);
        int direction = Random.Range(0, 360);
        transform.eulerAngles = Vector3.up * direction;

        while (timer > 0) {
            timer -= Time.deltaTime;
            transform.localPosition += transform.forward * Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
