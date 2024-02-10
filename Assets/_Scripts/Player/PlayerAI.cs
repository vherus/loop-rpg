using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    private NavMeshAgent agent;
    private int wpIndex;
    private Vector3 target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1) {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[wpIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        wpIndex++;

        if (wpIndex == waypoints.Length) {
            wpIndex = 0;
        }
    }

    public void PauseMovement()
    {
        agent.isStopped = true;
    }
    
    public void ResumeMovement()
    {
        if (!agent) {
            agent = GetComponent<NavMeshAgent>();
        }
        
        agent.isStopped = false;
    }
}
