using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class musicManager : MonoBehaviour
{
    public static musicManager instance;
    public AudioSource music;
    //public bool changeBool;

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
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(this);
        // }
        // else if(instance !=this)
        // {
        //     Destroy(this.gameObject);
        // }
    }

    void Update()
    {
        if (menuManager.instance.isMusicActive)
        {
            music.mute = false;
        }
        else
        {
            music.mute = true;
        }

        //changeBool = false;
    }
}