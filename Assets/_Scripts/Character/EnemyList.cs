using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using Observer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyList : MonoBehaviour
{
    //public Player playerClass;
    public GameObject player;
    public GameObject loading;
    //public GameObject panelWin;
    public static EnemyList enemyList;
    public UiManager uiManager;
    bool win;

    [SerializeField] private GameManagerFlow _gameManagerFlow;

    [System.Serializable]
    public class EnemyWave
    {
        public List<GameObject> enemies;
    }
    
    public List<Transform> objects = new List<Transform>();

    [SerializeField] private List<EnemyWave> enemyPref;
    private void Awake()
    {
        enemyList = this;
    }

    

    private void SpawnEnemy()
    {
        if (enemyPref.Count > 0)
        {
            var mylist = enemyPref[0];
           
            foreach (var enemy in mylist.enemies)
            {
                enemy.SetActive(true);
                objects.Add(enemy.transform);
            }
            enemyPref.RemoveAt(0);
            
            _gameManagerFlow.IncreaseWaveLevel();
            return;
        }

        if (enemyPref.Count == 0)
        {
            Debug.Log("On Win");
        }
    }
    void RemoveList()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Transform obj = this.objects[i];
            if (obj.gameObject.activeSelf) continue;
            objects.Remove(obj);
            
        }
    }
    private void Update()
    {
        if (objects.Count == 0)
        {
            SpawnEnemy();
            //playerClass.Win();
           
            // if (!win)
            // {
            //     Invoke("Active", 2f);
            //     //uiManager.PanelFadeIn(2);
            // }
            // win = true;
            //Invoke(nameof(ActiveLoading), 10f);
        }
    }
    public void Active()
    {
        uiManager.PanelFadeIn(2);
        player.SetActive(false);
    }

    public void ActiveLoading()
    {
        loading.SetActive(true);
        Invoke(nameof(ChangeScene),2f);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
    private void LateUpdate()
    {
        RemoveList();
    }
}
