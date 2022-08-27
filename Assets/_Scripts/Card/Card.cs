using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Observer;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler
{
    [SerializeField] public string id;
    [SerializeField] private bool _isSelect;
    [SerializeField] private bool _isInteractable;

    [SerializeField] private CardDataConfig _cardDataConfig;
    [SerializeField] private TMP_Text m_name;
    [SerializeField] private TMP_Text m_description;
    public void SetData(CardID cardID)
    {
        var cardData = _cardDataConfig.GetValueFromKey(cardID);
        m_name.text = cardData.name;
        m_description.text = cardData.description;
        this.GetComponent<Image>().sprite = cardData.sprite;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isInteractable) return;

        Debug.Log("on hover");
        

        //effect go bling bling
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        
    
        
        Debug.Log("exit hover");

        

        //stop effect
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        
        if (!_isSelect)
        {
            SelectCard();
            return;
        }

        if (_isSelect)
        {
            UseSkill();
            return;

        }
    }

    private void EnableCard(bool isEnable)
    {
        _isInteractable = isEnable;
    }

    private void SelectCard()
    {
        Debug.Log("select card");
        _isSelect = true;
        this.PostEvent(EventID.OnSelectCard, this);
    }

    private void UseSkill()
    {
        Debug.Log("use skill");
        this.PostEvent(EventID.OnUseCard, this);
        Destroy(this.gameObject);
    }

    public void SetSelectable(bool isSelectable)
    {
        _isSelect = isSelectable;
    }
    public void SetInteractable(bool isInteractables)
    {
        _isInteractable = isInteractables;
    }
}