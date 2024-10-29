using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [Header("Stats")]
    public float walkSpeed;


    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        SetWandering();
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
    }

    private void SetWandering()
    {
        agent.speed = walkSpeed;
        agent.isStopped = false;
        WanderToNewLocation();
    }

    private void WanderToNewLocation()
    {
        Vector3 newLocation = GetWanderLocation();
        agent.SetDestination(newLocation);
    }

    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(
            transform.position + (Random.insideUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)),
            out hit, maxWanderDistance, NavMesh.AllAreas);
        return hit.position;
    }
}

