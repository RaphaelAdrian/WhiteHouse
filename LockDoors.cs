using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoors : MonoBehaviour
{
    public Door[] disableDoors;
    public void LockDisabledDoors(){
        foreach(Door door in disableDoors) {
            door.SetBrokenAndClosed();
        }
    }

    public void UnlockDoors(){
        Debug.Log("unlocking doors");
        foreach(Door door in disableDoors) {
            door.SetBroken(false);
        }
    }
}
