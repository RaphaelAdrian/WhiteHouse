using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class OnTimelineEnd : MonoBehaviour
{
    public UnityEvent events;
    PlayableDirector director;

    // Start is called before the first frame update
    void Awake(){
        director = GetComponent<PlayableDirector>();
    }
    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        events.Invoke();
    }
}
