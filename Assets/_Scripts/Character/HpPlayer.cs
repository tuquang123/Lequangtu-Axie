using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Observer;

namespace _Scripts
{
    public class HpPlayer : MonoBehaviour
    {
      //Class
      [Header("Class")] 
        public CameraShake        cameraShake;
        public Player             player;
        private SkeletonAnimation _skeletonAnimation;
        public ManaBar            manaBar;
        public HPBar              hPBar;
        public Text               textHp;
        
        //gameObj
        [Header("GameObject")]
        public GameObject skillPanel;
        public GameObject skillImage1;
        public GameObject skillImage2;
        public GameObject skill;
        public GameObject skill2;
        public GameObject skill3;
        public GameObject fxThunder;
        public GameObject fxExplosive;
        public GameObject fxPlazma;
        public GameObject floatTextPrefab;
        public GameObject floatTextHpPrefab;
        public GameObject deathEffect;
        public GameObject fx;
        
        //variable
        [Header("variable")]
        int         _currentHealth;
        float       _mana = 0;
        float       _mana2 = 0;
        float       _mana3 = 0;
        public int   health = 32;
        public float maxMana = 10; //electric
        public float maxMana2 = 5; //hp 
        public float maxMana3 = 5; //Boom
        
        //Bool Skill
        public bool skillActive1;
        public bool skillActive2;
        public bool skillActive3;
        public bool skillActiveMain;
        public GameObject activeWhenButtonCall;
        readonly bool _skillAfterActive;

        //audio
        public AudioSource fire;
        public AudioSource healing;
        public AudioSource thurder;

        public HpPlayer(bool skillAfterActive)
        {
            this._skillAfterActive = skillAfterActive;
        }

        private void Update()
        {
            textHp.text = _currentHealth.ToString();
        }
        private void Awake()
        {
            _skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
            this.RegisterListener((EventID.OnUseCard), (param) =>UseSkill((Card)param));
        }
        private void Start()
        {
            manaBar.SetMaxMana(maxMana);
            _currentHealth = health;
            hPBar.SetMaxHealth(health);
        }
        private void FixedUpdate()
        {
            manaBar.SetMana(_mana);
            _mana += 1f * Time.deltaTime;
            _mana = Mathf.Clamp(_mana, 0f, maxMana);
            
            _mana2 += 1f * Time.deltaTime;
            _mana2 = Mathf.Clamp(_mana2, 0f, maxMana2);
            
            _mana3 += 1f * Time.deltaTime;
            _mana3 = Mathf.Clamp(_mana3, 0f, maxMana3);
            
            //Skill 1 
            if (_mana == maxMana && skillActive1)
            {
                //audio 
                fire.Play();

                //active
                skillPanel.SetActive(true);
                skillImage1.SetActive(true);
                player.onAttack = true;
                
                //instantiate
                Instantiate(skill,transform.position,Quaternion.identity);
                Instantiate(fxExplosive,player.target.transform.position,Quaternion.identity);
                
                //animation
                _skeletonAnimation.timeScale = 0.6f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "defense/evade", false); // shot
                Debug.Log("defense/evade");
                
                //reset mana
                _mana = 0;
                
                Invoke(nameof(Disable), 1.5f);
                //dame
                player.Attack(10);
            }
            
            //skill 2 
            else if (_mana2 == maxMana2 && skillActive2)
            {
                healing.Play();
                skill2.SetActive((true));
                player.onAttack = true;
                skillImage2.SetActive(true);
                Instantiate(skill2, transform.position, Quaternion.identity);
                Instantiate(fxPlazma, transform.position, Quaternion.identity);
                //animation
                _skeletonAnimation.timeScale = 0.6f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "battle/get-buff", false);
                Debug.Log("battle/get-buff");
                _mana2 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable2), 1.5f);
                //dame
                //player.AttackSkill();
                int hp = 5;
                _currentHealth += hp;
                if (floatTextHpPrefab && _currentHealth > 0)
                {
                    ShowFloatingTextHp(hp.ToString());
                }
            }
            //skill3
            else if (_mana3 == maxMana3 && skillActive3)
            {
                
                player.onAttack = true;
                Instantiate(skill3, transform.position, Quaternion.identity);
                Instantiate(skill3,player.target.transform.position,Quaternion.identity);
                
                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                Debug.Log("attack/ranged/cast-fly");
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(15);
            }

        }

        private void UseSkill(Card card)
        {
            if (card.id == "skill2")
            {
                Skill2();
            }
            if (card.id == "skill3")
            {
                Skill3();
            }
        }
        public void Skill2()
        {
            player.onAttack = true;
            Instantiate(skill3, transform.position, Quaternion.identity);
            Instantiate(skill3,player.target.transform.position,Quaternion.identity);
                
            //animation
            _skeletonAnimation.timeScale = 0.5f;
            _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
            Debug.Log("attack/ranged/cast-fly");
            _mana3 = 0;
            //panel
            skillPanel.SetActive(true);
            Invoke(nameof(Disable), 2f);
            //dame
            player.Attack(15);
        
        }
        //call to button 
        public void Skill3()
        {
            if(!_skillAfterActive && !skillActiveMain)
            {
                cameraShake.Shake();
                thurder.Play();
                
                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(skill3, transform.position, Quaternion.identity);
                Instantiate(fxThunder,player.target.transform.position,Quaternion.identity);
                
                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                Debug.Log("attack/ranged/cast-fly");
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(15);
                skillActiveMain = true;
            }
        }
        void Disable()
        {
            skillPanel.SetActive(false);
            player.onAttack = false;
        }
        void Disable2()
        {
            skillPanel.SetActive(false);
            player.onAttack = false;
            int hp = 5;
            _currentHealth += hp;
            if (floatTextHpPrefab && _currentHealth > 0)
            {
                ShowFloatingTextHp(hp.ToString());
            }
        }
        void ShowFloatingText(string text)
        {
            var go = Instantiate(floatTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponentInChildren<TextMesh>().text = " - " + text + " Hp ";
            go.transform.Rotate(0, 180, 0);
        }
        void ShowFloatingTextHp(string text)
        {
            var go = Instantiate(floatTextHpPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponentInChildren<TextMesh>().text = " + " + text + " Hp ";
            go.transform.Rotate(0, 180, 0);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            hPBar.SetHealth(_currentHealth);
            Instantiate(fx, transform.position, Quaternion.identity);
       
        
            if (_currentHealth <= 0)
            {
                Die();
            }
       
            if (floatTextPrefab && _currentHealth > 0)
            {
                ShowFloatingText(damage.ToString());
            }
        }

        void Die()
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
