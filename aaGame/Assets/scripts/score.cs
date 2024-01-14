using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    public static score instance;
    public string scoreS = "score";
    public float Score;
    public float maxScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(instance !=this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (maxScore < Score)
        {
            maxScore = Score;
            PlayerPrefs.SetFloat(scoreS, maxScore);
            PlayerPrefs.Save();
        }
    }
}
