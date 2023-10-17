using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CrossSceneManager : MonoBehaviour
{
    public static CrossSceneManager instance;
    public enum CrossSceneEffect { Fade,ScaleBall}
    public CrossSceneEffect crossSceneEffect = CrossSceneEffect.Fade;
    private Image fadeTarget;
    public float fadeTime;
    public Action SceneTransitionFinished;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        fadeTarget = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void ExecuteEffect(int sceneIndexToLoad)
    {
        switch (crossSceneEffect)
        {
            case CrossSceneEffect.Fade:
                Sequence s = DOTween.Sequence();
                s.AppendCallback(() => FadeToBlack());
                s.AppendInterval(fadeTime);
                s.AppendCallback(()=>SceneManager.LoadScene(sceneIndexToLoad));
                s.AppendCallback(() => FadeToTransparent());
                s.AppendCallback(() => SceneTransitionFinished());
                break;
            case CrossSceneEffect.ScaleBall:
                break;
        }
    }

    void FadeToBlack()
    {
        DOTween.To(() => fadeTarget.color, x => fadeTarget.color = x, new Color(0, 0, 0, 1), fadeTime);
    }

    void FadeToTransparent()
    {
        DOTween.To(() => fadeTarget.color, x => fadeTarget.color = x, new Color(0, 0, 0, 0), fadeTime);
    }
}
