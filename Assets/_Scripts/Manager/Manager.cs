using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject Started;
    public GameObject Win;
    public GameObject Loss;

    private void Start()
    {
        Invoke("StartUi", 1f);
    }
    void StartUi()
    {
        Started.SetActive(false);
    }

}
