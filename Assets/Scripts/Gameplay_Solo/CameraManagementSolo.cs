using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManagementSolo : MonoBehaviour
{
    [SerializeField] Transform defaultStageViewCameraPos;
    [SerializeField] Text playerControlsType1EN, playerControlsType1JP;
    [SerializeField] Text playerControlsType2EN, playerControlsType2JP;
    [SerializeField] Text playerControlsHintsEN, playerControlsHintsJP;
    [SerializeField] Text StageViewCameraControlsEN, StageViewCameraControlsJP;
    [SerializeField] float stageViewCameraMoveFactor = 50f, stageViewCameraZoomFactor = 10f;
    [SerializeField] float stageViewCameraMinFOV = 10f;
    [SerializeField] float stageViewCameraHorizontalBoundary = 20f, stageViewCameraVerticalBoundary = 20f;
    [SerializeField] Camera playerCamera, stageViewCamera;
    [SerializeField] Canvas playerRocketCanvas, stageViewCanvas;
    [SerializeField] GameObject playerRocketCurrentPositionArrowObject;

    int playerID;
    bool stageViewFlag, showHintsFlag;
    Transform defaultStageViewCameraPosition;
    float defaultStageViewCameraFoV, stageCameraMaxFov;

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
        showHintsFlag = false;
        playerID = GetComponent<PlayerStatusSolo>().GetPlayerID();
        if (playerRocketCurrentPositionArrowObject != null)
        {
            playerRocketCurrentPositionArrowObject.SetActive(false);
        }
        defaultStageViewCameraPosition = defaultStageViewCameraPos;
        defaultStageViewCameraFoV = stageViewCamera.fieldOfView;
        stageCameraMaxFov = stageViewCamera.fieldOfView;
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                playerControlsType1EN.enabled = false;
                playerControlsType1JP.enabled = false;
                playerControlsType2EN.enabled = false;
                playerControlsType2JP.enabled = false;
                playerControlsHintsEN.enabled = true;
                playerControlsHintsJP.enabled = false;
                break;
            case Language.DisplayLanauge.Japanese:
                playerControlsType1EN.enabled = false;
                playerControlsType1JP.enabled = true;
                playerControlsType2EN.enabled = false;
                playerControlsType2JP.enabled = false;
                playerControlsHintsEN.enabled = false;
                playerControlsHintsJP.enabled = true;
                break;

        }
    }

    private void Update()
    {
        ResponseToShowHints();
        ResponseToChangeCamera();
        ResponseToMoveStageViewCamera();
    }

    private void ResponseToShowHints()
    {
        if (!stageViewFlag)
        {
            if (Input.GetButtonDown(playerID + "PShowHints"))
            {
                showHintsFlag = !showHintsFlag;
                if (showHintsFlag)
                {
                    switch (Language.gameDisplayLanguage)
                    {
                        case Language.DisplayLanauge.English:
                            switch (ControlType.chosenControlType)
                            {
                                case "Type1":
                                    playerControlsType1EN.enabled = true;
                                    playerControlsType2EN.enabled = false;
                                    break;
                                case "Type2":
                                    playerControlsType2EN.enabled = true;
                                    playerControlsType1EN.enabled = false;
                                    break;
                            }
                            playerControlsHintsEN.enabled = false;
                            break;
                        case Language.DisplayLanauge.Japanese:
                            switch (ControlType.chosenControlType)
                            {
                                case "Type1":
                                    playerControlsType1JP.enabled = true;
                                    playerControlsType2JP.enabled = false;
                                    break;
                                case "Type2":
                                    playerControlsType2JP.enabled = true;
                                    playerControlsType1JP.enabled = false;
                                    break;
                            }
                            playerControlsHintsJP.enabled = false;
                            break;

                    }
                }
                else
                {
                    switch (Language.gameDisplayLanguage)
                    {
                        case Language.DisplayLanauge.English:
                            playerControlsType1EN.enabled = false;
                            playerControlsType2EN.enabled = false;
                            playerControlsHintsEN.enabled = true;
                            break;
                        case Language.DisplayLanauge.Japanese:
                            playerControlsType1JP.enabled = false;
                            playerControlsType2JP.enabled = false;
                            playerControlsHintsJP.enabled = true;
                            break;

                    }
                }
            }
        }
    }

    private void ResponseToChangeCamera()
    {
        if(FindObjectOfType<GameManagerSolo>().GetCurrentGameState() 
            != GameManagerSolo.GameState.GameStart)
        {
            return;
        }
        if(Input.GetButtonDown(playerID + "PChangeCamera"))
        {
            stageViewFlag = !stageViewFlag;
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if (stageViewFlag)
        {
            ChangeToStageViewCamera();
            ActiveCurrentPositionArrowObject();
            GetComponent<MovementSolo>().DisablePlayerControl();
        }
        else
        {
            ResetStageViewCamera();
            ChangeToPlayerCamera();
            DeactivateCurrentPositionArrowObject();
            GetComponent<MovementSolo>().EnablePlayerControl();
        }
    }

    private void ResetStageViewCamera()
    {
        stageViewCamera.transform.position = defaultStageViewCameraPosition.position;
        stageViewCamera.fieldOfView = defaultStageViewCameraFoV;
    }

    private void ResponseToMoveStageViewCamera()
    {
        if (!stageViewFlag) { return; }
        float horizontal = Input.GetAxis("1PHorizontal");
        float vertical = Input.GetAxis("1PVertical");
        /*
        if(stageViewCamera.transform.position.x >= defaultStageViewCameraPosition.position.x - stageViewCameraHorizontalBoundary && 
           stageViewCamera.transform.position.x <= defaultStageViewCameraPosition.position.x + stageViewCameraHorizontalBoundary &&
           stageViewCamera.transform.position.y >= defaultStageViewCameraPosition.position.y - stageViewCameraVerticalBoundary &&
           stageViewCamera.transform.position.y <= defaultStageViewCameraPosition.position.y + stageViewCameraVerticalBoundary)
        {
            stageViewCamera.transform.Translate(horizontal * stageViewCameraMoveFactor * Time.deltaTime, vertical * stageViewCameraMoveFactor * Time.deltaTime, 0f);
        }
        */
        stageViewCamera.transform.Translate(horizontal * stageViewCameraMoveFactor * Time.deltaTime, vertical * stageViewCameraMoveFactor * Time.deltaTime, 0f);

        if (Input.GetButton("1PZoomIn"))
        {
            stageViewCamera.fieldOfView -= stageViewCameraZoomFactor * Time.deltaTime;
            stageViewCamera.fieldOfView = Mathf.Clamp(stageViewCamera.fieldOfView, stageViewCameraMinFOV, stageCameraMaxFov);
        }
        else if (Input.GetButton("1PZoomOut"))
        {
            stageViewCamera.fieldOfView += stageViewCameraZoomFactor * Time.deltaTime;
            stageViewCamera.fieldOfView = Mathf.Clamp(stageViewCamera.fieldOfView, stageViewCameraMinFOV, stageCameraMaxFov);
        }
        else if (Input.GetButtonDown("1PZoomReset"))
        {
            ResetStageViewCamera();
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
        if(playerCamera == null || stageViewCamera == null) { return; }
        playerCamera.enabled = true;
        stageViewCamera.enabled = false;
        if(playerRocketCanvas == null || stageViewCanvas == null) { return; }
        playerRocketCanvas.enabled = true;
        stageViewCanvas.enabled = false;

        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                playerControlsType1EN.enabled = true;
                playerControlsType1JP.enabled = false;
                break;
            case Language.DisplayLanauge.Japanese:
                playerControlsType1EN.enabled = false;
                playerControlsType1EN.enabled = true;
                break;

        }
    }

    public void ChangeToStageViewCamera()
    {
        if (playerCamera == null || stageViewCamera == null) { return; }
        stageViewCamera.enabled = true;
        playerCamera.enabled = false;
        if (playerRocketCanvas == null || stageViewCanvas == null) { return; }
        playerRocketCanvas.enabled = false;
        stageViewCanvas.enabled = true;

        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                StageViewCameraControlsEN.enabled = true;
                StageViewCameraControlsJP.enabled = false;
                break;
            case Language.DisplayLanauge.Japanese:
                StageViewCameraControlsEN.enabled = false;
                StageViewCameraControlsJP.enabled = true;
                break;

        }
    }
}
