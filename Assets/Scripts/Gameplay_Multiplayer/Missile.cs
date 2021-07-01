using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float movingSpeed = 5f;
    [SerializeField] float turn = 20f;
    Rigidbody rb;
    float distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        distance = Mathf.Infinity;
    }

    public void SetTargetPlayer(GameObject player)
    {
        targetPlayer = player;
        distance = Mathf.Infinity;
        var diff = (targetPlayer.transform.position - gameObject.transform.position).sqrMagnitude;
        if(diff < distance)
        {
            distance = diff;
        }
    }

    private void FixedUpdate()
    {
        HomingTargetPlayer();
    }

    private void HomingTargetPlayer()
    {
        if(targetPlayer == null) { return; }
        rb.velocity = transform.forward * movingSpeed * Time.deltaTime;
        var targetRotation = Quaternion.LookRotation(targetPlayer.transform.position - gameObject.transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, turn));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
