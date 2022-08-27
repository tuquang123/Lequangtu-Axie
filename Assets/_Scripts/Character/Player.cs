using Observer;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private bool isActive = true;
        private SkeletonAnimation _skeletonAnimation;
        public float speed = 3f;
        public Transform target;

        public float minimumDistance;
        public float attackRate = 1f;
        [HideInInspector]public float nextAttackTime = 0f;
    
        public int damage = 1;
        public float attackRage = 20f;
        public float attackRage2 = 2f;
        public Transform attackPoint;
        public LayerMask enemyLayers;
        [HideInInspector]public bool onAttack;

        [HideInInspector]public Vector3 targetDir;
        [HideInInspector]public float targetDis = Mathf.Infinity;

        public AudioSource att;
        private void Awake()
        {
            _skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
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

        public void Win()
        {
            _skeletonAnimation.timeScale = .8f;
            _skeletonAnimation.AnimationState.SetAnimation(0, "activity/victory-pose-back-flip", false); //att
        }
        public void FixedUpdate()
        {
            if (!isActive) return;
            
            Turning();
            FindEnemy();
            IsTargetTooFar();
        }

        private void FindEnemy()
        {
            if (this.target) return;
            foreach (Transform obj in EnemyList.enemyList.objects)
            {
                var dis = Vector3.Distance(transform.position, obj.position);
                if (dis <= attackRage)
                {
                    SetTarget(obj);
                    return;
                }
            }
        }

        private void SetTarget(Transform targetCurrent)
        {
            this.target = targetCurrent;
            return;
        }

        private void IsTargetTooFar()
        {
            if (this.target == null) return;
            if (!this.target.gameObject.activeSelf)
            {
                this.target = null;
                return;
            }
            targetDis = Vector3.Distance(transform.position, this.target.position);
            if (targetDis > attackRage) target = null;
        }
        public void Attack(int dame)
        {
            // audio 
            att.Play();

            //Nhan dien enemy va attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRage2, enemyLayers);

            //dame them
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<HpEnemy>().TakeDamage(dame);
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
            Gizmos.DrawWireSphere(attackPoint.position, attackRage2);
        }
        private void Update()
        {
            if (target == null)
            {
                //anm.SetBool(Run, false);
            }
            else if (target)
            {
                if (Vector2.Distance(transform.position,target.position) > minimumDistance)//&& check.gg == false)
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
                        if (!onAttack)
                        {
                            AttackAnimation();
                        }
                        Attack(damage);
                        nextAttackTime = Time.time + 2f / attackRate;
                        //HpPlayer.mana += 1;
                    }
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, minimumDistance);
        }
        void AttackAnimation()
        {
            _skeletonAnimation.timeScale = 1f;
            _skeletonAnimation.AnimationState.SetAnimation(0, "attack/melee/horn-gore", false);
            //anm.SetTrigger(Att);
        }
    }
}