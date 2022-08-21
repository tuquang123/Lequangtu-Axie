using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UiTop : MonoBehaviour
{
    public int value;
    public List<RectTransform> lstLeftBt;
    public List<RectTransform> lstRightBt;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Display(0.3f,0.1f));
    }

    IEnumerator Display(float start, float display)
    {
        yield return new WaitForSeconds(start);
        for(int i = 0; i < lstLeftBt.Count; i++)
        {
            lstLeftBt[i].DOMoveY(value, 0.6f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(display);
        }
        for(int i = 0; i < lstRightBt.Count; i++)
        {
            lstRightBt[i].DOMoveY(value, 0.6f);
            yield return new WaitForSeconds(display);
        }
    }
    
}
