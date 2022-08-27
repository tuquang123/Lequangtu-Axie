using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class GameManagerFlow : MonoBehaviour
{
  
    public int waveLevel;

    private void Start()
    {
        ResetWaveLevel();
    }


    public void ResetWaveLevel()
    {
        waveLevel = 1;
        
        this.PostEvent(EventID.OnWaveStart);
    }
    
    public void IncreaseWaveLevel()
    {
        waveLevel++;
        this.PostEvent(EventID.OnWaveStart);
    }
    
    
    
}
