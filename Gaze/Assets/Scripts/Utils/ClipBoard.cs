using TMPro;
using UnityEngine;

public static class ClipBoard
{
    public static string text = "aaaa";
    
    public static void CopyToClipboard(string text2copy)
    {
        text = text2copy;
    }
    
    public static void PasteFromClipboard(TMP_InputField inputField, int charIndex)
    {
        Debug.Log(text);
        inputField.text = inputField.text.Insert(charIndex, text);
    }
}
