using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentPositionArrow : MonoBehaviour
{
    [SerializeField] GameObject playerRocket;
    [SerializeField] float arrowPositionOffsetY = 10f;

    // Update is called once per frame
    void Update()
    {
        UpdateArrowPosition();
    }

    private void UpdateArrowPosition()
    {
        if(playerRocket == null) { return; }
        gameObject.transform.position = new Vector3(playerRocket.transform.position.x,
            playerRocket.transform.position.y + arrowPositionOffsetY,
            playerRocket.transform.position.z);
    }
}
