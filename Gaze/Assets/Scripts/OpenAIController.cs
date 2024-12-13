using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenAIController : GenericSingleton<OpenAIController>
{

    private OpenAIAPI api;
    private List<ChatMessage> messages;

    // Start is called before the first frame update
    void Start()
    {
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User));
        StartConversation();
    }

    private void StartConversation()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, "Summarize the keypoints of this text.")
        };

    }

    public async void GetResponse()
    {
        // Check the status of the input field
        if (GazeUtils.inputField == null)
        {
            return;
        }
        
        TMP_InputField inputField = GazeUtils.inputField;
        
        if (inputField.text.Length < 1)
        {
            return;
        }
        
        // Fill the user message from the input field
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = inputField.text;
        
        // if (userMessage.Content.Length > 100)
        // {
        //     // Limit messages to 100 characters
        //     userMessage.Content = userMessage.Content.Substring(0, 100);
        // }
        
        Debug.Log(string.Format("{0}: {1}", userMessage.rawRole, userMessage.Content));

        // Add the message to the list
        messages.Add(userMessage);

        // // Update the text field with the user message
        // textField.text = string.Format("You: {0}", userMessage.Content);
        
        // Apply the new color
        inputField.textComponent.color = Color.gray;

        // Send the entire chat to OpenAI to get the next message
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.9,
            MaxTokens = 4096,
            Messages = messages
        });

        // Get the response message
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        // Add the response to the list of messages
        messages.Add(responseMessage);

        // Update the text field with the response
        inputField.textComponent.color = Color.black;
        inputField.text = string.Format(responseMessage.Content);

        
    }
}