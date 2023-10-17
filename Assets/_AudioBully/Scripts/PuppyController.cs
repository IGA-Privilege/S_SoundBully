using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuppyController : MonoBehaviour
{
    public LineMatch[] linesMatch;
    void Update()
    {
        FollowMouse();
        MatchLinePosition();
    }

    private void FollowMouse()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }

    private void MatchLinePosition()
    {
        for (int i = 0; i < linesMatch.Length; i++)
        {
            linesMatch[i].line.SetPosition(0, linesMatch[i].line.transform.position);
            linesMatch[i].line.SetPosition(1, linesMatch[i].puppyPivot.position);
        }
    }
}

[System.Serializable]
public class LineMatch
{
    public Transform puppyPivot;
    public LineRenderer line;
}
