using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingAnimController : MonoBehaviour
{
    private float animTime;
    private AnimationClip screamAnim;
    public AnimationClip[] animClips;
    public ScreamType screamType;
    public enum ScreamType { Wolf1, Wolf2, Wolf3, Snake1, Snake2, Snake3 };

    private VoiceReactor voiceReactor;

    private void Start()
    {
        InitializeAnim();
     
        voiceReactor = FindObjectOfType<VoiceReactor>();
        Debug.Log(voiceReactor.gameObject.name);
        float randomScale = Random.Range(0.8f, 1.3f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        //voiceReactor.InitializeReactor();
    }

    private void Update()
    {
        float loudness = voiceReactor.GetLoudness();
        if (loudness < 3) loudness = 0;
        if (loudness > 0) TimeForward();
        else TimeRewind();
    }

    void InitializeAnim()
    {
        switch (screamType)
        {
            case ScreamType.Wolf1:
                screamAnim = animClips[0];
                screamAnim.SampleAnimation(gameObject, 0);
                break;
            case ScreamType.Wolf2:
                screamAnim = animClips[1];
                screamAnim.SampleAnimation(gameObject, 0);
                break;
            case ScreamType.Wolf3:
                screamAnim = animClips[2];
                screamAnim.SampleAnimation(gameObject, 0);
                break;
            case ScreamType.Snake1:
                screamAnim = animClips[3];
                screamAnim.SampleAnimation(gameObject, 0);
                break;
            case ScreamType.Snake2:
                screamAnim = animClips[4];
                screamAnim.SampleAnimation(gameObject, 0);
                break;
            case ScreamType.Snake3:
                screamAnim = animClips[5];
                screamAnim.SampleAnimation(gameObject, 0);
                break;
        }
    }

    private void TimeForward()
    {
        if (screamAnim != null)
        {
            animTime += Time.deltaTime;
            if (animTime > screamAnim.length)
            {
                animTime = screamAnim.length;
            }
            screamAnim.SampleAnimation(gameObject, animTime);
        }
    }

    private void TimeRewind()
    {
        if (screamAnim != null)
        {
            if (animTime > 0)
            {
                animTime -= Time.deltaTime;
                screamAnim.SampleAnimation(gameObject, animTime);
            }
            else animTime = 0;
        }
    }


    //private void FlowerTurnIntoBlossomy()
    //{
    //    isBlossomy = true;
    //    blossomAnim.SampleAnimation(gameObject, blossomAnim.length);
    //    M_Vivarium.instance.waterAnim.SampleAnimation(M_Vivarium.instance.GetCharacterWaterAnim(treeType), 0);
    //    FluctifySkills();
    //    transform.GetComponent<BoxCollider2D>().enabled = false;
    //    if (!isDataBlossomy) UpdateUnlockedSkillToData();
    //}
}
