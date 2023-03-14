using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineActivator : MonoBehaviour
{
    public GameObject[] timelines;

    public void Start(){
        DisableAllTimelines();
    }
    public void PlayTimeline(int index){
        timelines[index].SetActive(true);
    }

    public void DisableAllTimelines(){
        foreach(GameObject timeline in timelines) {
            timeline.SetActive(false);
        }
    }
}
