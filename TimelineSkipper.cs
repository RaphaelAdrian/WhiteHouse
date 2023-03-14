using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineSkipper : MonoBehaviour
{

    PlayableDirector playableDirector;
    void Start(){
        playableDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.KeypadMinus)) {        
            playableDirector.time += 2f;
        }
    }
}
