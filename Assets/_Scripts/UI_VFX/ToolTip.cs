using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public string message;

    public void OnMouseEnter()
    {
        ToolTipManager.instance.SetAndShowToolTip(message);
    }

    private void OnMouseExit()
    {
        ToolTipManager.instance.HideToolTip();
    }
}
