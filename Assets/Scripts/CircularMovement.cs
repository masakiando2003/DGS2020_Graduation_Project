using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float width, height;
    float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        timeCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessCircularMovement();
    }

    private void ProcessCircularMovement()
    {
        timeCounter += Time.deltaTime * movementSpeed;
        float x = transform.position.x + Mathf.Cos(timeCounter) * width;
        float y = transform.position.y + Mathf.Sin(timeCounter) * height;
        float z = 0f;

        transform.position = new Vector3(x, y, z);
    }
}
