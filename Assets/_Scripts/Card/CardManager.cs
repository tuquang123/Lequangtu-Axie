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
    [SerializeField] private List<CardID> _cardPool;
    [SerializeField] private Card _cardPref;
    [SerializeField] private GameObject _randomCardPanel;
    [SerializeField] private GameObject _playerCardPanel;
    [SerializeField] private List<Card> _randomCards;
    [SerializeField] private List<Card> _playerCards;

    private void Awake()
    {
        this.RegisterListener(EventID.OnWaveStart, (param) => CardStart());
        
        this.RegisterListener(EventID.OnSelectCard, (param) => SelectCard((CardID)param));
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


        if (CardCollection.s_selectedCards == null)
        {
            for (int i = 0; i < cardCount; i++)
            {
                var rd =Random.Range(0, _cardPool.Count);
                SelectCard((CardID)rd);
            }
        }
        else
        {
            foreach (var cardID in CardCollection.s_selectedCards)
            {
                SelectCard((CardID)cardID);
            }
        }
        

        
    }

    private void ShowRandomPanel()
    {
        this.PostEvent(EventID.OnShowCard);
        _randomCardPanel.SetActive(true);
        
        //show player 3 cards
        int cardCount = 3;

        for (int i = 0; i < cardCount; i++)
        {
            var rd =Random.Range(0, _cardPool.Count);
            var card = Instantiate(_cardPref, _randomCardPanel.transform);
            card.SetData((CardID)rd);
            card.SetSelectable(false);
            card.SetInteractable(true);
            card.SetBlackCover(false);
            _randomCards.Add(card);
        }
    }
    
    private void HideRandomPanel()
    {
        _randomCardPanel.SetActive(false);
        this.PostEvent(EventID.OnStopShowCard);

        foreach (var card in _randomCards)
        {
            Destroy(card.gameObject);
           
        }

        _randomCards.Clear();

    }
    
    private void SelectCard(CardID id)
    {
        var myCard = Instantiate(_cardPref, _playerCardPanel.transform);
        myCard.SetData(id);
        myCard.SetSelectable(true);
        myCard.SetInteractable(true);
        myCard.SetBlackCover(false);
        _playerCards.Add(myCard);

        HideRandomPanel();
    }

    private void UseCard(Card card)
    {
        _playerCards.Remove(card);
       
    }

    
}