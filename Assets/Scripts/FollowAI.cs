using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    public enum States { Patrol, Follow, Attack };

    public NavMeshAgent agent;
    public Transform target;

    public Transform[] waypoints;

    [Header("AI Properties")]
    public float maxFollowDistance = 10f;
    public float shootDistance = 5f;
    public Weapon attackWeapon;

    private bool insight = false;
    private Vector3 directionToTarget;

    public States currentState = States.Patrol;

    private int currentWaypoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateStates();
        CheckForPlayer();
    }

    private void updateStates() {
        switch (currentState ){
            case States.Patrol:
                Patrol();
                break;
            case States.Follow:
                Follow();
                break;
            case States.Attack:
                Attack();
                break;
        }
    }

    public void CheckForPlayer(){
        directionToTarget = target.position - transform.position;

        RaycastHit hitInfo; 

        if (Physics.Raycast(transform.position, directionToTarget.normalized, out hitInfo))
        {
            insight = hitInfo.transform.CompareTag("Player");
        }
    }

    private void Patrol() {
        attackWeapon.canUse = false;
        if (agent.destination != waypoints[currentWaypoint].position)
        {
            agent.destination = waypoints[currentWaypoint].position;
        }
        if (HasReached())
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    private void Follow() {
        attackWeapon.canUse = false;
        if (agent.remainingDistance <= shootDistance && insight)
        {
            agent.ResetPath();
            currentState = States.Attack;
            
        } 
        else {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    private void Attack() {
       if (!insight) {
        currentState = States.Follow;
       } else {
       attackWeapon.canUse = true;
    //    target.GetComponent<HealthController>().TakeDamage(attackWeapon.damage);
    }
    }

    private bool HasReached(){
        return (!agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance); 
    }

 private void OnTriggerEnter(Collider other)
    {
        print("Here trigger");
        // Check if the object colliding with the enemy has a specific tag
        if (other.CompareTag("Bullet"))
        {
            // If the colliding object is a bullet, destroy the enemy
            print("A bullet");
            //target.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            print("Not a bullet");
            print("Tag: " + other.tag);
            print("Name: " + other.name);
            print(other);
            //Destroy(gameObject);        
            }
    }

}

   