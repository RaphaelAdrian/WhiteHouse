using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Audio))]
[RequireComponent(typeof(BoxCollider))]
public class SmartEnemyNPC : NPC
{

    [Header("Character Stats")]
    public float speed = 6.5f;


    [Header("Waypoint System")]
    int waypointIndex = 0;
    public float minDistanceToWaypoint = 2f;
    public GameObject waypointsParent;
    NPCWaypoint currentWayPoint;
    NPCWaypoint lastWayPoint;
    List<NPCWaypoint> waypoints;
    Vector3 targetWaypointPosition;


    
    [Header("Audio")]
    Audio myAudio;


    [Header("Optional Animations")]
    public string animVelocityXFloatName;
    public string animVelocityYFloatName;
    public string animIsNearBoolName;
    public Animator animator;


    // Others
    protected Vector3 velocity;
    int animVelocityX;
    int animVelocityY;
    int animIsNearBool;


    public virtual void Awake(){
        animator = animator ? animator : GetComponent<Animator>();
        waypoints = waypointsParent.GetComponentsInChildren<NPCWaypoint>().ToList();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        currentWayPoint = waypoints[0];
        lastWayPoint = currentWayPoint;

        // Cache animations for faster performance
        animVelocityX = GetAnimationHash(animVelocityXFloatName);
        animVelocityY = GetAnimationHash(animVelocityYFloatName);
        animIsNearBool = GetAnimationHash(animIsNearBoolName);

        myAudio = GetComponent<Audio>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (animVelocityX != 0) animator.SetFloat(animVelocityX, Vector3.Magnitude(velocity));
        if (animVelocityY != 0) animator.SetFloat(animVelocityY, velocity.y);
    }

    public override void WhileEnemyNotSeen()
    {
        base.WhileEnemyNotSeen();

        // if point not reached, then move
        if (Vector3.Distance(transform.position, currentWayPoint.transform.position) >= minDistanceToWaypoint)
        {
            Vector3 direction = (currentWayPoint.transform.position - transform.position).normalized;
            MoveToWaypoint(currentWayPoint, direction);
        }
        else
        {
            lastWayPoint = currentWayPoint;
            MoveToNextWayPoint();

            OnWaypointReached(lastWayPoint, currentWayPoint);
        }
    }

    protected void MoveToNextWayPoint()
    {
        // increment waypointIndex
        waypointIndex = (waypointIndex + 1) % waypoints.Count;
        // set nextwaypoint
        currentWayPoint = waypoints[waypointIndex];
    }

    public virtual void OnWaypointReached(NPCWaypoint waypoint, NPCWaypoint nextWaypoint){
        // play idle audio
        myAudio.PlayRandom(0, false);

        // if the waypoint is on top, basically, I can see everything, so I can
        // immediately detect Juana
        // But if I can't see her, I won't chase here

        if (waypoint.isDetectInAnyRange)
        {
            enemyState = EnemyState.IN_TRIGGER;
        }
    }

    // activate detection timeline just 
    public override void OnEnemyDetected()
    {
        base.OnEnemyDetected();   
        if (leaveTimer == 0){
            TimelineActivator timelineActivator = GetComponent<TimelineActivator>();
            if (timelineActivator) timelineActivator.PlayTimeline(0);
        }
    }
    public override void WhileEnemyDetected(Vector3 raycastDirection, RaycastHit hit)
    {
        base.WhileEnemyDetected(raycastDirection, hit);
        Quaternion lookRotation = Quaternion.LookRotation(raycastDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
    }

    public override void WhileEnemyEscaped(float leaveTimer, RaycastHit hit)
    {
        base.WhileEnemyEscaped(leaveTimer, hit);
        Vector3 direction = (lastPositionBeforeEscape - raycastEye.position).normalized;
        MoveOnEnemyEscaped(leaveTimer, hit, lastPositionBeforeEscape, direction);
    }

    public virtual void MoveOnEnemyEscaped(float leaveTimer, RaycastHit hit, Vector3 lastPositionBeforeEscape, Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.02f);
    }

    public override void OnExitState(EnemyState exitState)
    {
        base.OnExitState(exitState);
        if(exitState == EnemyState.NEAR){
            if (animIsNearBool != 0) animator.SetBool(animIsNearBool, false);
        } 
    }

    public override void OnEnterState(EnemyState currentState, EnemyState lastState)
    {
        base.OnEnterState(currentState, lastState);

        if (currentState == EnemyState.UNDETECTED && lastState != EnemyState.IN_TRIGGER) {
            GameManager.instance.HUD.HideHUD();
            player.PlayIntenseSFX(false);
            myAudio.PlayRandom(0, false); // play idle after leave
        } 
        
        else if (currentState == EnemyState.NEAR) {
            if (animIsNearBool != 0) animator.SetBool(animIsNearBool, true);
            myAudio.PlayRandom(2, true);
            FXSoundSystem.Instance.SetSoundVolume(1f);
        }

        else if (currentState == EnemyState.KILLED) {
            player.KnockOut(this.transform, this);
        }
        
        // Play audio when detected
        if (lastState == EnemyState.UNDETECTED || lastState == EnemyState.IN_TRIGGER)
        {
            if (currentState == EnemyState.UNDETECTED || currentState == EnemyState.IN_TRIGGER) return;
            lastPositionBeforeAttack = transform.position;
            // GameManager.instance.HUD.ShowHUDFadeInOut(1);
            myAudio.PlayRandom(1, false);
            player.PlayIntenseSFX(true);
        }
    }

    public override void EnableDetections(bool isEnable)
    {
        base.EnableDetections(isEnable);
        if(!isEnable) player.PlayIntenseSFX(false);
    }

    public override void Die()
    {
        base.Die();
        GameManager.instance.HUD.HideHUD();
        player.PlayIntenseSFX(false);
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public virtual void MoveToWaypoint(NPCWaypoint waypoint, Vector3 direction){
    }
    
    public void SetCurrentWayPoint(NPCWaypoint waypoint)
    {
        currentWayPoint = waypoint;
        waypointIndex = waypoints.IndexOf(waypoint) % waypoints.Count;
        attackDelayTimer = 0;
    }


    public int GetAnimationHash(string animationName) {
        return string.IsNullOrEmpty(animationName) ? 0 : Animator.StringToHash(animationName);
    }
}

