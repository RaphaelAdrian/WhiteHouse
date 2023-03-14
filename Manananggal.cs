using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manananggal : SmartEnemyNPC
{
    [Header("Manananggal")]
    public Vector3 chasePositionOffset;
    int randNextRotation;

    public override void MoveToWaypoint(NPCWaypoint waypoint, Vector3 direction)
    {
        base.MoveToWaypoint(waypoint, direction);
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if (Vector3.Magnitude(velocity) < 1)
        {
            // rotate for a while after slowing down
            Vector3 rot = Vector3.up * Vector3.Magnitude(velocity) * randNextRotation * Time.deltaTime;
            float rotX = Mathf.Lerp(transform.eulerAngles.x, 0, 0.01f);
            transform.eulerAngles += rot;
        }
        else
        {
            // make sure to face player
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.02f);
        }

        // Move
        transform.position = Vector3.SmoothDamp(transform.position,
        waypoint.transform.position, ref velocity, 1.5f);
    }
    
    public override void MoveChase(Vector3 direction, RaycastHit hit)
    {
        base.MoveChase(direction, hit);
        Vector3 movement = direction * Time.deltaTime * speed + chasePositionOffset;
        transform.position += movement;
        velocity = direction * 10f;
    }


    public override void MoveOnEnemyEscaped(float leaveTimer, RaycastHit hit, Vector3 lastPositionBeforeEscape, Vector3 direction)
    {
        base.MoveOnEnemyEscaped(leaveTimer, hit, lastPositionBeforeEscape, direction);
        if (hit.distance < 1.5f) return; // if there is an obstacle in front, of course don't proceed to lastplayerpos
        transform.position += direction * Time.deltaTime * speed;
    }

    public override void OnWaypointReached(NPCWaypoint waypoint, NPCWaypoint nextWaypoint)
    {
        base.OnWaypointReached(waypoint, nextWaypoint);
         // after each point randomize rotation in each pause
        randNextRotation = Random.Range(-2, 3) * 180;
    }

    public void Freeze(bool enabled)
    {
        animator.SetBool("isFrozen", enabled);
        this.enabled = !enabled;
    }
    private IEnumerator DetectEnemy()
    {
        yield return new WaitForEndOfFrame();
        enemyState = EnemyState.IN_TRIGGER;
    }

    public override void Die()
    {
        GetComponent<Collider>().enabled = false;
        base.Die();
    }
}
