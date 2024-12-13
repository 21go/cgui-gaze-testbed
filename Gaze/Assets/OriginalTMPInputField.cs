using System;
using TMPro;
using UnityEngine;

public class OriginalTMPInputField : MonoBehaviour
{
    public string originalText;
    
    public void Start()
    {
        originalText = GetComponent<TMP_InputField>().text;
    }
}