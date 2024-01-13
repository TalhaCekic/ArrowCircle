using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    public static RandomObstacle instance;
    public ScribtableObstacle _scribtableObstacle;
    public GameObject SelectedObstacle;
    public bool isNextObstacle;
    public bool isSpawn;
    public float delay;
    public float a = 0.1f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        firsSpawn();
    }

    void Update()
    {
        NextObstacle();
        spawner();
    }

    void NextObstacle()
    {
        if (isNextObstacle)
        {
            delay += Time.deltaTime;
            if (delay > 2)
            {
                isSpawn = true;
                SelectedObstacle = GetRandomElement(_scribtableObstacle.ObtacleObject);
                isNextObstacle = false;
                delay = 0;
            }
        }
    }

    void spawner()
    {
        if (isSpawn && !GameManager.instance.isFinished)
        {
            Instantiate(SelectedObstacle);
            isSpawn = false;
        }
    }

    private void firsSpawn()
    {
        SelectedObstacle = GetRandomElement(_scribtableObstacle.ObtacleObject);
        Instantiate(SelectedObstacle);
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