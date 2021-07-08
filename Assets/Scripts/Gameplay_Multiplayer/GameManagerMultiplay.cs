using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMultiplay : MonoBehaviour
{

    public static GameManagerMultiplay Instance
    {
        get; private set;
    }

    [SerializeField] GameObject rocketMesh;
    [SerializeField] GameObject[] playerRockets;
    [SerializeField] GameObject[] fuelObjects;
    [SerializeField] Camera[] followPlayerCameras;
    [SerializeField] Camera[] playerStageViewCameras;
    [SerializeField] Transform[] startPositions;
    [SerializeField] Transform[] checkPointPositions;
    [SerializeField] Transform goalPosition;
    [SerializeField] Transform[] lastCheckPointPositions;
    [SerializeField] Text[] playerNameTexts;
    [SerializeField] Text[] playerSpeedTexts;
    [SerializeField] Text[] playerCheckPointTexts;
    [SerializeField] Text[] playerTimeElapsedTexts;
    [SerializeField] Text winnerPlayerNameText;
    [SerializeField] Text countDownTimeText;
    [SerializeField] float startPointSpawnPositionOffsetY = 10f;
    [SerializeField] float checkPointSpawnPositionOffsetX = 5f;
    [SerializeField] float checkPointSpawnPositionOffsetY = 10f;
    [SerializeField] float stopOtherPlayersMovementTime = 3f;
    [SerializeField] float stopMissilesMovementTime = 3f;
    [SerializeField] float showFinishPanelTime = 2f;
    [SerializeField] float startCountDownTime = 3.9f;
    [SerializeField] float hideCountDownTime = 1f;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] string startGameText = "GO!";
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] GameObject finishPanel;
    [SerializeField] Canvas finishCanvas;
    [SerializeField] Slider[] stageMiniSliders;

    float countDownTimer, startTime;
    string finalElapsedTime;

    public enum GameState
    {
        CountDown,
        GameStart,
        Pause,
        Finish
    }
    private static GameState currentGameState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        for (int i = 0; i < playerRockets.Length; i++)
        {
            if (playerRockets[i] != null)
            {
                playerRockets[i].transform.position = startPositions[i].transform.position + new Vector3(checkPointSpawnPositionOffsetX, startPointSpawnPositionOffsetY, 0f); ;
                /*
                if(stageMiniSliders[i] != null)
                {
                    stageMiniSliders[i].minValue = -Vector3.Distance(goalPosition.position, startPositions[i].transform.position);
                    stageMiniSliders[i].maxValue = 0;
                }
                */
                playerNameTexts[i].text = PlayerNameTempSaveMultiplay.playerName[i];
                playerRockets[i].GetComponent<MovementMultiplay>().DisablePlayerControl();
                lastCheckPointPositions[i] = startPositions[i];
                playerCheckPointTexts[i].text = "Start";
                //UpdateStageMiniSlider(i);
            }
            if(followPlayerCameras[i] != null)
            {
                followPlayerCameras[i].GetComponent<PostProcessLayer>().enabled = false;
            }
            if (playerStageViewCameras[i] != null)
            {
                playerStageViewCameras[i].GetComponent<PostProcessLayer>().enabled = false;
            }
        }
        if (finishCanvas != null)
        {
            finishCanvas.enabled = false;
        }
        pauseCanvas.enabled = false;
        currentGameState = GameState.CountDown;
        finalElapsedTime = "";
        Time.timeScale = 1f;
        countDownTimer = startCountDownTime;
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.CountDown:
                countDownTimeText.enabled = true;
                countDownTimer -= Time.deltaTime;
                CountDown(countDownTimer);
                break;
            case GameState.GameStart:
                for (int i = 0; i < playerRockets.Length; i++)
                {
                    if (playerRockets[i] != null)
                    {
                        UpdateTimeElapsed(i);
                        UpdatePlayerSpeedText(i);
                        ShowCheckPoint(i);
                        //UpdateStageMiniSlider(i);
                    }
                }
                RespondToPauseGame();
                break;
            case GameState.Finish:
                break;
            default:
                break;
        }
        RotateRocketMeshContinously();
    }

    private void ChangeGameState(GameState targetGameState)
    {
        currentGameState = targetGameState;
    }

    private void CountDown(float countDownTimer)
    {
        if (countDownTimeText == null)
        {
            Instance.ChangeGameState(GameState.GameStart);
            return;
        }

        ShowCountTimeText(countDownTimer);
        if (countDownTimer <= 0.0f)
        {
            Invoke("HideCountDownText", hideCountDownTime);
            Instance.ChangeGameState(GameState.GameStart);
            for(int i = 0; i < playerRockets.Length; i++)
            {
                playerRockets[i].GetComponent<MovementMultiplay>().EnablePlayerControl();
            }
        }
    }

    private void ShowCountTimeText(float countDownTimer)
    {
        int remainingSeconds = Mathf.FloorToInt(countDownTimer);
        countDownTimeText.text = (remainingSeconds > 0) ? remainingSeconds.ToString() : startGameText;
    }

    private void HideCountDownText()
    {
        countDownTimeText.enabled = false;
        startTime = Time.time - countDownTimer;
    }


    private void UpdateTimeElapsed(int playerIndex)
    {
        float t = Time.time - PlayTimeTempSaveMultiplay.totalTimeElapsed[playerIndex] - startCountDownTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");
        string miliseconds = (t * 60 % 60).ToString("00");
        finalElapsedTime = minutes + ":" + seconds + ":" + miliseconds;

        if (playerTimeElapsedTexts[playerIndex] == null) { return; }
        playerTimeElapsedTexts[playerIndex].text = finalElapsedTime;
    }

    private void CalculateRemainingDistance()
    {
        /*
        if(playerRocket == null || checkPointPositions.Length <= 0 || goalPosition == null || remainingDistanceText == null)
        {
            return;
        }
        goalDistance += Vector3.Distance(checkPointPositions[0].transform.position, playerRocket.);
        
        if (playerRocket != null && goalPosition != null && finalPositionText)
        {
            if (playerRocket.transform.position.x > goalPosition.transform.position.x)
            {
                goalDistance = -Vector3.Distance(goalPosition.position, playerRocket.transform.position);
            }
            else
            {
                goalDistance = Vector3.Distance(goalPosition.position, playerRocket.transform.position);
            }
            finalPositionText.text = Mathf.FloorToInt(goalDistance).ToString() + " m";
        }
        */
    }

    private void ShowCheckPoint(int playerIndex)
    {
        string currentCheckPoint = lastCheckPointPositions[playerIndex].gameObject.name;
        if (currentCheckPoint.Contains("Start"))
        {
            currentCheckPoint = "Start";
        }
        playerCheckPointTexts[playerIndex].text = currentCheckPoint;
    }

    private void UpdateStageMiniSlider(int playerIndex)
    {
        if (playerRockets[playerIndex - 1].transform.position.x > goalPosition.transform.position.x)
        {
            stageMiniSliders[playerIndex].value = 0;
        }
        else
        {
            stageMiniSliders[playerIndex].value = -Vector3.Distance(goalPosition.position, playerRockets[playerIndex - 1].transform.position);
        }
    }

    internal void SaveLatestCheckPoint(int playerIndex, Transform checkPointPos)
    {
        lastCheckPointPositions[playerIndex - 1] = checkPointPos;
    }

    private void UpdatePlayerSpeedText(int playerIndex)
    {
        if (playerSpeedTexts[playerIndex] == null) { return; }
        float currentSpeed;
        currentSpeed = Mathf.FloorToInt(playerRockets[playerIndex].GetComponent<Rigidbody>().velocity.magnitude);
        /*
        if (playerRocket.GetComponent<Rigidbody>().velocity.magnitude > playerRocket.GetComponent<Movement>().GetLimitedMaxVelocity())
        {
            currentSpeed = playerRocket.GetComponent<Movement>().GetLimitedMaxVelocity();
        }
        else
        {
            currentSpeed = Mathf.FloorToInt(playerRocket.GetComponent<Rigidbody>().velocity.magnitude);
        }
        */
        playerSpeedTexts[playerIndex].text = currentSpeed.ToString() + "Km / s";
    }

    private void RespondToPauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void ResetPlayerToStartPosition(int playerIndex, GameObject player)
    {
        if (lastCheckPointPositions[playerIndex - 1] != null)
        {
            player.transform.position = lastCheckPointPositions[playerIndex - 1].transform.position + new Vector3(checkPointSpawnPositionOffsetX, checkPointSpawnPositionOffsetY, 0f);
        }
        else if (startPositions[playerIndex - 1] != null)
        {
            player.transform.position = startPositions[playerIndex - 1].transform.position + new Vector3(0f, startPointSpawnPositionOffsetY, 0f);
        }
        ResetPlayerRotation(player);
    }

    public void ResetPlayerRotation(GameObject player)
    {
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.GetComponent<MovementMultiplay>().StopMovement();
    }
    public void ActiviateAllFuelObjects()
    {
        foreach (GameObject fuel in fuelObjects)
        {
            if (fuel.GetComponent<Fuel>() != null)
            {
                fuel.GetComponent<Fuel>().ActivateFuelObject();
            }
        }
    }

    public void Finish(int playerID)
    {
        if (finishCanvas == null) { return; }
        finishCanvas.enabled = true;
        ChangeGameState(GameState.Finish);
        StartCoroutine(ShowFinishPanel(playerID));
    }

    public void PauseGame()
    {
        Instance.ChangeGameState(GameState.Pause);
        pauseCanvas.enabled = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Instance.ChangeGameState(GameState.GameStart);
        pauseCanvas.enabled = false;
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        PlayTimeTempSaveSolo.totalTimeElapsed = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    private IEnumerator ShowFinishPanel(int playerIndex)
    {
        yield return new WaitForSeconds(showFinishPanelTime);

        if (finishPanel != null)
        {
            finishPanel.SetActive(true);
            string winnerPlayerName = PlayerNameTempSaveMultiplay.playerName[playerIndex - 1];
            if(winnerPlayerNameText != null)
            {
                winnerPlayerNameText.text = winnerPlayerName;
            }
        }
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }

    private void RotateRocketMeshContinously()
    {
        if(rocketMesh == null) { return; }
        Time.timeScale = 1f;
        rocketMesh.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public void StopOtherPlayersMovement(int playerID)
    {
        int playerIndex = playerID - 1;
        StartCoroutine(StopPlayersMovementAndGrayScreen(playerIndex));
    }

    private IEnumerator StopPlayersMovementAndGrayScreen(int playerIndex)
    {
        for (int i = 0; i < playerRockets.Length; i++)
        {
            if (i != playerIndex)
            {
                if (followPlayerCameras[i] != null)
                {
                    followPlayerCameras[i].GetComponent<PostProcessLayer>().enabled = true;
                }
                if (playerStageViewCameras[i] != null)
                {
                    playerStageViewCameras[i].GetComponent<PostProcessLayer>().enabled = true;
                }
                playerRockets[i].GetComponent<MovementMultiplay>().DisablePlayerControl();
                playerRockets[i].GetComponent<PlayerItem>().DisableUseItem();
                playerRockets[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        yield return new WaitForSeconds(stopOtherPlayersMovementTime);


        for (int i = 0; i < playerRockets.Length; i++)
        {
            if (i != playerIndex)
            {
                if (followPlayerCameras[i] != null)
                {
                    followPlayerCameras[i].GetComponent<PostProcessLayer>().enabled = false;
                }
                if (playerStageViewCameras[i] != null)
                {
                    playerStageViewCameras[i].GetComponent<PostProcessLayer>().enabled = false;
                }
                playerRockets[i].GetComponent<MovementMultiplay>().EnablePlayerControl();
                playerRockets[i].GetComponent<PlayerItem>().EnableUseItem();
                playerRockets[i].GetComponent<Rigidbody>().constraints =
                    RigidbodyConstraints.FreezePositionZ | 
                    RigidbodyConstraints.FreezeRotationX | 
                    RigidbodyConstraints.FreezeRotationY;
            }
        }
    }

    public void StopAllMissilesMovement()
    {
        StartCoroutine(FreezeAllMissiles());
    }

    private IEnumerator FreezeAllMissiles()
    {
        foreach(Missile missile in FindObjectsOfType<Missile>())
        {
            missile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        yield return new WaitForSeconds(stopMissilesMovementTime);

        foreach (Missile missile in FindObjectsOfType<Missile>())
        {
            missile.GetComponent<Rigidbody>().constraints = 
                    RigidbodyConstraints.FreezePositionZ |
                    RigidbodyConstraints.FreezeRotationX |
                    RigidbodyConstraints.FreezeRotationY;
        }
    }

    public GameObject GetClossetPlayerRocket(int playerID)
    {
        GameObject closetPlayerRocket = null;
        int playerIndex = playerID - 1;
        Vector3 currentPlayerPosition = playerRockets[playerIndex].transform.position;
        float closetDiff = Mathf.Infinity;
        for (int i = 0; i < playerRockets.Length; i++)
        {
            if (i != playerIndex)
            {
                Vector3 otherplayerPosition = playerRockets[i].transform.position;
                float posDiff = Vector3.Distance(currentPlayerPosition, otherplayerPosition);
                if (posDiff < closetDiff)
                {
                    closetPlayerRocket = playerRockets[i];
                }
            }
        }
        return closetPlayerRocket;
    }

    public string GetClossetPlayerName(int playerID)
    {
        string closePlayerName = "";
        int playerIndex = playerID - 1;
        Vector3 currentPlayerPosition = playerRockets[playerIndex].transform.position;
        float closetDiff = Mathf.Infinity;
        for (int i = 0; i < playerRockets.Length; i++)
        {
            if (i != playerIndex)
            {
                Vector3 otherplayerPosition = playerRockets[i].transform.position;
                float posDiff = Vector3.Distance(currentPlayerPosition, otherplayerPosition);
                if(posDiff < closetDiff)
                {
                    closePlayerName = PlayerNameTempSaveMultiplay.playerName[i];
                }
            }
        }
        return closePlayerName;
    }
}
