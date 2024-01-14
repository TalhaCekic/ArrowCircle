using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class inputMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed;
    public bool isReady;
    public bool isGo;
    public bool isTouch;
    public string touchColor;
    public bool isDestroyer;
    public float delay;

    private float desroyDelay;

    public AudioSource touchSound;

    //public Transform TargetPos;
    private Vector3 TargetPos = new Vector3(0, -4, 0);

    // Titreşim süresi
    public float shakeDuration = 0.5f;

    // Titreşim şiddeti
    public float shakeStrength = 0.2f;

    // Kamera
    private Transform mainCameraTransform;

    public GameObject consoleObj;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        consoleObj = GameObject.Find("console");
        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        TouchScreen();
        arrowMovement();
        Detector();

        if (isDestroyer)
        {
            delay += Time.deltaTime;
            if (delay > 0.5f)
            {
                  Destroy(this.gameObject);
            }
        }
    }

    private void TouchScreen()
    {
        if (Input.touchCount > 0 && isReady)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                isGo = true;
            }
        }
    }

    private void arrowMovement()
    {
        if (isGo)
        {
            rb.simulated = true;
            rb.AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        else
        {
            rb.simulated = false;
        }
    }

    void Detector()
    {
        if (transform.position != TargetPos)
        {
            transform.position = Vector3.Lerp(this.transform.position, TargetPos, 4 * Time.deltaTime);
            isReady = true;
        }
        else
        {
            isReady = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("red"))
        {
            GameManager.instance.isDead = true;
            mainCameraTransform.DOShakePosition(shakeDuration, shakeStrength);
            
            if (menuManager.instance.isSoundActive)
            {
                touchSound.Play();
            }

        }
        else if (other.CompareTag("green"))
        {
            if (menuManager.instance.isSoundActive)
            {
                touchSound.Play();
            }
            this.transform.parent = other.gameObject.transform.parent;
            Destroy(rb);
            enabled = false;
            
            RandomObstacle.instance.isNextObstacle = true;
            GameManager.instance.isFinished = true;
            GameManager.instance.ScoreUp = true;
            GameManager.instance.isSpawnArrow = true;
            GameManager.instance.isNext = true;
            isDestroyer = true;
        }
        else if (other.CompareTag("white"))
        {
            if (menuManager.instance.isSoundActive)
            {
                touchSound.Play();
            }
            GameManager.instance.isSpawnArrow = true;
            this.transform.parent = other.gameObject.transform.parent;
            Destroy(rb);
            enabled = false;
            mainCameraTransform.DOShakePosition(shakeDuration, shakeStrength);
        }
        else
        {
            GameManager.instance.isSpawnArrow = true;
        }

        speed = 0;
    }
}