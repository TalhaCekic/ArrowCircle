using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arrowSpawn : MonoBehaviour
{
    public ScribtableObstacle _scribtableObstacle;
    public GameObject selectArrow;

    public float selectedArrowCount;
    public float delay;
    public TMP_Text arrowCountText;

    void Start()
    {
        selectedArrowCount = GetRandomElement(_scribtableObstacle.arrows);
        spawnAwwor();
    }

    void Update()
    {
        if (selectedArrowCount > 0)
        {
            if (GameManager.instance.isSpawnArrow)
            {
                delay += Time.deltaTime;
                if (delay > 1)
                {
                    spawnAwwor();
                    delay = 0;
                }
            }

            if (GameManager.instance.isNext)
            {
                ArrowCount();
            }

            if (selectedArrowCount < 2)
            {
                arrowCountText.color = Color.red;
            }
            else
            {
                arrowCountText.color = Color.white;
            }
        }
        else
        {
            GameManager.instance.isDead = true;
        }
        
        arrowCountText.text = "x" + selectedArrowCount.ToString();
    }

    private void spawnAwwor()
    {
        selectArrow = _scribtableObstacle.arrowObj; 
        Instantiate(selectArrow);
        selectArrow.transform.position = new Vector3(0, -5,0);
        selectedArrowCount--;
        GameManager.instance.isSpawnArrow = false;
    }

    void ArrowCount()
    {
        selectedArrowCount = GetRandomElement(_scribtableObstacle.arrows);
        GameManager.instance.isNext = false;
    }

    //randomize aray sistemi
    private T GetRandomElement<T>(T[] array)
    {
        if (array != null && array.Length > 0)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }
        else
        {
            return default(T);
        }
    }
}