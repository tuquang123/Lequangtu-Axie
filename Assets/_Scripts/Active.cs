using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    public void StartActive()
    {
        player.SetActive(true);
    }
    public void StartActiveFalse()
    {
        player.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
