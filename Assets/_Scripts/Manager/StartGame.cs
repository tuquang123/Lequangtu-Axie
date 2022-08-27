using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button m_button;

    private void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(Ingame);
    }

    private void Ingame()
    {
        Application.LoadLevel("SelectCard");
    }
}


