using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCard : MonoBehaviour
{
    [SerializeField] private List<GameObject> listCard;
    [SerializeField] private Transform transform;
    [SerializeField] private GameObject panelSpawnItem;
    [SerializeField] private int cardnumber;

    private void Start()
    {
        panelSpawnItem.SetActive(false);
        SpawnItem();
    }

    private void SpawnItem()
    {
        bool[] checkSpawnItem = new bool[listCard.Count];
        for (int i = 0; i < checkSpawnItem.Length; i++)
        {
            checkSpawnItem[i] = false;
        }

        for (int i = 0; i < cardnumber; i++)
        {
            var randomItem = Random.Range(0, checkSpawnItem.Length);
            if (checkSpawnItem[randomItem] == true)
            {
                i--;
                continue;
            }

            checkSpawnItem[randomItem] = true;
            var spawnItemUI = Instantiate(listCard[randomItem], transform);
        }
    }
}
