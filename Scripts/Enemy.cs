using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Vector3 targetPosition;

    private bool isMoving;
    public bool patrolling;
    public bool attack;
    public bool chasing;

    public Transform target;

    [Header("Layer")] 
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;

    public void Update()
    {
        Vector3 p = transform.position;
        
        patrolling = !Physics.CheckSphere(p, 5, playerMask);
        chasing = Physics.CheckSphere(p, 5, playerMask) & 
                 !Physics.CheckSphere(p, 0.5f, playerMask);
        attack = Physics.CheckSphere(p, 0.5f, playerMask);

        if (chasing & Physics.Raycast(transform.position, Angle(target), 2, obstacleMask))
        {
            chasing = false;
            patrolling = true;
        }

        if (Agent.remainingDistance < 0.1f)
            isMoving = true;
        else
            isMoving = false;
    }

    private void FixedUpdate()
    {
        //巡邏
        if (patrolling & isMoving)
            Agent.SetDestination(RandomPosition());
        //追縱
        if (chasing)
            Agent.SetDestination(target.position);
        //攻擊
        if (attack)
            Debug.Log("I catch you.");
    }

    private Vector3 Angle(Transform t)
    {
        return t.position - transform.position;
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-5, 5);
        float y = Random.Range(-5, 5);

        Vector3 p = new Vector3(x, 0, y);
        
        return p;
    }

    private void OnDrawGizmos()
    {
        Vector3 p = transform.position;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(p, 5);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(p, 0.5f);
    }
}
