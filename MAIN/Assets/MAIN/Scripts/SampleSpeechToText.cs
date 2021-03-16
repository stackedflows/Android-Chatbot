using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using System;

public class SampleSpeechToText : MonoBehaviour
{

    public RasaConnect rasa;

    public InputField inputText;
    public float pitch;
    public float rate;

    public EventHandler<to_rasa> on_speech;
    public class to_rasa : EventArgs
    {
        public string message_for_rasa;
    }

    void Start()
    {
        Setting("en-US");
        SpeechToText.instance.onResultCallback = OnResultSpeech;

        rasa.on_response += OnClickSpeak;
    }

    public void StartRecording()
    {
#if UNITY_EDITOR
#else
        SpeechToText.instance.StartRecording("Speak any");
#endif
    }

    public void StopRecording()
    {
#if UNITY_EDITOR
        OnResultSpeech("Not support in editor.");
#else
        SpeechToText.instance.StopRecording();
#endif
    }
    void OnResultSpeech(string _data)
    {
        on_speech?.Invoke(this, new to_rasa { message_for_rasa = _data });
    }

    public void OnClickSpeak(object sender, RasaConnect.to_speech say_this)
    {
        TextToSpeech.instance.StartSpeak(say_this.message);
    }

    public void  OnClickStopSpeak()
    {
        TextToSpeech.instance.StopSpeak();
    }

    public void Setting(string code)
    {
        TextToSpeech.instance.Setting(code, pitch, rate);
        SpeechToText.instance.Setting(code);
    }
}
