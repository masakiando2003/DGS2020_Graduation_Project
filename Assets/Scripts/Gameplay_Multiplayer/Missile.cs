using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float movingSpeed = 50f;
    [SerializeField] float rayRange = 10f;
    [SerializeField] float adjustYPos = 15f;
    [SerializeField] Quaternion targetRot = Quaternion.identity;
    [SerializeField] float rotateSpeed = 50f;
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
        Ray ray = new Ray();
        RaycastHit hitInfo;
        ray.origin = transform.position;
        
        // left of right
        if (transform.position.x >= targetPlayer.transform.position.x)
        {
            ray.direction = Vector3.left;
        }
        else
        {
            ray.direction = Vector3.right;
        }
        if (Physics.Raycast(ray, out hitInfo, rayRange)) // Front sensor
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            Debug.Log("Hit Info: Collider Tag: "+hitInfo.collider.tag);
            if (hitInfo.collider.tag == "Wall" ||
                hitInfo.collider.tag == "CheckPoint" ||
                hitInfo.collider.tag == "StartPoint" ||
                hitInfo.collider.tag == "Finish" ||
                hitInfo.collider.tag == "SafeZone")
            {
                if (hitInfo.collider.gameObject.transform.position.y >= transform.position.y)
                {
                    transform.position -= new Vector3(0f, adjustYPos, 0f) * Time.deltaTime;
                }
                else
                {
                    transform.position += new Vector3(0f, adjustYPos, 0f) * Time.deltaTime;
                }
            }
        }

        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, Time.deltaTime * movingSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStatusMultiplay>().SetCautionState("", false);
        }
        else
        {
            targetPlayer.gameObject.GetComponent<PlayerStatusMultiplay>().SetCautionState("", false);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 100f);
    }
}
