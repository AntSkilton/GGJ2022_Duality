using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class RoamBehaviour : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    public bool moving = false;
    public bool waiting = false;
    public bool running = false;
    public float walkSpeed = 3.5f;
    public float runSpeed = 5f;
    public Vector3 targetPosition;
    public float maxRange = 20f;
    public float minWaitTime = 5f;
    public float maxWaitTime = 15f;
    public float stopDistanceSquared = 0.3f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Snap to the nearest navmesh point
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
        {
            transform.position = closestHit.position;
        }

        agent.enabled = true;
        agent.updatePosition = false;
    }

    void FixedUpdate()
    {
        if (!waiting && !moving)
        {
            FindNewTargetPos();
        }

        var distance = targetPosition - transform.position;
        if (distance.sqrMagnitude <= stopDistanceSquared && !waiting)
        {
            moving = false;
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        waiting = true;
        var waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        waiting = false;
        yield return null;
    }

    private void FindNewTargetPos()
    {
        Vector3 pos = transform.position;
        targetPosition = new Vector3();
        targetPosition.x = Random.Range(pos.x - maxRange, pos.x + maxRange);
        targetPosition.y = pos.y;
        targetPosition.z = Random.Range(pos.z - maxRange, pos.z + maxRange);

        var navMeshFilter = new NavMeshQueryFilter
        {
            agentTypeID = agent.agentTypeID,
            areaMask = agent.areaMask,
        };

        // Snap to the nearest navmesh point
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(targetPosition, out closestHit, 500, navMeshFilter))
        {
            targetPosition = closestHit.position;
        }

        agent.destination = targetPosition;
        moving = true;
    }


    void Update()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        if (running)
        {
            anim.SetBool("Walk Forward", false);
            anim.SetBool("Run Forward", shouldMove);
            agent.speed = runSpeed;
        }
        else
        {
            anim.SetBool("Walk Forward", shouldMove);
            anim.SetBool("Run Forward", false);
            agent.speed = walkSpeed;
        }

        var lookAtTargetPosition = agent.steeringTarget + transform.forward;
        transform.LookAt(lookAtTargetPosition);
    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = agent.nextPosition;
    }

    private void OnDrawGizmos()
    {
        Vector3 position = targetPosition;

        if (Vector3.Distance(position, Camera.current.transform.position) > 10f && !waiting)
            Gizmos.DrawIcon(position, "walking.png");
    }
}