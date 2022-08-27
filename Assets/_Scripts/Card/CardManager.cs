using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Observer;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameManagerFlow _gameManagerFlow; 
    [SerializeField] private List<Card> _cardPool;
    [SerializeField] private GameObject _randomCardPanel;
    [SerializeField] private GameObject _playerCardPanel;
    [SerializeField] private List<Card> _randomCards;
    [SerializeField] private List<Card> _playerCards;

    private void Awake()
    {
        this.RegisterListener(EventID.OnWaveStart, (param) => CardStart());
        
        this.RegisterListener(EventID.OnSelectCard, (param) => SelectCard((Card)param));
        this.RegisterListener(EventID.OnUseCard, (param) => UseCard((Card)param));
        
        HideRandomPanel();
    }

    void CardStart()
    {
        if(_gameManagerFlow.waveLevel ==1)
        {
            GivePlayerCards();
        }
        else
        {
            ShowRandomPanel();
        }
    }

    public void GivePlayerCards()
    {
        _randomCardPanel.SetActive(false);

        //give player 3 cards
        int cardCount = 3;
        
        for (int i = 0; i < cardCount; i++)
        {
            var rd =Random.Range(0, _cardPool.Count);
            SelectCard(_cardPool[rd]);
        }
    }

    private void ShowRandomPanel()
    {
        _randomCardPanel.SetActive(true);
        
        //show player 3 cards
        int cardCount = 3;

        for (int i = 0; i < cardCount; i++)
        {
            var rd =Random.Range(0, _cardPool.Count);
            var card = Instantiate(_cardPool[rd], _randomCardPanel.transform);
            card.SetSelectable(false);
            card.SetInteractable(true);
            _randomCards.Add(card);
        }
    }
    
    private void HideRandomPanel()
    {
        _randomCardPanel.SetActive(false);

        foreach (var card in _randomCards)
        {
            Destroy(card.gameObject);
           
        }

        _randomCards.Clear();

    }
    
    private void SelectCard(Card card)
    {
        var myCard = Instantiate(card, _playerCardPanel.transform);
        myCard.SetSelectable(true);
        myCard.SetInteractable(true);
        _playerCards.Add(myCard);

        HideRandomPanel();
    }

    private void UseCard(Card card)
    {
        _playerCards.Remove(card);
       
    }

    
}