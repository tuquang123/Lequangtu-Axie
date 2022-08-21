using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class HpEnemy : MonoBehaviour
    {
        public GameObject floatTextPrefab;
        public GameObject deathEffect;
        public GameObject fx;

        public HPBar hPBar;
        public Text textHp;
        public int health = 10;
        int _currentHealth;
       
        private void FixedUpdate()
        {
            textHp.text = _currentHealth.ToString();
        }
        void ShowFloatingText(string text)
        {
            var go = Instantiate(floatTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponentInChildren<TextMesh>().text = " - " + text + " Hp ";
        }
        private void Start()
        {
            _currentHealth = health;
            hPBar.SetMaxHealth(health);
        }
        public void TakeDamage(int damage)
        {
            //dame 
            _currentHealth -= damage;

            //hp
            hPBar.SetHealth(_currentHealth);

            //fx hit
            Instantiate(fx, transform.position, Quaternion.identity);

            //die
            if (_currentHealth <= 0)
            {
                Die();
            }

            //popup text
            if (floatTextPrefab && _currentHealth > 0)
            {
                ShowFloatingText(damage.ToString());
            }
        }
       
        void Die()
        {
            
            transform.gameObject.SetActive(false);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
