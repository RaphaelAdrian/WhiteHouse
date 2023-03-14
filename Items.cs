using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New item", menuName = "Items")]
public class Items : ScriptableObject
{
    // Start is called before the first frame update
    
    public string itemName;
    public Sprite image;
    [TextArea(15, 20)]
    public string itemDescription;
}
