using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackPlayer : MonoBehaviour
{
    public GameObject att;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    
    public int damage = 2;
    public float attackRage = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    private static readonly int Att1 = Animator.StringToHash("att");

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            //if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                AttAnimation();
                Att();
                nextAttackTime = Time.time + 1f / attackRate;
                //var bul =  Instantiate(att, transform.position, Quaternion.identity);
                //bul.transform.parent = gameObject.transform;

            }
        }

    }
    void AttAnimation()
    {
        //play attack animation
        animator.SetTrigger(Att1);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Att()
    {
        //Nhan dien enemy va attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRage, enemyLayers);

        //dame them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HpEnemy>().TakeDamage(damage);
        }

    }

    // draw radius att
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRage);
    }

}

