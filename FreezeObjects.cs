using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeObjects : MonoBehaviour
{
    public MonoBehaviour freezeComponent;
    // Start is called before the first frame update
    void FreezeComponents(bool enable)
    {
        freezeComponent.enabled = enable;
    }
}
