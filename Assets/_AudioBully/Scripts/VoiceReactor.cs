using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class VoiceReactor : MonoBehaviour
{
    private M_VoiceDetection detector;
    public float loudnessSensibility = 100;
    public float threshold_Default = 0.1f;
    public Action Screaming;

    public void InitializeReactor()
    {
        detector = FindObjectOfType<M_VoiceDetection>();
        //Screaming += FindObjectOfType<ScreamingAnimController>().Talk;
    }

    public float GetLoudness()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        //Screaming(loudness);
        return loudness;
    }
}
