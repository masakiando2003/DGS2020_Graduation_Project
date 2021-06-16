using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManagementMultiplay : MonoBehaviour
{
    [SerializeField] Camera playerCamera, stageViewCamera;
    [SerializeField] GameObject boostSlider;
    [SerializeField] GameObject playerRocketCurrentPositionArrowObject;

    int playerID;
    bool stageViewFlag;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
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
        playerID = GetComponent<PlayerStatusMultiplay>().GetPlayerID();
        if (playerRocketCurrentPositionArrowObject != null)
        {
            playerRocketCurrentPositionArrowObject.SetActive(false);
        }
    }

    private void Update()
    {
        ResponseToChangeCamera();
    }

    private void ResponseToChangeCamera()
    {
        if (FindObjectOfType<GameManagerMultiplay>().GetCurrentGameState()
            != GameManagerMultiplay.GameState.GameStart)
        {
            return;
        }
        if (Input.GetButton(playerID + "PChangeCamera"))
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
            ActiveCurrentPositionArrowObject();
        }
        else
        {
            ChangeToPlayerCamera();
            DeactivateCurrentPositionArrowObject();
        }
    }

    private void ActiveCurrentPositionArrowObject()
    {
        playerRocketCurrentPositionArrowObject.SetActive(true);
    }

    private void DeactivateCurrentPositionArrowObject()
    {
        playerRocketCurrentPositionArrowObject.SetActive(false);
    }

    public void ChangeToPlayerCamera()
    {
        if (playerCamera == null || stageViewCamera == null) { return; }
        playerCamera.enabled = true;
        stageViewCamera.enabled = false;
        boostSlider.SetActive(true);
    }

    public void ChangeToStageViewCamera()
    {
        if (playerCamera == null || stageViewCamera == null) { return; }
        stageViewCamera.enabled = true;
        playerCamera.enabled = false;
        boostSlider.SetActive(false);
    }
}
