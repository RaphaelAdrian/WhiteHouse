using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
#endif
public class WhitehouseLight : MonoBehaviour
{
    public bool generateLights = true;
    public LightTemplate lightTemplate = LightTemplate.INCANDESCENT_1;
    private LightSettings lightingSettings;

    // Start is called before the first frame update
    Light[] lightsCreated = null;

    void Awake()
    {
        lightingSettings = Resources.Load<LightSettings>("Light Settings");
    }
#if UNITY_EDITOR
    void OnValidate() {
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;
        
        lightsCreated = GetComponentsInChildren<Light>();
        if (generateLights) {
            lightingSettings = Resources.Load<LightSettings>("Light Settings");
            if (lightsCreated == null || lightsCreated.Length == 0) {
                GenerateLights();
            } 
        } else if (lightsCreated.Length > 0) {
            StartCoroutine(DestroyPreview());
        }
    }

    private void GenerateLights()
    {
        try {
            PrefabUtility.InstantiatePrefab(lightingSettings.GetLight(lightTemplate), gameObject.transform);
        } catch (Exception){};
    }

    IEnumerator DestroyPreview()
    {
        yield return null;
        foreach(Light light in lightsCreated) {
            DestroyImmediate(light.gameObject);
        }
    }
#endif
}
