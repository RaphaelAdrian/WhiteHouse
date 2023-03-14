using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent navMesh;
    public Transform playerPos;

    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        navMesh = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        OnChase(playerPos);
    }

    void OnChase(Transform target)
    {
        navMesh.destination = target.position;
        Vector3 lookAtGoal = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.LookAt(lookAtGoal);
    }
}
