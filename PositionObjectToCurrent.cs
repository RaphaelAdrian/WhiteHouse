using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionObjectToCurrent : MonoBehaviour
{
    public Transform objectToMove;
    public bool positionOnEnable;
    void OnEnable() {
        if (positionOnEnable)
            SetPositionToCurrent();
    }

    public void SetPositionToCurrent()
    {
        objectToMove.position = this.transform.position;
        objectToMove.rotation = this.transform.rotation;
    }
}
