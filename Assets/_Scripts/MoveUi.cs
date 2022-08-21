using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class MoveUi : MonoBehaviour
{
   public int value = 0;
   public int value2 = 0;
   public GameObject obj;
   public void Move()
   {
      obj.transform.DOMoveX(value, 0.8f).SetEase(Ease.OutBack);
        invokeMove();
   }

   public void ComeBack()
   {
      obj.transform.DOMoveX(value2, 0.8f).SetEase(Ease.OutBack);
   }
   public void invokeMove()
    {
        Invoke("Destroy", 2f);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
