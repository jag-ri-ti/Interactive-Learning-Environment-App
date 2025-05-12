using System.Collections.Generic;
using UnityEngine;

public class TTSManager : MonoBehaviour
{
    private static AndroidJavaObject ttsManager;    
    private bool isSpeaking = false;      

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                ttsManager = new AndroidJavaObject("com.yourcompany.texttospeech.TextToSpeechManager", activity);
            }
        }        
    }

    public void Speak(string text)
    {
        if (ttsManager != null)
        {
            ttsManager.Call("speak", text);
            isSpeaking = true;            
        }        
    }

    public void Stop()
    {
        if (ttsManager != null)
        {
            ttsManager.Call("stop");
            isSpeaking = false;            
        }        
    }

    void Update()
    {
        // Check if TTS has finished speaking (assuming the TTSManager Java class can indicate this)
        if (isSpeaking && !ttsManager.Call<bool>("isSpeaking")) // Replace with actual check if available
        {
            isSpeaking = false;            
        }        
    }

    void OnApplicationQuit()
    {
        if (ttsManager != null)
        {
            ttsManager.Call("shutdown");
        }
    }
}
