using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using Observer;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool boss;
    private SkeletonAnimation skeletonAnimation;
    public Transform target;
    public Transform attackPoint;
    public LayerMask enemyLayers;
  
    public float speed = 3f;
    public float minimumDistance;
    public float attackRate = 1f;
    public int damage;
    public float attackRage = 0.5f;

    [HideInInspector] public float nextAttackTime = 0f;
    [HideInInspector] public Vector3 targetDir;

    [SerializeField] private bool isActive = true;

    private void Awake()
    {
        skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
        this.RegisterListener(EventID.OnShowCard, (param) => Stop());
        this.RegisterListener(EventID.OnStopShowCard, (param) => Continue());
    }
    
    void Stop()
    {
        isActive = false;
    }

    void Continue()
    {
        isActive = true;
    }
    
    public void Attack(int dame)
    {
        //Nhan dien enemy va attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRage, enemyLayers);

        //dame them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HpPlayer>().TakeDamage(dame);
        }
    }
    private void Turning()
    {
        if (target == null) return;
        //if (turnOff)
        {
            targetDir = target.position - transform.position;
            Transform charTrans = transform;
            charTrans.transform.localScale = new Vector3(Mathf.Sign(-targetDir.x), 1, 1);
        }
    }
    // draw radius att
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRage);
    }
    private void FixedUpdate()
    {
        if(!isActive) return;
        
        Turning();
        if (target == null)
        {
            //anm.SetBool(Run, false);
        }
        else if (target)
        {
            if (Vector2.Distance(transform.position, target.position) > minimumDistance)//&& check.gg == false)
            {
                //anm.SetBool(Run, true);
                transform.position =
                    Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, target.position) <= minimumDistance) //&& check.gg == true)
            {
                if (Time.time >= nextAttackTime)
                {
                    //anm.SetBool(Run, false);
                    AttackAnimation();
                    Attack(damage);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, minimumDistance);
    }
    public void AttackAnimation()
    {
        skeletonAnimation.timeScale = 1f;
        if(boss)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/melee/normal-attack", false);
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/melee/horn-gore", false);
           
        }
        //anm.SetTrigger(Att);
    }
}