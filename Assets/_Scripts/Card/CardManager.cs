using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<Card> _cardPool;
    [SerializeField] private GameObject _selectCardPanel;
    [SerializeField] private GameObject _playerCardPanel;
    [SerializeField] private List<Card> _playerCards;
        
        
    // Start is called before the first frame update
    void Start()
    {
        OnGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStart()
    {
        _selectCardPanel.SetActive(false);
        
        //give player 3 cards
        for (int i = 0; i < 3; i++)
        {
            var rd =Random.Range(0, _cardPool.Count);
            Instantiate(_cardPool[rd].gameObject, _playerCardPanel.transform);
        }
        
        
    }
}
