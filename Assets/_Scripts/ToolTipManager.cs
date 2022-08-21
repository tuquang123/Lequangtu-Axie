using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
   public static ToolTipManager instance;
   public Text text;

   private void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      Cursor.visible = true;
      gameObject.SetActive(false);
   }

   private void Update()
   {
      //transform.position = Input.mousePosition;
   }

   public void SetAndShowToolTip(string message)
   {
      gameObject.SetActive(true);
      text.text = message;
   }
   public void HideToolTip()
   {
      gameObject.SetActive(false);
      text.text = String.Empty;
   }
   
}
