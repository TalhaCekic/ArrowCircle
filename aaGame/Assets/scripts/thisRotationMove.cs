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
    }

    void Update()
    {
        transform.Rotate(0, 0, speedRotation * Time.deltaTime);
        if (!value && !GameManager.instance.isFinished)
        {
            transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(2f, 2f, 2f), 2 * Time.deltaTime);
            if (transform.localScale.magnitude >= 3)
            {
                value = true;
            }
        }
        else if( value && !GameManager.instance.isFinished)
        {
            transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 2 * Time.deltaTime);
        }

        if (GameManager.instance.isFinished)
        {
            transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(0f, 0f, 0f), 5 * Time.deltaTime);
            if (transform.localScale == new Vector3(0f, 0f, 0f))
            {
                GameManager.instance.isFinished = false;
                Destroy(this.gameObject);
                
            }
            // if (transform.localScale.magnitude <= 0)
            // {
            //     GameManager.instance.isFinished = false;
            //     Destroy(this.gameObject);
            // }
        }
    }
}