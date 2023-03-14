using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObstaclesInBasement : MonoBehaviour
{
    public ApplyForceInteractable[] boxInteractables;
    // Start is called before the first frame update
    public void Enable(bool isEnable){
        foreach(ApplyForceInteractable boxInteractable in boxInteractables) {
            boxInteractable.enabled = true;
            boxInteractable.EnableRigidbodies(isEnable);
        }
    }
}
