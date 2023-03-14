using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLadyPosition : MonoBehaviour
{
    public int indexPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            transform.parent.GetComponentInParent<TriggerPointManager>().CollisionDetected(this, indexPosition);
        }
    }
}
