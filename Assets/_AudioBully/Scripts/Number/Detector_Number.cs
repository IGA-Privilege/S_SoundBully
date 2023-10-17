using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_Number : VoiceReactor
{
    float tempLoudness = 0;
    float totalLoudness = 0;
    public float timeSpan = 0.6f;
    float timer;
    bool isShouting = false;
    public bool isShoutBegin { set { isShouting=value; } }

    void Start()
    {
        timer = timeSpan;
        InitializeReactor();
    }

    private void Update()
    {
        if (isShouting) ShoutPeriodRecord();
    }

    public int GetShoutedLevel()
    {
        tempLoudness = GetLoudness();
        int loudnessLevel = 0;
        if (tempLoudness < threshold_Default) tempLoudness = 0;
        if (tempLoudness != 0) {
            loudnessLevel = Mathf.RoundToInt(tempLoudness);
            loudnessLevel = 3;
            //loudnessLevel = loudnessLevel switch
            //{
            //    < 30 => 1,
            //    >= 30 and < 70 => 2,
            //    _ => 3
            //};
        }
        return loudnessLevel;
    }

    public void ShoutPeriodRecord()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Debug.Log("B: " + totalLoudness);
            totalLoudness = totalLoudness / 200;
            Debug.Log("T: " + totalLoudness);
            int intLoudness = Mathf.RoundToInt(totalLoudness);
            Debug.Log("I: " + intLoudness);
            intLoudness = 3;
            //intLoudness = intLoudness switch
            //{
            //    < 50 => 1,
            //    >= 50 and < 80 => 2,
            //    _ => 3
            //};
            Debug.Log("IA: " + intLoudness);
            isShouting = false;
            FindObjectOfType<NumberManager>().SetShoutLevel(intLoudness);
        }
        else
        {
            totalLoudness += GetLoudness();
        }
    }
}
