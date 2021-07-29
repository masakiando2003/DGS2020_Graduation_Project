using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float movingSpeed = 50f;
    [SerializeField] float waitToStartMovingTime = 3f;

    Camera targetPlayerCamera;
    CapsuleCollider cc;
    bool canMoveToTargetPlayer;
    float offsetX;

    private void Awake()
    {
        cc = GetComponent<CapsuleCollider>();
        cc.enabled = false;
        canMoveToTargetPlayer = false;
        offsetX = 0f;
    }

    public void SetTargetPlayer(GameObject player)
    {
        targetPlayer = player;
        StartCoroutine(CountDownToMove(waitToStartMovingTime));
    }

    public void SetTargetPlayerCamera(Camera targetCamera)
    {
        targetPlayerCamera = targetCamera;
    }

    private IEnumerator CountDownToMove(float waitToStartMovingTime)
    {
        yield return new WaitForSeconds(waitToStartMovingTime);
        canMoveToTargetPlayer = true;
    }

    private void FixedUpdate()
    {
        HomingTargetPlayer();
    }

    private void HomingTargetPlayer()
    {
        if (!canMoveToTargetPlayer)
        {
            transform.position = targetPlayer.transform.position + new Vector3(offsetX, 0f, 0f);
        }

        Vector3 screenPoint = targetPlayerCamera.WorldToViewportPoint(gameObject.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            cc.enabled = true;
        }
        else
        {
            cc.enabled = false;
        }

        if(targetPlayer == null || !canMoveToTargetPlayer) { return; }

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

    public float GetMissileWaitToStartMovingTime()
    {
        return waitToStartMovingTime;
    }

    public void SetPositionOffSetX(float posOffSetX)
    {
        offsetX = posOffSetX;
    }
}
