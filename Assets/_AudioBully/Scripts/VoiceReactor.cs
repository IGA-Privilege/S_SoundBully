using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoiceReactor : MonoBehaviour
{
    private M_VoiceDetection detector;
    public float loudnessSensibility = 100;
    public float threshold_Default = 0.1f;

    protected void InitializeReactor()
    {
        detector = FindObjectOfType<M_VoiceDetection>();
    }

    protected float GetLoudness()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        return loudness;
    }
}
