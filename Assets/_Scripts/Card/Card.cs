using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler
{
    [SerializeField] private string id;
    [SerializeField] private bool _isSelect;
    [SerializeField] private bool _isInteractable;
    

    private void Start()
    {
        _isSelect = false;
        _isInteractable = true;
     
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
            Debug.Log("select card");
            _isSelect = true;

            return;
        }

        if (_isSelect)
        {
            Debug.Log("use skill");

            //dung skill
            // animattion
            //destroy
        }
    }

    private void EnableCard(bool isEnable)
    {
        _isInteractable = isEnable;
    }
}
