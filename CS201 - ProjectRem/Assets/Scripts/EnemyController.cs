using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float health;
    public GameObject mindmapIcon;
    public GameObject spawner;

    [Header("Attacking")]
    public int attackDamage;
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public GameObject closestPlant;
    public int numOfPlants;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        mindmapIcon.transform.position = transform.position;
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        if (plants.Length > 0)
        {
            numOfPlants = plants.Length;
            closestPlant = plants[0];
            foreach (GameObject plant in plants)
            {
                plant.TryGetComponent<PlantController>(out var p);
                if (Vector3.Distance(plant.transform.position, transform.position) < Vector3.Distance(closestPlant.transform.position, transform.position) && !p.beingAttacked)
                {
                    closestPlant = plant;
                }
            }
            transform.LookAt(closestPlant.transform.position);

        }
        agent.SetDestination(closestPlant.transform.position);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Plant"))
        {
            if (other.name == closestPlant.name)
            {
                closestPlant.TryGetComponent<PlantController>(out var p);
                p.canBeAttacked = true;
                AttackPlant();
            }
        }

        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryGetComponent<PickupController>(out var p);
                p.PickUp();
                agent.speed = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plant"))
        {
            if (other.name == closestPlant.name)
            {
                closestPlant.TryGetComponent<PlantController>(out var p);
                p.canBeAttacked = false;
            }
        }
    }


    private void AttackPlant()
    {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(closestPlant.transform);

        if (!alreadyAttacked && GameObject.FindGameObjectsWithTag("Farm").Length > 0)
        {
            closestPlant.gameObject.TryGetComponent<PlantController>(out var p);
            p.beingAttacked = true;
            p.TakeDamage(attackDamage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        closestPlant.gameObject.TryGetComponent<PlantController>(out var p);
        p.beingAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(this.name + " has been hit with " + damage + ". Health now: " + health);
        if (health <= 0)
        {
            spawner.TryGetComponent<RatSpawner>(out var ratSpawner);
            ratSpawner.countRats--;
            Destroy(gameObject);
        }
    }
}
