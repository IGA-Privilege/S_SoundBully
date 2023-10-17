using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool isStart;
    private float animTime;
    private AnimationClip buttonAnim;
    public AnimationClip[] animClips;
    private bool isLeaved = true;

    private void Start()
    {
        if (isStart) 
        {
            buttonAnim = animClips[0];
            buttonAnim.SampleAnimation(gameObject, 0);
        }
        else
        {
            buttonAnim = animClips[1];
            buttonAnim.SampleAnimation(gameObject, 0);
        }
    }

    private void Update()
    {
        if (isLeaved) TimeRewind();
        else TimeForward();
    }

    private void OnMouseEnter()
    {
        transform.DOScale(1.3f, 0.3f);
        isLeaved = false;

    }

    private void OnMouseDown()
    {
        if (isStart) CrossSceneManager.instance.ExecuteEffect(1);
        else Application.Quit();
    }

    private void OnMouseExit() 
    {
        transform.DOScale(1f, 0.3f);
        isLeaved = true;

    }

    private void TimeForward()
    {
        if (buttonAnim != null)
        {
            animTime += Time.deltaTime;
            if (animTime > buttonAnim.length)
            {
                animTime = buttonAnim.length;
            }
            buttonAnim.SampleAnimation(gameObject, animTime);
        }
    }

    private void TimeRewind()
    {
        if (buttonAnim != null)
        {
            if (animTime > 0)
            {
                animTime -= Time.deltaTime;
                buttonAnim.SampleAnimation(gameObject, animTime);
            }
            else animTime = 0;
        }
    }
}
