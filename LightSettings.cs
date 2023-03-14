using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light Settings", menuName = "Light Settings", order = 1)]
public class LightSettings : ScriptableObject
{
    public List<LightSetting> lightSettings;
    public Dictionary<LightTemplate, Light> dictionary;

    void OnValidate() {
        dictionary = new Dictionary<LightTemplate, Light>();

        foreach(LightSetting lightSetting in lightSettings) {
            dictionary.Add(lightSetting.lightTemplate, lightSetting.lightPrefab);
        }
    }

    public Light GetLight(LightTemplate lightTemplate) {
        if (dictionary != null) return dictionary[lightTemplate];
        return null;
    }
}

[System.Serializable]
public struct LightSetting {
    public LightTemplate lightTemplate;
    public Light lightPrefab;
}

public enum LightTemplate {
    INCANDESCENT_1,
    FLOURESCENT_1,
    BLINKING_INCANDESCENT_1,
    BLINKING_INCANDESCENT_2,
    BLINKING_FLOURESCENT_1,
    BLINKING_FLOURESCENT_2,
    HANGING_LAMP_1,
    HANGING_LAMP_2,
    BLINKING_HANGING_LAMP_1,
    BLINKING_HANGING_LAMP_2,
    BLINKING_HANGING_LAMP_1_INVERTED,
    STANDING_LAMP_1,
    SMALL_LAMP_1
}