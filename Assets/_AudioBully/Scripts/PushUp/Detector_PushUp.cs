using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_PushUp : VoiceReactor
{
    public float pushSpeed;
    public TMPro.TMP_Text number;
    private float count = 0;
    public MoreMountains.Feedbacks.MMF_Player mmf_NumberPopUp;
    private bool isPushUp = false;
    public float timeToWin = 60f;
    private bool isGameResultOut = false;
    public TMPro.TMP_Text timer;

    void Start()
    {
        InitializeReactor();
    }

    void Update()
    {
        AIMateMovement();
        PushUpStateControl();
        if(!isGameResultOut) GameDetermination();
    }

    void AIMateMovement()
    {
        float loudness = GetLoudness();
        if (loudness < threshold_Default) loudness = 0;

        if (loudness != 0) transform.position += Vector3.up * pushSpeed * Time.deltaTime;
    }

    private void PushUpStateControl()
    {
     

        if (transform.position.y < 1.1f && isPushUp) { isPushUp = false; return; }
        else if (transform.position.y > 2.9f && !isPushUp) 
        { 
            isPushUp = true; 
            count++; 
            number.text = count.ToString(); 
            mmf_NumberPopUp.PlayFeedbacks();
        }
    }

    private void GameDetermination()
    {
        timeToWin -= Time.deltaTime;
        timer.text = Mathf.CeilToInt(timeToWin).ToString();

        if (count >= 10)
        {
            FindObjectOfType<GameEnd>().GameWin(3);
            isGameResultOut = true;
        }

        if (timeToWin<0)
        {
            FindObjectOfType<GameEnd>().GameLose(2);
            isGameResultOut = true;
        }
    }
}
