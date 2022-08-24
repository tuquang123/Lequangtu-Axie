using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Enemy enemy;
    public GameObject fx;

    void Start()
    {
        Invoke("SkillBoss", 15f);
        
        Invoke("SkillBoss", 35f);
    }

    public void SkillBoss()
    {
        enemy.Attack(10);
        enemy.AttackAnimation();
        Instantiate(fx, enemy.target.transform.position, Quaternion.identity);
    }
}
