using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : Singleton<Globals>
{
    [Header("Outline Effect")]
    public LayerMask interactablesLayer;
    public LayerMask activeLayer;
    public LayerMask inspectLayer;
}


public static class Methods
{
    public static int GetLayer(LayerMask layerMask)
    {
        return (int)Mathf.Log(layerMask.value, 2);
    }

    public static string GetKeyString(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Escape: return "Esc";

            default:
                return keyCode.ToString();
        }
    }
    public static string ToRoman(int number)
    {
        if (number < 1) return string.Empty;            
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900); 
        if (number >= 500) return "D" + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C" + ToRoman(number - 100);            
        if (number >= 90) return "XC" + ToRoman(number - 90);
        if (number >= 50) return "L" + ToRoman(number - 50);
        if (number >= 40) return "XL" + ToRoman(number - 40);
        if (number >= 10) return "X" + ToRoman(number - 10);
        if (number >= 9) return "IX" + ToRoman(number - 9);
        if (number >= 5) return "V" + ToRoman(number - 5);
        if (number >= 4) return "IV" + ToRoman(number - 4);
        if (number >= 1) return "I" + ToRoman(number - 1);

        return "";
    }

    public static SlotData GetCurrentSlot()
    {
        return SaveSystem.LoadSlot(GetCurrentSlotNumber());
    }

    public static int GetCurrentSlotNumber()
    {
        return PlayerPrefs.GetInt("CurrentSlot", 0);
    }
}