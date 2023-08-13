using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class M_Environment : MonoBehaviour
{
    public Sprite[] elements;
    public Vector3 pos_Start;
    public Vector3 pos_End;

    public float moveSpeed;
    public string layerName_Environment;

    private void Start()
    {
        //M_Global.Instance.GameStart += () => isEnGen = true;
        //M_HeartJelly.Instance.SwitchWorldView += StoneAppearanceChange;

        StartCoroutine(SpawnElements());
    }

    void Update()
    {
        //if (isEnGen) GenerateNewElements();
    }

    void GenerateNewElements()
    {
        int randomGroup = Random.Range(0, 10);
        int spawnNumber = randomGroup switch
        {
            >= 0 and < 3 => 1,
            >= 3 and < 6 => 2,
            >= 6 and < 9 => 3,
            _ => 4
        };
        StartCoroutine(SpawnWeed(spawnNumber));

        IEnumerator SpawnWeed(int toSpawnNum)
        {
            for (int i = 0; i < toSpawnNum; i++)
            {
                int random = Random.Range(0, elements.Length);

                bool isFlip = Random.Range(0, 10) > 4;
                Transform newTrans = new GameObject("New Stone").AddComponent<SpriteRenderer>().transform;
                newTrans.GetComponent<SpriteRenderer>().sprite = elements[random];
                newTrans.position = pos_Start;
                newTrans.localScale = isFlip ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
                newTrans.GetComponent<SpriteRenderer>().sortingLayerName = layerName_Environment;
                float randomScale = Random.Range(0.7f, 1.3f);
                newTrans.localScale = new Vector3(randomScale, randomScale, 1);

                newTrans.DOMoveX(pos_End.x, moveSpeed).OnComplete(() => Destroy(newTrans.gameObject));

                //Transform newTrans = Instantiate(weeds[random], pos_Start, Quaternion.identity).transform;
        
                //newTrans.DOMoveX(pos_End.x, moveSpeed).OnComplete(() => Destroy(newTrans.gameObject));
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            }
        }
    }

    IEnumerator SpawnElements()
    {
        while (true)
        {
            GenerateNewElements();
            //int random = Random.Range(0, elements.Length);
            //bool isFlip = Random.Range(0, 10) > 4;
            //Transform newTrans = new GameObject("New Stone").AddComponent<SpriteRenderer>().transform;
            //newTrans.GetComponent<SpriteRenderer>().sprite = elements[random];
            //newTrans.position = pos_Start;
            //newTrans.localScale = isFlip ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            //newTrans.GetComponent<SpriteRenderer>().sortingLayerName = layerName_Environment;
            //newTrans.DOMoveX(pos_End.x, moveSpeed).OnComplete(() => Destroy(newTrans.gameObject));

            float delay = Random.Range(2f, 4f);
            yield return new WaitForSeconds(delay);
        }
    }


    void SpawnStone()
    {
        int random = Random.Range(0, elements.Length);
        bool isFlip = Random.Range(0, 10) > 4;
        Transform newTrans = new GameObject("New Stone").AddComponent<SpriteRenderer>().transform;
        newTrans.GetComponent<SpriteRenderer>().sprite = elements[random];
        newTrans.position = pos_Start;
        newTrans.localScale = isFlip ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        newTrans.GetComponent<SpriteRenderer>().sortingLayerName = layerName_Environment;
        newTrans.DOMoveX(pos_End.x, moveSpeed).OnComplete(() => Destroy(newTrans.gameObject));

        //void RemoveStoneAndDestroy(GameObject stoneObj)
        //{
        //    inSceneStones.Remove(stoneObj);
        //    Destroy(stoneObj);
        //}
    }
}
