using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using UnityEngine.UI;

public class CardCollection : MonoBehaviour
{
    [SerializeField] public static int s_maxCard = 3;
    [SerializeField] public static List<CardID> s_selectedCards;
    
    [SerializeField] private GameObject _cardHolder;
    [SerializeField] private CardDataConfig _cardDataConfig;
    [SerializeField] private Card _cardPref;
    
    [SerializeField] private Button _startBtn;

    private void Awake()
    {
        this.RegisterListener(EventID.OnSelectCard, (param) => SelectCard((CardID)param));
        s_selectedCards = new List<CardID>();
        
        _startBtn.enabled = s_selectedCards.Count == s_maxCard;
        
    }

    

    // Start is called before the first frame update
    void Start()
    {
        ShowCollection();
    }

    public void ShowCollection()
    {
        var cardList = _cardDataConfig.list;
        foreach (var cardId in cardList)
        {
            var myCard = Instantiate(_cardPref,  _cardHolder.transform);
            myCard.SetData((cardId.id));
            myCard.SetInteractable(true);
            myCard.SetIsInMenuMode();
            myCard.SetBlackCover(true);
        }
    }
    
    private void SelectCard(CardID cardID)
    {
        if (s_selectedCards.Contains(cardID))
        {
            s_selectedCards.Remove(cardID);
        }
        else
        {
           
                s_selectedCards.Add(cardID);
            
            
        }
        
        _startBtn.enabled = s_selectedCards.Count == s_maxCard;
    }

}
