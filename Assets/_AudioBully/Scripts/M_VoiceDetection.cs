using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_VoiceDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;
    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public float GetLoudnessFromAudioClip(int clipPosition,AudioClip clip)
    {
        int startPos = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPos);
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }

    public void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }
}
