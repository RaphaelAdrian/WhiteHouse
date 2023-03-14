using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TiyanakAI : SmartEnemyNPC
{
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    float invalidPathTimer;

    public override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = this.speed;
    }

    public override void Update()
    {
        base.Update();
        velocity = navMeshAgent.velocity;

        // if there is no way, then move to next waypoint
        if (navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial || navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            if (invalidPathTimer < 0.5f)
            {
                invalidPathTimer += Time.deltaTime;
            }
            else
            {
                invalidPathTimer = 0;
                enemyState = EnemyState.UNDETECTED;
                MoveToNextWayPoint();
            }
        }
    }

    public override void MoveToWaypoint(NPCWaypoint waypoint, Vector3 direction)
    {
        base.MoveToWaypoint(waypoint, direction);
        navMeshAgent.SetDestination(waypoint.transform.position);
    }

    public override void MoveChase(Vector3 direction, RaycastHit hit)
    {
        base.MoveChase(direction, hit);
        navMeshAgent.SetDestination(hit.transform.position);
    }

    public override void MoveOnEnemyEscaped(float leaveTimer, RaycastHit hit, Vector3 lastPositionBeforeEscape, Vector3 direction)
    {
        base.MoveOnEnemyEscaped(leaveTimer, hit, lastPositionBeforeEscape, direction);
        navMeshAgent.SetDestination(lastPositionBeforeEscape);
    }
}
