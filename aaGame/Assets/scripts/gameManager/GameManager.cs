using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ScribtableObstacle _scribtableObstacle;
    public bool isFinished;

    public string scoreS = "score";
    public TMP_Text scoreText;
    public TMP_Text scoreAddText;
    public float score;
    public float maxScore;
    public bool ScoreUp;
    public bool scoreAdd;
    public Transform target1;
    public Transform target2;

    public bool isSpawnArrow;
    public bool isNext;

    //scene settings
    public bool isDead;

    //menu islemler
    public Button pauseButton;
    public GameObject pauseUI;
    public GameObject deadUI;

    public AudioSource buttonClick;

    void Start()
    {
        instance = this;
        isNext = true;
        isDead = false;
        pauseUI.gameObject.SetActive(false);
        deadUI.gameObject.SetActive(false);
        scoreAddText.gameObject.SetActive(false);
    }

    void Update()
    {
        Score();
        scoreText.text = score.ToString();

        if (isDead)
        {
            Dead();
        }

        isNexting();
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

    void Score()
    {
        if (ScoreUp)
        {
            score += 10;
            ScoreUp = false;
            scoreAdd = true;
            if (maxScore < score)
            {
                maxScore = score;
                PlayerPrefs.SetFloat(scoreS, maxScore);
                PlayerPrefs.Save();
            }
        }

        if (scoreAdd)
        {
            scoreAddText.gameObject.SetActive(true);
            scoreAddText.text = "+10";
            scoreAddText.gameObject.transform.DOMove(target2.position, 2).OnComplete(() =>
            {
                scoreAddText.gameObject.SetActive(false);
                scoreAddText.gameObject.transform.DOMove(target1.position, 1);
                scoreAdd = false;
            });
        }
    }

    private void isNexting()
    {
        if (scoreAdd)
        {
             float targetSize = 0;
             float duration = 1f;
             Camera.main.DOOrthoSize(targetSize, duration);
        }
        else
        {
            float targetSize2 = 5;
            float duration2 = 2f;
            Camera.main.DOOrthoSize(targetSize2, duration2);
        }
    }
    
    //menus:
    public void ResumeButton()
    {
        if (menuManager.instance.isSoundActive)
        {
            buttonClick.Play();
        }
        pauseButton.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseButton()
    {
        if (menuManager.instance.isSoundActive)
        {
            buttonClick.Play();
        }
        pauseButton.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        pauseButton.gameObject.SetActive(true);
        deadUI.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        isDead = false;
        menuManager.instance.RestartButton();
    }

    public void MainMenuButton()
    {
        pauseButton.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(false);
        deadUI.gameObject.SetActive(false);
        menuManager.instance.MainMenuButton();
    }

    public void Soundbutton()
    {
        buttonClick.Play();
        menuManager.instance.isSoundActive = !menuManager.instance.isSoundActive;
        if (menuManager.instance.isSoundActive)
        {
            menuManager.instance.soundValue = 1;
        }
        else
        {
            menuManager.instance.soundValue = 0;
        }

        PlayerPrefs.SetInt(menuManager.instance.sound, menuManager.instance.soundValue);
        PlayerPrefs.Save();
    }

    public void Musicbutton()
    {
        buttonClick.Play();
        menuManager.instance.isMusicActive = !menuManager.instance.isMusicActive;
        if (menuManager.instance.isMusicActive)
        {
            menuManager.instance.musicValue = 1;
        }
        else
        {
            menuManager.instance.musicValue = 0;
        }

        PlayerPrefs.SetInt(menuManager.instance.music, menuManager.instance.musicValue);
        PlayerPrefs.Save();
    }

    public void Dead()
    {
        deadUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}