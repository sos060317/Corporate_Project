using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
    [SerializeField] private Text levelText;

    public void TextChange(string text)
    {
        levelText.text = text;
    }
}