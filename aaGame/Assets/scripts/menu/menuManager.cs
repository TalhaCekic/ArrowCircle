using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    public static menuManager instance;

    public string MenuScore;
    public float scoreMenu;

    //sound
    public string sound = "sound";
    public string music = "music";
    public int soundValue;
    public int musicValue;
    public Sprite activeSound;
    public Sprite deActiveSound;
    public bool isSoundActive;
    public Sprite activeMusic;
    public Sprite deActiveMusic;
    public bool isMusicActive;

    public TMP_Text scoreTextMenu;

    public AudioSource buttonClick;

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
        isSoundActive = true;
        isMusicActive = true;

        scoreMenu = PlayerPrefs.GetFloat(GameManager.instance.scoreS, GameManager.instance.maxScore);
        scoreTextMenu.text = scoreMenu.ToString();


        soundValue = PlayerPrefs.GetInt(sound, soundValue);
        musicValue= PlayerPrefs.GetInt(music, musicValue);
    }

    void Start()
    {
    
    }

    void Update()
    {
        GameObject soundGameObject = GameObject.Find("sound");
        Button soundButton = soundGameObject.GetComponent<Button>();
        GameObject musicGameObject = GameObject.Find("music");
        Button musicButton = musicGameObject.GetComponent<Button>();
        if (soundValue == 1)
        {
            isSoundActive = true;
            soundButton.image.sprite = activeSound;
            buttonClick.mute = false;
        }
        else
        {
            isSoundActive = false;
            soundButton.image.sprite = deActiveSound;
            buttonClick.mute = true;
        }

        if (musicValue == 1)
        {
            isMusicActive = true;
            musicButton.image.sprite = activeMusic;
        }
        else
        {
            isMusicActive = false;
            musicButton.image.sprite = deActiveMusic;
        }

        PlayerPrefs.SetInt(sound, soundValue);
        PlayerPrefs.SetInt(music, musicValue);
        PlayerPrefs.Save();
    }

    public void PlayButton()
    {
        if (isSoundActive)
        {
            buttonClick.Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SoundButton()
    {
        buttonClick.Play();
        isSoundActive = !isSoundActive;
        if (isSoundActive)
        {
            soundValue = 1;
        }
        else
        {
            soundValue = 0;
        }
    }

    public void MusicButton()
    {
        if (isSoundActive)
        {
            buttonClick.Play();
        }
        isMusicActive = !isMusicActive;
        if (isMusicActive)
        {
            musicValue = 1;
        }
        else
        {
            musicValue = 0;
        }
    }

    public void RestartButton()
    {
        if (isSoundActive)
        {
            buttonClick.Play();
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        if (isSoundActive)
        {
            buttonClick.Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Destroy(this);
    }

    public void QuitButton()
    {
        if (isSoundActive)
        {
            buttonClick.Play();
        }
        Application.Quit();
    }
}