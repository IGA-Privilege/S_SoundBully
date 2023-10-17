using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnd : MonoBehaviour
{
    public SpriteRenderer endding;

    public void GameLose(int targetReloadScene)
    {
        Sequence s = DOTween.Sequence();
        s.AppendCallback(() => DOTween.To(() => endding.color, x => endding.color = x, new Color(0, 0, 0, 1), 1));
        s.AppendInterval(1);
        s.AppendCallback(() => DOTween.To(() => endding.color, x => endding.color = x, new Color(1, 1, 1, 1), 1));
        s.AppendInterval(3);
        s.AppendCallback(() => CrossSceneManager.instance.ExecuteEffect(targetReloadScene));
    }

    public void GameWin(int nextToLoadScene)
    {
        CrossSceneManager.instance.ExecuteEffect(nextToLoadScene);
    }
}
