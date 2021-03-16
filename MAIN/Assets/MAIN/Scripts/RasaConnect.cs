using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Reflection;
using System;
using UnityEngine.UI;

public class RasaConnect : MonoBehaviour
{

    //get voice input
    public SampleSpeechToText get_voice;

    public InputField field;

    public Text responseText;

    //observer pattern initialise
    public EventHandler<to_speech> on_response;
    public class to_speech : EventArgs
    {
        public string message;
    }

    //server webhook location
    private const string rasa_url = "https://72a2024dd3fe.ngrok.io/webhooks/rest/webhook";

    // Rasa server requires requests in this format
    public class post_json
    {
        public string message;
        public string sender;
    }

    [Serializable]
    // A class to extract multiple json objects nested inside a value
    public class recieve_json
    {
        public message_format[] messages;
    }

    [Serializable]
    // A class to extract a single message returned from the bot
    public class message_format
    {
        public string recipient_id;
        public string text;
        public string image;
        public string attachemnt;
        public string button;
        public string element;
        public string quick_replie;
    }

    private void Start()
    {
        //voice input activates prepare-message
        get_voice.on_speech += prepare_message_voice;
    }

    //user sends message with voice
    public void prepare_message_voice(object sender, SampleSpeechToText.to_rasa voice_in)
    {
        // Create a json object from user message
        post_json message_json = new post_json
        {
            sender = "user",
            message = voice_in.message_for_rasa
        };

        string json = JsonUtility.ToJson(message_json);

        // Create a post request with the data to send to Rasa server
        StartCoroutine(to_rasa(rasa_url, json));
    }

    //user sends message with text
    public void prepare_message_text()
    {
        // Create a json object from user message
        post_json message_json = new post_json
        {
            sender = "user",
            message = field.text
        };

        string json = JsonUtility.ToJson(message_json);

        // Create a post request with the data to send to Rasa server
        StartCoroutine(to_rasa(rasa_url, json));
    }

    private IEnumerator to_rasa(string url, string jsonBody)
    {
        //create networking reference
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        //encode json, and upload it to the server
        byte[] rawBody = new UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(rawBody);

        //prepare buffer for response
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        parse_response(request.downloadHandler.text);
    }

    public void parse_response(string response)
    {
        // Deserialize response recieved from the bot
        recieve_json recieve_messages = JsonUtility.FromJson<recieve_json>("{\"messages\":" + response + "}");

        // show message based on message type on UI
        foreach (message_format message in recieve_messages.messages)
        {
            FieldInfo[] fields = typeof(message_format).GetFields();
            foreach (FieldInfo field in fields)
            {
                string data = null;

                // extract data from response in try-catch for handling null exceptions
                try
                {
                    data = field.GetValue(message).ToString();
                }
                catch (NullReferenceException) { }

                // print data
                if (data != null && field.Name != "recipient_id")
                {
                    //execute observer pattern
                    on_response?.Invoke(this, new to_speech { message = data });

                    //bot response in text
                    responseText.text = data;
                }
            }
        }
    }
}