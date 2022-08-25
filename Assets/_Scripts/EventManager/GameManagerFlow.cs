using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class GameManagerFlow : MonoBehaviour
{
    private void Start()
    {
        this.PostEvent(EventID.OnStartFirstBattle);
    }
    
    public void OnStartFirstBattle()
    {
        this.PostEvent(EventID.OnStartFirstBattle);
    }

    public void OnStartBattle()
    {
        this.PostEvent(EventID.OnStartBattle);
    }
}
