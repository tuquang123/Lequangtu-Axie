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
    [SerializeField] public CardID id;
    [SerializeField] private bool _isSelect;
    [SerializeField] private bool _isInteractable;
    [SerializeField] private bool _isInMenuMode;
    [SerializeField] private bool _isSelectInMenuMode;

    [SerializeField] private CardDataConfig _cardDataConfig;
    [SerializeField] private TMP_Text m_name;
    [SerializeField] private TMP_Text m_description;
    [SerializeField] private GameObject _blackCover;
    [SerializeField] private GameObject _checkMark;
    public void SetData(CardID cardID)
    {
        var cardData = _cardDataConfig.GetValueFromKey(cardID);
        id = cardID;
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

        if (_isInMenuMode)
        {
            SelectInMenuMode();
            return;
        }
        
        
        if (!_isSelect)
        {
            SelectCard();
            return;
        }

        if (_isSelect)
        {
            UseSkill();
            SetInteractable(false);
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
        this.PostEvent(EventID.OnSelectCard, this.id);
    }

    private void UseSkill()
    {
        var scale = this.gameObject.transform.localScale;
        var sequence = DOTween.Sequence()
                .Append(this.gameObject.transform.DOScale(scale * 1.3f, 1.5f))
                .SetLoops(1);
        //this.gameObject.transform.DOScale(scale, time);

        Debug.Log("use skill");
        this.PostEvent(EventID.OnUseCard, this);

        GameObject game = this.gameObject;

        StartCoroutine(MoveCard(game));
    }

    IEnumerator MoveCard( GameObject mgameObject)
    {
        yield return new WaitForSeconds(.6f);
        Destroy(mgameObject);

    }


    public void SetSelectable(bool isSelectable)
    {
        _isSelect = isSelectable;
    }
    public void SetInteractable(bool isInteractables)
    {
        _isInteractable = isInteractables;
    }

    public void SetIsInMenuMode()
    {
        _isInMenuMode = true;
    }
    
    public void SetBlackCover(bool isCover)
    {
        _blackCover.SetActive(isCover);
    }
    
    public void SetCheckMark(bool isMark)
    {
        _checkMark.SetActive(isMark);
    }
    
    private void SelectInMenuMode()
    {
        Debug.Log("click");
        
        
        if (!_isSelectInMenuMode && CardCollection.s_selectedCards.Count >= CardCollection.s_maxCard)
        {
            return;
        }
        
        _isSelectInMenuMode = !_isSelectInMenuMode;
        SetBlackCover(!_isSelectInMenuMode);
        SetCheckMark(_isSelectInMenuMode);
        this.PostEvent(EventID.OnSelectCard, this.id);

    }

}