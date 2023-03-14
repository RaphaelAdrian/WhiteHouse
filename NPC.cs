using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum EnemyState
    {
        UNDETECTED, IN_TRIGGER, CHASED, NEAR, KILLED
    }

    public enum NPCState
    {
        IDLE, PATROLLING, DETECTIONS_DISABLED, DEAD
    }

    [Header("NPC Chase")]
    public EnemyState enemyState;
    public NPCState npcState = NPCState.PATROLLING;
    public LayerMask playerAndObstaclesMask;
    public Transform raycastEye;

    [Header("NPC distance")]
    public float detectNearDistance = 6f;
    public float killDistance = 2f;


    [Header("NPC Chase Timers")]

    public float leaveSecondsAfterUndetected = 3;
    public float waitSecondsBeforeAttack = 1;
    protected float leaveTimer;
    protected float attackDelayTimer;


    protected Player player;
    protected Vector3 lastPositionBeforeAttack;
    protected Vector3 lastPositionBeforeEscape;
    EnemyState lastState;

    public virtual void Start()
    {
        player = GameManager.instance.player;
        enemyState = EnemyState.UNDETECTED;
        lastState = enemyState;
    }

    void OnTriggerStay(Collider other)
    {
        if (npcState == NPCState.DETECTIONS_DISABLED) return;
        if (enemyState == EnemyState.IN_TRIGGER) return;
        if (enemyState == EnemyState.UNDETECTED)
        {
            Player playerDetected = other.GetComponent<Player>();
            if (playerDetected)
            {
                attackDelayTimer = 0;
                enemyState = EnemyState.IN_TRIGGER;
            }
        }
    }

    void OnTriggerExit() {
        if (enemyState != EnemyState.IN_TRIGGER) return;
        enemyState = EnemyState.UNDETECTED;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // We would need to stop updating when there is no point to update
        if (npcState == NPCState.DEAD) return;
        if (enemyState == EnemyState.KILLED) return;

        bool isEnemyNotSeen = enemyState == EnemyState.UNDETECTED || enemyState == EnemyState.IN_TRIGGER;
        if (isEnemyNotSeen)
        {
            WhileEnemyNotSeen();
        }
        if (enemyState != EnemyState.UNDETECTED)
        {
            WhileOnTrigger();
        }

        // to handle Changing of States
        // do this so we call functions only once
        if (lastState != enemyState)
        {
            // animate based on states once
            HandleChangeStates(enemyState, lastState);
        }
        lastState = enemyState;
    }

    //
    // Summary:
    //     Update Method while enemy is never seen using raycast
    public virtual void WhileEnemyNotSeen(){}

    //
    // Summary:
    //     Update Method while enemy is in trigger
    //     This is where the raycast detection starts
    public virtual void WhileOnTrigger()
    {
        if (npcState == NPCState.DETECTIONS_DISABLED) return;
        
        // when I am detected, I need to check if there is no obstacle that blocks the manananggal from seeing me;
        Vector3 raycastDirection = player.raycastPos.position - raycastEye.position;
        RaycastHit hit = CheckIfPlayerInSight(raycastDirection);

        // if player is detected
        if (hit.transform.GetComponent<Player>()) {
            WhileEnemyDetected(raycastDirection, hit);
        } else if (enemyState != EnemyState.IN_TRIGGER) {
            WhileEnemyHidden(raycastDirection, hit);
        }
    }

    //
    // Summary:
    //     Update Method called when enemy is on trigger but the NPC can's see it
    private void WhileEnemyHidden(Vector3 direction, RaycastHit hit)
    {        
        if (leaveTimer < leaveSecondsAfterUndetected) {
            leaveTimer += Time.deltaTime;
            WhileEnemyEscaped(leaveTimer, hit);
        }
        else {
            enemyState = EnemyState.UNDETECTED;
            leaveTimer = 0;
        }
    }

    //
    // Summary:
    //     Update Method when enemy is raycasted / visible to player
    public virtual void WhileEnemyDetected(Vector3 raycastDirection, RaycastHit hit) {

        // Make sure enemy is in trigger before chasing
        // Before setting to chased, make sure that last state is IN_TRIGGER to avoid duplicate calls
        if (enemyState == EnemyState.IN_TRIGGER){
            enemyState = EnemyState.CHASED;
            OnEnemyDetected();
        }

        // Activate delay before attack (eg. so that you can have animation before moving or...
        // so that the player can anticipate)
        if (attackDelayTimer < waitSecondsBeforeAttack){
            attackDelayTimer += Time.deltaTime;
            WhileOnAttackDelay();
        }
        else{
            MoveChase(raycastDirection.normalized, hit);
        }
        lastPositionBeforeEscape = player.raycastPos.position;
    }

    
    //
    // Summary:
    //     Called before chasing starts to happen
    public virtual void OnEnemyDetected(){}


    //
    // Summary:
    //     Update Method while enemy is being chased (after waitForAttack seconds)
    //     This is where the all the near / far / kill distances is calculated
    public virtual void MoveChase(Vector3 direction, RaycastHit hit){
        leaveTimer = 0; // we need to reset the leave timer while we are being chased

        if (hit.distance < killDistance)
            enemyState = EnemyState.KILLED;
        else if (hit.distance < detectNearDistance)
            enemyState = EnemyState.NEAR;
        else
            enemyState = EnemyState.CHASED;
    }


    private RaycastHit CheckIfPlayerInSight(Vector3 direction)
    {
        RaycastHit hit;
        Physics.Raycast(raycastEye.position, direction, out hit, 200, playerAndObstaclesMask);
        Debug.DrawLine(raycastEye.position, direction * 200, Color.red, 0.05f);
        return hit;
    }
    private void HandleChangeStates(EnemyState currentState, EnemyState exitState)
    {
        OnExitState(exitState);
        OnEnterState(currentState, lastState);
    }

    public virtual void EnableDetections(bool isEnable) {
        npcState = isEnable ? NPCState.PATROLLING : NPCState.DETECTIONS_DISABLED;
        if(!isEnable) StartCoroutine(DelayLeave());
    }

    private IEnumerator DelayLeave()
    {
        yield return new WaitForSeconds(leaveSecondsAfterUndetected);
        enemyState = EnemyState.UNDETECTED;
    }

    public virtual void Die()
    {
        npcState = NPCState.DEAD;
    }

    public virtual void WhileOnAttackDelay()
    {
    }
    
    //
    // Summary:
    //     Update Method while enemy has escaped, just after being chased and before the NPC leaves for leaveSeconds
    public virtual void WhileEnemyEscaped(float leaveTimer, RaycastHit hit)
    {
    }

    public virtual void OnExitState(EnemyState exitState)
    {
    }

    public virtual void OnEnterState(EnemyState enterState, EnemyState lastState)
    {
    }
}
