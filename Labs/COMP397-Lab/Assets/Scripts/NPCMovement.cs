using KBCore.Refs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCMovement : MonoBehaviour
{
    [SerializeField, Self] private NavMeshAgent agent;
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] private NPCState currentState;
    [SerializeField] private Transform player;
    private Vector3 destination;
    private int index;

    private void OnValidate() => this.ValidateRefs();


    void Start()
    {
        currentState = NPCState.Patrol;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();
        if (waypoints.Count < 0) return;
        agent.destination = destination = waypoints[index].transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCState.Patrol:
                if (waypoints.Count < 0) return;
                if (Vector3.Distance(transform.position, destination) < 3f)
                {
                    index = (index + 1) % waypoints.Count;
                    destination = waypoints[index].transform.position;
                    agent.destination = destination;
                }
                break;
            case NPCState.Chase:
                agent.destination = player.position;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = NPCState.Chase;
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = NPCState.Patrol;
            agent.destination = destination;
        }
    }
}

[System.Serializable]
public enum NPCState
{
    Patrol, Chase
}
