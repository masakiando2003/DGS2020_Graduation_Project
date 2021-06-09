using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] Camera playerCamera, stageViewCamera;
    [SerializeField] Canvas playerRocketCanvas;

    int playerID;
    bool stageViewFlag;

    private void Awake()
    {
        InitialCamera();
    }

    private void InitialCamera()
    {
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        if (stageViewCamera != null)
        {
            stageViewCamera.enabled = false;
        }
        stageViewFlag = false;
        playerID = GetComponent<PlayerStatus>().GetPlayerID();
    }

    private void Update()
    {
        ResponseToChangeCamera();
    }

    private void ResponseToChangeCamera()
    {
        if(FindObjectOfType<GameManagerSolo>().GetCurrentGameState() 
            != GameManagerSolo.GameState.GameStart)
        {
            return;
        }
        if(Input.GetButton(playerID + "PChangeCamera"))
        {
            stageViewFlag = true;
        }
        else
        {
            stageViewFlag = false;
        }
        if (stageViewFlag)
        {
            ChangeToStageViewCamera();
        }
        else
        {
            ChangeToPlayerCamera();
        }
    }

    public void ChangeToPlayerCamera()
    {
        if(playerCamera == null || stageViewCamera == null) { return; }
        playerCamera.enabled = true;
        stageViewCamera.enabled = false;
        if(playerRocketCanvas == null) { return; }
        playerRocketCanvas.enabled = true;
    }

    public void ChangeToStageViewCamera()
    {
        if (playerCamera == null || stageViewCamera == null) { return; }
        stageViewCamera.enabled = true;
        playerCamera.enabled = false;
        if (playerRocketCanvas == null) { return; }
        playerRocketCanvas.enabled = false;
    }
}
