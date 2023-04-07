using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform plant;
    public LayerMask whatIsGround, whatIsPlant;

    public float health;
    public int attackDamage;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool plantInSightRange, plantInAttackRange;

    private void Start()
    {
        plant = GameObject.FindWithTag("Plant").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        //Check for sight and attack range
        plantInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlant);
        plantInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlant);
        
        if (!plantInSightRange && !plantInAttackRange) Wandering();

        if (plantInSightRange)
        {
            FindNearestPlant();

            if (plant.TryGetComponent<PlantController>(out var p)) {

                if (!plantInAttackRange)
                    NavigateToPlant();
            }
        }
    }

    private void Wandering()
    {
        if (!walkPointSet) SearchForWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < .1f)
            walkPointSet = false;
    }

    private void SearchForWalkPoint()
    {

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, .2f, whatIsGround))
            walkPointSet = true;

    }

    private void NavigateToPlant()
    {

        agent.SetDestination(plant.position);
    }

    private void FindNearestPlant()
    {
        if (!plant.gameObject.activeSelf) plant = GameObject.FindWithTag("Plant").transform;

        RaycastHit hit;
        if (!Physics.Raycast(agent.transform.position, transform.position, out hit, sightRange, whatIsPlant))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.white);
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);

            if (hit.rigidbody.gameObject.CompareTag("Plant"))
            {
                plant = hit.rigidbody.gameObject.transform;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == plant.name)
        {
            plant.TryGetComponent<PlantController>(out var p);
                p.canBeAttacked = true;
                AttackPlant();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == plant.name)
        {
            plant.TryGetComponent<PlantController>(out var p);
            p.canBeAttacked = false;
        }
    }


    private void AttackPlant()
    {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(plant);

        if (!alreadyAttacked  && GameObject.FindGameObjectsWithTag("Farm").Length > 0)
        {
            plant.gameObject.TryGetComponent<PlantController>(out var p);
            p.beingAttacked = true;
            p.TakeDamage(attackDamage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(this.name + " has been hit with " + damage + ". Health now: " + health);
        if (health == 0) Invoke(nameof(DestroyRat), 0.5f);
    }

    private void DestroyRat()
    {
        TryGetComponent<RatSpawner>(out var ratSpawner);
        ratSpawner.countRats--;
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
