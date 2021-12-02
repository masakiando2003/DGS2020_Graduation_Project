using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFabrics : MonoBehaviour
{
    [SerializeField] float flySpeed = 20.0f;

    Rigidbody[] rigidBody;
    bool fabricsIsFlying;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        rigidBody = GetComponentsInChildren<Rigidbody>();
        fabricsIsFlying = false;
    }

    public void StartToFly()
    {
        fabricsIsFlying = true;
        foreach(Rigidbody rb in rigidBody)
        {
            Debug.Log("rb Game object: "+rb.gameObject.name);
            rb.velocity = new Vector3(0f, 0f, -flySpeed);
        }
    }

    private void Update()
    {
       //DestroyStoppedFabrics();
    }

    private void DestroyStoppedFabrics()
    {
        foreach (Rigidbody rb in rigidBody)
        {
            if(rb != null)
            {
                Debug.Log(rb.gameObject.name + " --- " + rb.velocity.normalized.z);
            }
            if (fabricsIsFlying && rb != null && rb.velocity.normalized.z == 0f)
            {
                Destroy(rb.gameObject);
            }
        }
    }
}
