using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class musicManager : MonoBehaviour
{
    public static musicManager instance;
    public AudioSource music;
    //public bool changeBool;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
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