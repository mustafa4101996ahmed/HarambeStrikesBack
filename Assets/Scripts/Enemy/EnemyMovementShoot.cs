using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementShoot : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    float playerDistance;
    public bool following;
    public EnemyAttackShoot shoot;
    private GameObject target;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        following = true;
        target = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        targetPoint = target.transform.position;
        targetPoint.y = transform.position.y;
        transform.LookAt(targetPoint);
        // If the enemy and the player have health left...
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (playerDistance <= 5)
            {
                following = false;
                nav.enabled = false;
                //shoot.playerInRange = true;
                //Debug.Log("Attacking Player Now!");
            }
            else
            {
                following = true;
                // ... set the destination of the nav mesh agent to the player.
                nav.enabled = true;
                nav.SetDestination(player.position);
                //shoot.playerInRange = false;
            }
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }
}


