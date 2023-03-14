using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoints : MonoBehaviour
{
    private float lastWeight = 0;
    public float weight = 0;
    public Transform[] waypoints;
    // Start is called before the first frame update

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePosition();
    }

    void UpdatePosition(){
        Transform previousWaypoint = waypoints[(int)weight % waypoints.Length];
        Transform currentWaypoint = waypoints[((int)weight + 1) % waypoints.Length];
        float percentage = weight - (int) weight;
        gameObject.transform.position = Vector3.Lerp(previousWaypoint.transform.position, 
        currentWaypoint.transform.position, percentage);
        lastWeight = weight;
    }

}
