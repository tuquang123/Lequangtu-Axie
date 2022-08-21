using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public int damage = 1;
    private Vector3 targetPosition;
    public float speed = 5;
    //public GameObject deathEffect;

    private void Awake()
    {
        //transform.Rotate(0,0,-90);
    }

    private void Start()
    {
        //Destroy(gameObject, 10f);
        targetPosition = FindObjectOfType<HpEnemy>().transform.position;
    }
    private void Update()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(targetPosition == transform.position)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HpEnemy enemy = other.GetComponent<HpEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Instantiate(bullet,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}