using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class ChangeScene : MonoBehaviour
    {
        public static int gameLevel;
        public GameObject player;

        public void SetGameLevel(int level)
        {
            gameLevel = level;
        }
        
        public void LoadIntoSelectCardScene()
        {
            player.SetActive(false);
            g2(1);
        }

        public void LoadIntoLevelScene()
        {
            player.SetActive(false);
            g2(gameLevel+1);
        }
        
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
