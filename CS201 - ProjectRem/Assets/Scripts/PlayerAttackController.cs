using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float attackRange;
    public int attackDamage;
    public int totalKills;

    [Header("Attacking")]
    public Transform leftFoot;
    public Transform rightFoot;

    [Header("Keybinds")]
    public KeyCode leftAttack = KeyCode.Mouse0;
    public KeyCode rightAttack = KeyCode.Mouse1;

    public bool enemyInAttackRange;
    public LayerMask whatIsEnemy;
    RaycastHit hit;


    private void Update()
    {
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);


        if (enemyInAttackRange)
        {
            Physics.Raycast(transform.position, transform.forward, out hit, attackRange, whatIsEnemy);
            Debug.DrawRay(transform.position, transform.forward, Color.red);

            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, whatIsEnemy)) {

                if (hit.rigidbody.gameObject.CompareTag("Enemy"))
                {
                    if (Input.GetKeyDown(leftAttack)) LeftAttack();
                    if (Input.GetKeyDown(rightAttack)) RightAttack();
                }
                else
                {
                    Debug.Log("Stop hitting, Not an enemy");
                }
            }

        }

    }

    private void LeftAttack()
    {
        Debug.Log("Left Attack");
        hit.collider.gameObject.TryGetComponent<EnemyController>(out var rat);
        rat.TakeDamage(attackDamage);
        totalKills++;
    }

    private void RightAttack()
    {
        Debug.Log("Right Attack");
        hit.collider.gameObject.TryGetComponent<EnemyController>(out var rat);
        rat.TakeDamage(attackDamage);
        totalKills++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
       // Gizmos.color = Color.red;
       // Gizmos.DrawRay(transform.position, transform.f);

    }
}
