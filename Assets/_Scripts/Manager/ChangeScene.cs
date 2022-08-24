using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class ChangeScene : MonoBehaviour
    {
        public GameObject player;
        public GameObject loading;
    
        public void Change1(int index)
        {
            player.SetActive(false);
            loading.SetActive(true);
            //SceneManager.LoadScene(1);
            //Invoke(Next(index),2f);
            StartCoroutine(g2(index));

        }
        void Next(int index)
        {
            SceneManager.LoadScene(index);
        }

        IEnumerator g2(int index)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(index);
        }
    }
}
