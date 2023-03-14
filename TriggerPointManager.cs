using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPointManager : MonoBehaviour
{
    public BlackLady blackLady;
    
    public void CollisionDetected(BlackLadyPosition blackLadyPosition, int index)
    {
        if(!blackLady.gameObject.activeInHierarchy) return;
        if (index == blackLady.currentIndex)
        {
            blackLady.Teleport(blackLady.lastIndex);
            blackLady.lastIndex = blackLady.currentIndex;

        }
        else
        { 

            blackLady.currentIndex = index;
            blackLady.Teleport(blackLady.currentIndex);
        }

    }


}
