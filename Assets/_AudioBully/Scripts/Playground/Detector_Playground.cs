using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Detector_Playground : VoiceReactor
{
    public float runningSpeed;
    private bool isResultOut = false;

    void Awake()
    {
        InitializeReactor();
    }

    void Update()
    {
        AIMateMovement();
        if(!isResultOut) RaceResultDetermination();
    }

    void AIMateMovement()
    {
        float loudness = GetLoudness();
        if (loudness < threshold_Default) loudness = 0;

        if (loudness == 0) transform.position += Vector3.left * runningSpeed * Time.deltaTime;
        else transform.position -= Vector3.left * runningSpeed * Time.deltaTime;
    }

    void RaceResultDetermination()
    {
        if (transform.position.x < -10.3f)
        {
            isResultOut = true;
            FindObjectOfType<GameEnd>().GameLose(1);
        }

        if (transform.position.x > 10.3f)
        {
            isResultOut = true;
            FindObjectOfType<GameEnd>().GameWin(2);
        }
    }
}
