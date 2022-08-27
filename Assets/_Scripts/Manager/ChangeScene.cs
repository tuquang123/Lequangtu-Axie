using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class ChangeScene : MonoBehaviour
    {
        public GameObject player;
    
        public void Change1(int index)
        {
            player.SetActive(false);
            g2(index);
        }
        void Next(int index)
        {
            SceneManager.LoadScene(index);
        }

        private void g2(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}
