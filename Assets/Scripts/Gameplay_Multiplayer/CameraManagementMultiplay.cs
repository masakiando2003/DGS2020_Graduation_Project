using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManagementMultiplay : MonoBehaviour
{
    [SerializeField] Transform defaultStageViewCameraPos;
    [SerializeField] Text StageViewCameraControlsEN, StageViewCameraControlsJP;
    [SerializeField] float stageViewCameraMoveFactor = 50f, stageViewCameraZoomFactor = 10f;
    [SerializeField] float stageViewCameraMinFOV = 10f;
    [SerializeField] float stageViewCameraHorizontalBoundary = 20f, stageViewCameraVerticalBoundary = 20f;
    [SerializeField] Camera playerCamera, stageViewCamera;
    [SerializeField] GameObject boostSlider, playerItemLabels;
    [SerializeField] Canvas playerRocketCanvas, stageViewCanvas;
    [SerializeField] GameObject playerRocketCurrentPositionArrowObject;

    int playerID;
    bool stageViewFlag;
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
        playerID = GetComponent<PlayerStatusMultiplay>().GetPlayerID();
        if (playerRocketCurrentPositionArrowObject != null)
        {
            playerRocketCurrentPositionArrowObject.SetActive(false);
        }
        defaultStageViewCameraPosition = defaultStageViewCameraPos;
        defaultStageViewCameraFoV = stageViewCamera.fieldOfView;
        stageCameraMaxFov = stageViewCamera.fieldOfView;
    }

    private void Update()
    {
        ResponseToChangeCamera();
        ResponseToMoveStageViewCamera();
    }

    private void ResponseToChangeCamera()
    {
        if (FindObjectOfType<GameManagerMultiplay>().GetCurrentGameState()
            != GameManagerMultiplay.GameState.GameStart)
        {
            return;
        }
        if (Input.GetButtonDown(playerID + "PChangeCamera"))
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
            GetComponent<MovementMultiplay>().DisablePlayerControl();
        }
        else
        {
            ResetStageViewCamera();
            ChangeToPlayerCamera();
            DeactivateCurrentPositionArrowObject();
            GetComponent<MovementMultiplay>().EnablePlayerControl();
        }
    }

    private void ResponseToMoveStageViewCamera()
    {
        if (!stageViewFlag) { return; }
        playerID = GetComponent<PlayerStatusMultiplay>().GetPlayerID();
        float horizontal = Input.GetAxis(playerID+"PHorizontal");
        float vertical = Input.GetAxis(playerID+"PVertical");
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

        if (Input.GetButton(playerID + "PZoomIn"))
        {
            stageViewCamera.fieldOfView -= stageViewCameraZoomFactor * Time.deltaTime;
            stageViewCamera.fieldOfView = Mathf.Clamp(stageViewCamera.fieldOfView, stageViewCameraMinFOV, stageCameraMaxFov);
        }
        else if (Input.GetButton(playerID + "PZoomOut"))
        {
            stageViewCamera.fieldOfView += stageViewCameraZoomFactor * Time.deltaTime;
            stageViewCamera.fieldOfView = Mathf.Clamp(stageViewCamera.fieldOfView, stageViewCameraMinFOV, stageCameraMaxFov);
        }
        else if (Input.GetButtonDown(playerID + "PZoomReset"))
        {
            ResetStageViewCamera();
        }
    }

    private void ResetStageViewCamera()
    {
        stageViewCamera.transform.position = defaultStageViewCameraPosition.position;
        stageViewCamera.fieldOfView = defaultStageViewCameraFoV;
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
        if (playerItemLabels== null || stageViewCanvas == null) { return; }
        playerItemLabels.SetActive(true);
        stageViewCanvas.enabled = false;
    }

    public void ChangeToStageViewCamera()
    {
        if (playerCamera == null || stageViewCamera == null) { return; }
        stageViewCamera.enabled = true;
        playerCamera.enabled = false;
        boostSlider.SetActive(false);
        if (playerItemLabels == null || stageViewCanvas == null) { return; }
        playerItemLabels.SetActive(false);
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
