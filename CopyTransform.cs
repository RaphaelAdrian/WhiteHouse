using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyTransform : MonoBehaviour
{
    public Transform sourceTransform;
    public TransformVector position;
    public bool isLocalPosition;
    public TransformVector rotation;
    public bool isLocalRotation;

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }

    void UpdatePosition() {
        if (!position.enable) return;
        if (!isLocalPosition) transform.position = sourceTransform.position + position.offset;
        else transform.position = sourceTransform.TransformPoint(position.offset);
    }
    void UpdateRotation() {
        if (!rotation.enable) return;
        if (!isLocalPosition) transform.eulerAngles = sourceTransform.eulerAngles + rotation.offset;
        else transform.rotation = Quaternion.Euler(sourceTransform.eulerAngles) * Quaternion.Euler(rotation.offset);
        
    }

    public void Disable(float delay){
        StartCoroutine(DelayDisable(delay));
    }

    private IEnumerator DelayDisable(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.position.enable = false;
        this.rotation.enable = false;
    }
}

[System.Serializable]
public struct TransformVector {
    public bool enable;
    public Vector3 offset;
}
