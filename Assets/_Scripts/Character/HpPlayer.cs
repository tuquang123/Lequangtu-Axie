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
        public Enemy              enemy;
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
        public GameObject fxExplosive;
        public GameObject fxPlazma;
        public GameObject floatTextPrefab;
        public GameObject floatTextHpPrefab;
        public GameObject deathEffect;
        public GameObject fx;

        //VFX Skill
        public GameObject vfxBumm;
        public GameObject vfxDame;
        public GameObject vfxHp;
        public GameObject vfxLand;
        public GameObject vfxLight;
        public GameObject vfxRain;
        public GameObject vfxShield;
        public GameObject vfxThunder;
        public GameObject vfxWater;
        public GameObject vfxWinter;

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
        }

        private void UseSkill(Card card)
        {
            if (card.id == CardID.Bumm)
            {
                Skill1Bumm();
            }
            if (card.id == CardID.Dame)
            {
                Skill2Dame();
            }
            if (card.id == CardID.Hp)
            {
                Skill3Hp();
            }
            if (card.id == CardID.Land)
            {
                Skill4Land();
            }
            if (card.id == CardID.Light)
            {
                Skill5Light();
            }
            if (card.id == CardID.Rain)
            {
                Skill6Rain();
            }
            if (card.id == CardID.Shield)
            {
                Skill7Shield();
            }
            if (card.id == CardID.Thunder)
            {
                Skill8Thunder();
            }
            if (card.id == CardID.Water)
            {
                Skill9Water();
            }
            if (card.id == CardID.Winter)
            {
                Skill10Winter();
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

        // Script Skill                
        public void Skill1Bumm()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxBumm, player.target.transform.position, Quaternion.identity);

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(20);
                skillActiveMain = true;
        }
        public void Skill2Dame()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;

                Instantiate(vfxDame, transform.position, Quaternion.identity);
                player.damage += 5;

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
        }
        public void Skill3Hp()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;

                Instantiate(vfxHp, transform.position, Quaternion.identity);
                int hp = 50;
                _currentHealth += hp;

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
        }
        public void Skill4Land()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxLand, player.target.transform.position, Quaternion.identity);

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(20);
                skillActiveMain = true;
        }
        public void Skill5Light()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxLight, player.target.transform.position, Quaternion.identity);

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(25);
                skillActiveMain = true;
        }
        public void Skill6Rain()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxRain, player.target.transform.position, Quaternion.identity);

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(25);
                skillActiveMain = true;
        }
        public void Skill7Shield()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxShield, transform.position, Quaternion.identity);
                enemy.damage -= 3;

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
        }
        public void Skill8Thunder()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxThunder, player.target.transform.position, Quaternion.identity);

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(25);
                skillActiveMain = true;
        }
        public void Skill9Water()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxWater, player.target.transform.position, Quaternion.identity);

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(20);
                skillActiveMain = true;
        }
        public void Skill10Winter()
        {
                cameraShake.Shake();
                thurder.Play();

                activeWhenButtonCall.SetActive(true);
                player.onAttack = true;
                Instantiate(vfxWinter, player.target.transform.position, Quaternion.identity);

                //animation
                _skeletonAnimation.timeScale = 0.5f;
                _skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false); // shot
                _mana3 = 0;
                //panel
                skillPanel.SetActive(true);
                Invoke(nameof(Disable), 2f);
                //dame
                player.Attack(30);
                skillActiveMain = true;
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