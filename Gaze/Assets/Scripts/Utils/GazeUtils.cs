using System;
using UnityEngine;
using TMPro;

public static class GazeUtils
{
    // public static TMP_Text textComponent;
    public static TMP_InputField inputField; // Reference to the TMP_InputField
    public static Vector3 cursorPosition;
    
    public static void PlaceCaretAtCursor(Vector3 cursorPosition, Camera uiCamera)
    {
        // transform to 2d local screen space
        
        // Ensure the input field is focused
        inputField.ActivateInputField();
        
        // Find the nearest character to the mouse position
        int charIndex = TMP_TextUtilities.FindNearestCharacter(
            inputField.textComponent, 
            cursorPosition, 
            uiCamera, 
            true);
        
        Debug.Log("charIndex: " + charIndex);

        // Place the caret at the closest character
        if (charIndex != -1)
        {
            inputField.caretPosition = charIndex;
        }
    }
}
