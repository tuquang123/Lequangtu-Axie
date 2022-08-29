using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f);
    }
    public void Destroy()
    {
        Destroy(gameObject, .7f);
    }

    
}
