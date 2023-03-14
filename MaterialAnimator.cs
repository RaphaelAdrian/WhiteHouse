using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialAnimator : MonoBehaviour
{

    public MaterialProperties materialProperties;

    // Start is called before the first frame update
    void Update() {
        materialProperties.materialToAnimate.SetFloat(materialProperties.materialPropertyName, materialProperties.value);
    }
}

[Serializable]
public struct MaterialProperties {
    public Material materialToAnimate;
    [Range(-1, 1)]
    public float value;
    public string materialPropertyName;
}