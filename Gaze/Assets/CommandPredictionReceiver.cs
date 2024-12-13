using NetMQ.Sockets;
using UnityEngine;
using NetMQ;
using System;
using AsyncIO;
using UnityEngine.UI;

public class CommandPredictionReceiver : GenericSingleton<CommandPredictionReceiver>
{
    [Header("Command Prediction Receiver Port")]
    public string CommandReceiverPort = "tcp://localhost:5557";
    // public string CommandReceiverTopic = "Hotkey";
    
    private SubscriberSocket subscriber;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            ForceDotNet.Force();
            subscriber = new SubscriberSocket();
            subscriber.Connect(CommandReceiverPort); 
            subscriber.Subscribe("Hotkey");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there is a message to receive
        if (subscriber.TryReceiveFrameString(out string message))
        {
            // Process the received message
            Debug.Log($"Received message: {message}");

            // Optionally, split the message into topic and content
            string[] parts = message.Split(':');
            string topic = parts[0];
            string content = parts.Length > 1 ? parts[1] : "";

            // Handle the message based on the topic
            if (topic == "Hotkey")
            {
                if (content == "copy")
                {
                    Debug.Log("Performing Copy Action");
                    if(GazeUtils.inputField != null)
                    {
                        // ColorBlock cb = GazeUtils.inputField.colors;
                        //
                        //
                        // cb.normalColor = Color.green;
                        //
                        // GazeUtils.inputField.colors = cb;
                        
                        OpenAIController.Instance.GetResponse();  
                    }
                }
                else if (content == "paste")
                {
                    Debug.Log("Performing Paste Action");
                    if(GazeUtils.inputField != null)
                    {
                        // ColorBlock cb = GazeUtils.inputField.colors;
                        //
                        // cb.normalColor = Color.red;
                        //
                        // GazeUtils.inputField.colors = cb;

                        GazeUtils.inputField.text = GazeUtils.inputField.gameObject
                            .GetComponent<OriginalTMPInputField>().originalText;
                    }
                }
            }
        }
    }
    
    // OnDestroy is called when the script is destroyed
    void OnDestroy()
    {
        // Clean up the ZMQ socket
        if (subscriber != null)
        {
            subscriber.Close();
            subscriber.Dispose();
        }
        NetMQConfig.Cleanup();
        Debug.Log("ZMQ Subscriber closed and cleaned up.");
    }
}
