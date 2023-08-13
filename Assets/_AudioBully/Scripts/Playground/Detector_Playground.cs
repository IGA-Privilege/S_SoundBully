using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_Playground : VoiceReactor
{
    public float runningSpeed;

    void Start()
    {
        InitializeReactor();
    }

    void Update()
    {
        AIMateMovement();
    }

    void AIMateMovement()
    {
        float loudness = GetLoudness();
        if (loudness < threshold_Default) loudness = 0;

        if (loudness == 0) transform.position += Vector3.left * runningSpeed * Time.deltaTime;
        else transform.position -= Vector3.left * runningSpeed * Time.deltaTime;
    }
}
