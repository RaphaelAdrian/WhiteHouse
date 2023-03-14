using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokenKeyDoor : Door
{
    bool isEnableUnlock = false;
    public UnityEvent onNotEnabled;

    public override bool CheckIfMatched(Items item)
    {
        if (!isEnableUnlock && (item == matchItem)) {
            GameMenu.instance.Toggle();
            onNotEnabled.Invoke();
        }
        return isEnableUnlock && (matchItem == item);
    }

    public void EnableUnlock(bool enable) {
        isEnableUnlock = enable;
    }

}
