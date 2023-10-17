using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class NumberManager : MonoBehaviour
{
    public Transform soldiersToNumber;
    public Sprite soldierCrowd;
    public Vector2 soldierSquare;
    public Vector2 startPivot;
    public Vector2 apartDistance;
    public int playerIndex;
    public float numberingDelay;
    private bool isPlayerGap = false;
    private bool isShouted = false;
    private Detector_Number detector;

    // Start is called before the first frame update
    void Start()
    {
        detector = FindObjectOfType<Detector_Number>();
        RearrangeSoldierHorizontally();
        SoldierCrowdGeneration();
        StartCoroutine(Numbering());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerGap)
        {
            Debug.Log("IsGapping");
            int shoutLevel = detector.GetShoutedLevel();
            if (shoutLevel != 0 && !isShouted)
            {
                //Debug.Log("Shouted");
                //detector.isShoutBegin = true;
                soldiersToNumber.GetChild(4).Find("MMF_Number").GetComponent<MMF_Player>().PlayFeedbacks();
                isShouted = true;
            }
        }
    }

    public void SetShoutLevel(int loudness)
    {
        int shoutLevel = loudness;
        MMF_Player mmf_Number = soldiersToNumber.GetChild(4).Find("MMF_Number").GetComponent<MMF_Player>();
        MMF_Scale scaleFeedback = mmf_Number.GetFeedbackOfType<MMF_Scale>();

        if (shoutLevel == 3) scaleFeedback.RemapCurveOne = 3;
        else
        {
            Debug.Log("ENtered");
            StopCoroutine(Numbering());
            scaleFeedback.RemapCurveOne = 2;
        }
        mmf_Number.PlayFeedbacks();
    }

    public void RearrangeSoldierHorizontally()
    {
        for (int i = 0; i < soldiersToNumber.childCount; i++)
        {
            soldiersToNumber.GetChild(i).position = new Vector2(i * apartDistance.x + startPivot.x, soldiersToNumber.GetChild(i).position.y);
        }
    }

    public void SoldierCrowdGeneration()
    {
        GameObject soldierContainer = new GameObject("Soldier Container");

        for (int i = 0; i < soldierSquare.y; i++)
        {
            for (int j = 0; j < soldierSquare.x; j++)
            {
                SpriteRenderer newSoldier = new GameObject("Soldier").AddComponent<SpriteRenderer>();
                newSoldier.sprite = soldierCrowd;
                newSoldier.transform.position = new Vector2(j * apartDistance.x + startPivot.x, i * apartDistance.y + startPivot.y);
                newSoldier.transform.localScale = new Vector2(1 - 0.1f * (i+1), 1 - 0.1f * (i+1));
                newSoldier.sortingOrder = -(i + 1);
                newSoldier.transform.SetParent(soldierContainer.transform);
            }
        }
    }

    IEnumerator Numbering()
    {
        for (int i = 0; i < soldiersToNumber.childCount; i++)
        {
            MMF_Player mmf_Number = soldiersToNumber.GetChild(i).Find("MMF_Number").GetComponent<MMF_Player>();
            if (i + 1 != playerIndex)
            {
                mmf_Number.PlayFeedbacks();
            }
            else isPlayerGap = true;

            yield return new WaitForSeconds(numberingDelay);
            isPlayerGap = false;
        }

        yield return new WaitForSeconds(1);

        if (isShouted) FindObjectOfType<GameEnd>().GameWin(0);
        else FindObjectOfType<GameEnd>().GameLose(3);
    }
}
