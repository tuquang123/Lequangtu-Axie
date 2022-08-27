using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ACard : MonoBehaviour
{
    [SerializeField] int m_id;
    [SerializeField] private TMP_Text m_name;
    [SerializeField] private TMP_Text m_description;
    [SerializeField] private Button m_button;

    public void GetInt(int id)
    {
        m_id = id;
    }

    public void GetName(string name)
    {
        m_name.text = name;
    }

    public void GetDescription(string description)
    {
        m_description.text = description;
    }

    public void GetSprite(Sprite sprite)
    {
        m_button.image.sprite = sprite;
    }
}
