using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] GameObject targetPlayer;
    [SerializeField] float movingSpeed = 50f;
    [SerializeField] float waitToStartMovingTime = 3f;

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

    private IEnumerator CountDownToMove(float waitToStartMovingTime)
    {
        yield return new WaitForSeconds(waitToStartMovingTime);
        cc.enabled = true;
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
