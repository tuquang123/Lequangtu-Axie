using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HpEnemy enemy = other.GetComponent<HpEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        
    }
}
