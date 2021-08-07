using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerSolo : MonoBehaviour
{
    public static GameManagerSolo Instance
    {
        get; private set;
    }

    [SerializeField] GameObject playerRocket;
    [SerializeField] GameObject[] fuelObjects;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform[] checkPointPositions;
    [SerializeField] Waypoint[] wayPoints;
    [SerializeField] Transform goalPosition;
    [SerializeField] Transform lastCheckPointPosition;
    [SerializeField] Text playerNameText;
    [SerializeField] Text playerLifeText;
    [SerializeField] Text playerSpeedText;
    [SerializeField] Text playerBoostText; // For Debug
    [SerializeField] Text remainingTimeText;
    [SerializeField] Text timeElapsedText;
    [SerializeField] Text finalPositionText;
    [SerializeField] Text countDownTimeText;
    [SerializeField] Text clearRankedLabel, clearRankedText, clearOutOfRankText;
    [SerializeField] Text gameOverRankedLabel, gameOverRankedText, gameOverOutOfRankText;
    [SerializeField] float startPointSpawnPositionOffsetY = 10f;
    [SerializeField] float checkPointSpawnPositionOffsetX = 5f;
    [SerializeField] float checkPointSpawnPositionOffsetY = 10f;
    [SerializeField] float maxRemainingTime = 300f;
    [SerializeField] float showGameOverPanelTime = 2f;
    [SerializeField] float startCountDownTime = 3.9f;
    [SerializeField] float hideCountDownTime = 1f;
    [SerializeField] string startGameText = "GO!";
    [SerializeField] TextAsset rankingDataFile;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas rankingCanvas;
    [SerializeField] GameObject clearPanel, clearPanel2;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Canvas clearCanvas;
    [SerializeField] Slider stageMiniSlider;
    [SerializeField] Text[] rankingPlayerNameText;
    [SerializeField] Text[] rankingFinalPositionText;
    [SerializeField] Text[] rankingRemainingTimeText;
    [SerializeField] Text[] rankingTimeElaspedText;

    int currentCheckPointIndex;
    float remainingTime, countDownTimer, timeElapsed, startTime, totalDistance;
    string fromPanel, finalElapsedTime, finalPosition;

    public enum GameState
    {
        CountDown,
        GameStart,
        Pause,
        Win,
        GameOver
    }
    private static GameState currentGameState;

    [Serializable]
    public class RankingJson
    {
        public RankingData[] detail;
    }

    [Serializable]
    public class RankingData
    {
        public string playerName;
        public string finalPosition;
        public int checkPointIndex;
        public int remainingTime;
        public string timeElapsed;
    }

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
        if (playerRocket != null)
        {
            playerRocket.transform.position = startPosition.transform.position + new Vector3(checkPointSpawnPositionOffsetX, startPointSpawnPositionOffsetY, 0f); ;
        }
        if (gameOverCanvas != null)
        {
            gameOverCanvas.enabled = false;
        }
        if (clearCanvas != null)
        {
            clearCanvas.enabled = false;
        }
        gameOverPanel.SetActive(false);
        pauseCanvas.enabled = false;
        rankingCanvas.enabled = false;
        currentGameState = GameState.CountDown;
        fromPanel = "";
        finalElapsedTime = "";
        Time.timeScale = 1f;
        remainingTime = maxRemainingTime;
        countDownTimer = startCountDownTime;
        playerNameText.text = PlayerNameTempSaveSolo.playerName;
        playerRocket.GetComponent<MovementSolo>().DisablePlayerControl();
        UpdatePlayerBoostText(); // For Debug
        finalPosition = "Start";
        finalPositionText.text = finalPosition;
        totalDistance = 0;
        // checkPointIndex: 0 = Start, 99 = Goal, 1 = Check1Point 1, 2 Check Point 2, etc.
        currentCheckPointIndex = 0;
        CalculatTotalDistance();
        ShowFinalPosition();
        CheckRemainingTime();
        InitializeRemainingTime();
        ReadRankingData();
    }

    private void Update()
    {
        UpdatePlayerLifeText();
        switch (currentGameState)
        {
            case GameState.CountDown:
                countDownTimeText.enabled = true;
                countDownTimer -= Time.deltaTime;
                CountDown(countDownTimer);
                break;
            case GameState.GameStart:
                UpdatePlayerBoostText(); // For Debug
                UpdatePlayerSpeedText();
                CalculateRemainingDistance();
                ShowFinalPosition();
                CheckRemainingTime();
                UpdateRemainingTimeAndTimeElapsed();
                RespondToPauseGame();
                break;
            case GameState.Win:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
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
            playerRocket.GetComponent<MovementSolo>().EnablePlayerControl();
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

    private void InitializeRemainingTime()
    {
        remainingTimeText.text = Mathf.FloorToInt(remainingTime).ToString();
    }

    private void ReadRankingData()
    {
        if (rankingDataFile == null) { return; }
        int index = 0;
        RankingJson rankingJson = JsonUtility.FromJson<RankingJson>(rankingDataFile.text);
        foreach(RankingData data in rankingJson.detail)
        {
            rankingPlayerNameText[index].text = data.playerName;
            rankingFinalPositionText[index].text = data.finalPosition.ToString();
            rankingRemainingTimeText[index].text = data.remainingTime.ToString();
            rankingTimeElaspedText[index].text = data.timeElapsed;
            index++;
        }
    }

    private void UpdateRemainingTimeAndTimeElapsed()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTimeText == null) { return; }
        if (remainingTime >= 0.0f)
        {
            remainingTimeText.text = Mathf.FloorToInt(remainingTime).ToString();
        }

        float t = Time.time - PlayTimeTempSaveSolo.totalTimeElapsed - startCountDownTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");
        string miliseconds = (t * 60 % 60).ToString("00");
        finalElapsedTime = minutes + ":" + seconds + ":" + miliseconds;

        if (timeElapsedText == null) { return; }
        timeElapsedText.text = finalElapsedTime;
    }

    private void CheckRemainingTime()
    {
        if (remainingTime <= 0.0f)
        {
            DisablePlayerMovement();
            DisablePlayerCollision();
            GameOver();
        }
    }

    public void RecoverRemainingTime(float recoverRemainingTime)
    {
        remainingTime += recoverRemainingTime;
        if(recoverRemainingTime >= maxRemainingTime)
        {
            recoverRemainingTime = maxRemainingTime;
        }
    }

    public void UpdateFinalPosition(string positionName, int checkPointIndex)
    {
       finalPosition = positionName;
       currentCheckPointIndex = checkPointIndex;
    }

    private void CalculatTotalDistance()
    {
        if (wayPoints.Length <= 0)
        {
            totalDistance = Vector3.Distance(startPosition.position, goalPosition.position);
        }
        else if (wayPoints.Length == 1)
        {
            totalDistance += Vector3.Distance(startPosition.position, wayPoints[0].transform.position);
            totalDistance += Vector3.Distance(goalPosition.position, wayPoints[0].transform.position);
        }
        else
        {
            for(var i = 0; i < wayPoints.Length - 1; i++)
            {
                totalDistance += Vector3.Distance(wayPoints[i].transform.position, wayPoints[i+1].transform.position);
            }
        }
        Debug.Log("Calculated Total Distance: "+totalDistance);
        stageMiniSlider.minValue = 0;
        stageMiniSlider.maxValue = totalDistance;
    }

    private void CalculateRemainingDistance()
    {
        Dictionary<int, float> ToPlayerDistances = new Dictionary<int, float>();
        for (var i = 0; i < wayPoints.Length; i++)
        {
            Debug.Log("i: "+i+", Distance: "+ Vector3.Distance(wayPoints[i].transform.position, playerRocket.transform.position)+", Direction: "+ (wayPoints[i].transform.position - playerRocket.transform.position).normalized);
            ToPlayerDistances.Add(i, Vector3.Distance(wayPoints[i].transform.position, playerRocket.transform.position));
        }
        int positionIndex = 0;
        float minDistance = Mathf.Infinity;
        foreach(KeyValuePair<int, float> distanceInfo in ToPlayerDistances)
        {
            if(distanceInfo.Value < minDistance)
            {
                positionIndex = distanceInfo.Key;
                minDistance = distanceInfo.Value;
            }
        }
        Debug.Log("positionIndex: " + positionIndex);

        float totalMovedDistance = 0f;
        if (positionIndex == 0)
        {
            totalMovedDistance += Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
        }
        else if (positionIndex == wayPoints.Length - 1)
        {
            for (var i = 0; i < positionIndex; i++)
            {
                totalMovedDistance += Vector3.Distance(wayPoints[i].transform.position, wayPoints[i+1].transform.position);
            }
            totalMovedDistance -= Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
        }
        else
        {
            for (var i = 0; i <= positionIndex - 1; i++)
            {
                totalMovedDistance += Vector3.Distance(wayPoints[i].transform.position, wayPoints[i + 1].transform.position);
            }

            string previousDirection = wayPoints[positionIndex].GetFromPreviousWaypointDirection();
            if (previousDirection == "Left")
            {
                if (playerRocket.transform.position.x <= wayPoints[positionIndex].transform.position.x)
                {
                    totalMovedDistance -= Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
                else
                {
                    totalMovedDistance += Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
            }
            else if (previousDirection == "Right")
            {
                if (playerRocket.transform.position.x >= wayPoints[positionIndex].transform.position.x)
                {
                    totalMovedDistance -= Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
                else
                {
                    totalMovedDistance += Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
            }
            else if (previousDirection == "Up")
            {
                if (playerRocket.transform.position.y <= wayPoints[positionIndex].transform.position.y)
                {
                    totalMovedDistance += Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
                else
                {
                    totalMovedDistance -= Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
            }
            else if (previousDirection == "Down")
            {
                if (playerRocket.transform.position.y >= wayPoints[positionIndex].transform.position.y)
                {
                    totalMovedDistance += Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
                else
                {
                    totalMovedDistance -= Vector3.Distance(wayPoints[positionIndex].transform.position, playerRocket.transform.position);
                }
            }
        }
        Debug.Log("totalMovedDistance: "+ totalMovedDistance);
        stageMiniSlider.value = totalMovedDistance;
    }

    private void ShowFinalPosition()
    {
        finalPositionText.text = finalPosition;
    }

    internal void SaveLatestCheckPoint(Transform checkPointPos)
    {
        lastCheckPointPosition = checkPointPos;
    }

    private void DisablePlayerMovement()
    {
        playerRocket.GetComponent<MovementSolo>().enabled = false;
    }
    private void DisablePlayerCollision()
    {
        playerRocket.GetComponent<CollisionHandlerSolo>().enabled = false;
    }

    private void UpdatePlayerLifeText()
    {
        if(playerLifeText == null) { return; }
        playerLifeText.text = playerRocket.GetComponent<PlayerStatusSolo>().GetCurrentLife().ToString();
    }

    private void UpdatePlayerSpeedText()
    {
        if(playerSpeedText == null) { return; }
        float currentSpeed;
        currentSpeed = Mathf.FloorToInt(playerRocket.GetComponent<Rigidbody>().velocity.magnitude);
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
        playerSpeedText.text = currentSpeed.ToString() + "Km / s";
    }

    private void UpdatePlayerBoostText()
    {
        if (playerBoostText == null) { return; }
        int playerBoost = Mathf.FloorToInt(playerRocket.GetComponent<PlayerStatusSolo>().GetCurrentBoost());
        if(playerBoost < 0)
        {
            playerBoost = 0;
        }
        playerBoostText.text = playerBoost.ToString();
    }
    private void RespondToPauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void ResetPlayerToStartPosition(GameObject player)
    {
        if(lastCheckPointPosition != null)
        {
            player.transform.position = lastCheckPointPosition.transform.position + new Vector3(checkPointSpawnPositionOffsetX, checkPointSpawnPositionOffsetY, 0f);
        }
        else if (startPosition != null)
        {
            player.transform.position = startPosition.transform.position + new Vector3(0f, startPointSpawnPositionOffsetY, 0f);
        }
        ResetPlayerRotation(player);
    }

    public void ResetPlayerRotation(GameObject player)
    {
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        player.GetComponent<MovementSolo>().StopMovement();
    }
    public void ActiviateAllFuelObjects()
    {
        foreach(GameObject fuel in fuelObjects)
        {
            if(fuel.GetComponent<Fuel>() != null)
            {
                fuel.GetComponent<Fuel>().ActivateFuelObject();
            }
        }
    }

    public void Clear()
    {
        UpdateRanking();
        if (clearCanvas == null) { return; }
        clearCanvas.enabled = true;
        StartCoroutine(ShowClearPanel());
        Instance.ChangeGameState(GameState.Win);
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

    public void GameOver()
    {
        UpdateRanking();
        if (gameOverCanvas == null) { return; }
        gameOverCanvas.enabled = true;
        StartCoroutine(ShowGameOverPanel());
        Instance.ChangeGameState(GameState.GameOver);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    private IEnumerator ShowClearPanel()
    {
        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
        }
        yield return new WaitForSeconds(showGameOverPanelTime);

        if (clearPanel2 != null)
        {
            clearPanel2.SetActive(true);
        }
    }
    private IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(showGameOverPanelTime);
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void ShowRankingPanelFromClear()
    {
        fromPanel = "Clear";
        if(clearPanel == null || rankingCanvas == null) { return; }
        clearPanel.SetActive(false);
        rankingCanvas.enabled = true;
    }

    public void ShowRankingPanelFromGameOver()
    {
        fromPanel = "GameOver";
        if (gameOverPanel == null || rankingCanvas == null) { return; }
        gameOverPanel.SetActive(false);
        rankingCanvas.enabled = true;
    }

    private void UpdateRanking()
    {
        RankingData rankingData = new RankingData();
        rankingData.playerName = PlayerNameTempSaveSolo.playerName;
        rankingData.finalPosition = finalPosition;
        rankingData.checkPointIndex = currentCheckPointIndex;
        rankingData.remainingTime = Mathf.FloorToInt(remainingTime);
        rankingData.timeElapsed = timeElapsedText.text;
        if (rankingDataFile == null) { return; }
        int index = 0;
        RankingJson rankingJson = JsonUtility.FromJson<RankingJson>(rankingDataFile.text);
        foreach (RankingData data in rankingJson.detail)
        {
            Debug.Log(rankingData.checkPointIndex+" --- "+data.checkPointIndex);
            if(rankingData.checkPointIndex >= data.checkPointIndex)
            {
                if(rankingData.checkPointIndex == data.checkPointIndex)
                {
                    if (rankingData.remainingTime <= data.remainingTime)
                    {
                        for (int j = rankingJson.detail.Length - 1; j > index; j--)
                        {
                            rankingJson.detail[j].playerName = rankingJson.detail[j - 1].playerName;
                            rankingJson.detail[j].finalPosition = rankingJson.detail[j - 1].finalPosition;
                            rankingJson.detail[j].remainingTime = rankingJson.detail[j - 1].remainingTime;
                            rankingJson.detail[j].timeElapsed = rankingJson.detail[j - 1].timeElapsed;
                        }
                        rankingJson.detail[index].playerName = rankingData.playerName;
                        rankingJson.detail[index].finalPosition = rankingData.finalPosition;
                        rankingJson.detail[index].remainingTime = rankingData.remainingTime;
                        rankingJson.detail[index].timeElapsed = rankingData.timeElapsed;
                        rankingPlayerNameText[index].text = rankingData.playerName;
                        rankingFinalPositionText[index].text = rankingData.finalPosition.ToString();
                        rankingRemainingTimeText[index].text = rankingData.remainingTime.ToString();
                        rankingTimeElaspedText[index].text = rankingData.timeElapsed;
                        break;
                    }
                }
                else
                {
                    for (int j = rankingJson.detail.Length - 1; j > index; j--)
                    {
                        rankingJson.detail[j].playerName = rankingJson.detail[j - 1].playerName;
                        rankingJson.detail[j].finalPosition = rankingJson.detail[j - 1].finalPosition;
                        rankingJson.detail[j].remainingTime = rankingJson.detail[j - 1].remainingTime;
                        rankingJson.detail[j].timeElapsed = rankingJson.detail[j - 1].timeElapsed;
                    }
                    rankingJson.detail[index].playerName = rankingData.playerName;
                    rankingJson.detail[index].finalPosition = rankingData.finalPosition;
                    rankingJson.detail[index].remainingTime = rankingData.remainingTime;
                    rankingJson.detail[index].timeElapsed = rankingData.timeElapsed;
                    rankingPlayerNameText[index].text = rankingData.playerName;
                    rankingFinalPositionText[index].text = rankingData.finalPosition.ToString();
                    rankingRemainingTimeText[index].text = rankingData.remainingTime.ToString();
                    rankingTimeElaspedText[index].text = rankingData.timeElapsed;
                    break;
                }
            }
            index++;
        }

        Debug.Log("Index: " + index);

        if (index >= rankingJson.detail.Length)
        {
            clearOutOfRankText.enabled = true;
            clearRankedLabel.enabled = false;
            clearRankedText.enabled = false;

            gameOverOutOfRankText.enabled = true;
            gameOverRankedLabel.enabled = false;
            gameOverRankedText.enabled = false;
        }
        else
        {
            clearOutOfRankText.enabled = false;
            clearRankedLabel.enabled = true;
            clearRankedText.enabled = true;

            gameOverOutOfRankText.enabled = false;
            gameOverRankedLabel.enabled = true;
            gameOverRankedText.enabled = true;
            switch (index)
            {
                case 0:
                    clearRankedText.text = "1st";
                    gameOverRankedText.text = "1st";
                    break;
                case 1:
                    clearRankedText.text = "2nd";
                    gameOverRankedText.text = "2nd";
                    break;
                case 2:
                    clearRankedText.text = "3rd";
                    gameOverRankedText.text = "3rd";
                    break;
                case 3:
                    clearRankedText.text = "4th";
                    gameOverRankedText.text = "4th";
                    break;
                case 4:
                    clearRankedText.text = "5th";
                    gameOverRankedText.text = "5th";
                    break;
            }
        }
        string saveRankingData = JsonUtility.ToJson(rankingJson, true);
        Debug.Log(saveRankingData);
        File.WriteAllText(Application.dataPath + "/Resources/ranking.json", saveRankingData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void ReturnFromRanking()
    {
        if (clearPanel == null || gameOverPanel == null || rankingCanvas == null) { return; }
        rankingCanvas.enabled = false;
        switch (fromPanel)
        {
            case "Clear":
                clearPanel.SetActive(true);
                break;
            case "GameOver":
                gameOverPanel.SetActive(true);
                break;
            default:
                gameOverPanel.SetActive(true);
                break;
        }
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
}
