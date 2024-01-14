using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thisRotationMove : MonoBehaviour
{
    public static thisRotationMove instance;
    public float speedRotation;
    public bool value;

    void Start()
    {
        instance = this;
        
        //random
        int randomDirection = Random.Range(0, 2);
        if (randomDirection == 1)
        {
            speedRotation -= 100.0f;
        }
        else
        {
            speedRotation += 100.0f;
        }

    }

    void Update()
    {
        transform.Rotate(0, 0, GameManager.instance.speedObj*speedRotation * Time.deltaTime);
        transform.position = new Vector3(0, 2, 0);
        if (!value && !GameManager.instance.isFinished)
        {
            transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 2 * Time.deltaTime);
            if (transform.localScale.magnitude >= 3)
            {
                value = true;
            }
        }
        else if(value && !GameManager.instance.isFinished)
        {
            transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1f, 1f, 1f), 2 * Time.deltaTime);
        }

        if (GameManager.instance.isFinished)
        {
            transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(0f, 0f, 0f), 5 * Time.deltaTime);
            if (transform.localScale == new Vector3(0f, 0f, 0f))
            {
                GameManager.instance.isFinished = false;
                Destroy(this.gameObject);
                
            }
        }
    }
}