using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float movingSpeed = 5f;
    [SerializeField] float torqueRatio = 20f;
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
        /*
        var diff = targetPlayer.transform.position - transform.position;
        var target_rot = Quaternion.LookRotation(diff);
        var rot = target_rot * Quaternion.Inverse(transform.rotation);
        if(rot.w < 0f)
        {
            rot.x = -rot.x;
            rot.y = -rot.y;
            rot.z = -rot.z;
            rot.w = -rot.w;
        }
        var torque = new Vector3(rot.x, rot.y, rot.z) * torqueRatio;
        rb.AddTorque(torque);
        rb.velocity = transform.forward * movingSpeed * Time.deltaTime;
        */
        //rb.MovePosition(targetPlayer.transform.position);

        rb.velocity = transform.forward * movingSpeed * Time.deltaTime;
        var targetRotation = Quaternion.LookRotation(targetPlayer.transform.position - gameObject.transform.position);
        //rb.MoveRotation(Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, torqueRatio));
        rb.MoveRotation(targetRotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Item")
        {
            if(other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerStatusMultiplay>().SetCautionState("", false);
            }
            Destroy(gameObject);
        }
    }
}
