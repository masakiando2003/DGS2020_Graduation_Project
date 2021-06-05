using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManagerSolo;

public class Instructions : MonoBehaviour
{
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] GameObject rankingCanvas;
    [SerializeField] string targetMap;
    [SerializeField] string titleMap;
    [SerializeField] Text[] rankingPlayerNameText;
    [SerializeField] Text[] rankingFinalPositionText;
    [SerializeField] Text[] rankingRemainingTimeText;
    [SerializeField] Text[] rankingTimeElaspedText;
    [SerializeField] TextAsset rankingDataFile;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        instructionCanvas.SetActive(true);
        rankingCanvas.SetActive(false);
        ReadRankingData();
    }

    private void ReadRankingData()
    {
        if (rankingDataFile == null) { return; }
        int index = 0;
        RankingJson rankingJson = JsonUtility.FromJson<RankingJson>(rankingDataFile.text);
        foreach (RankingData data in rankingJson.detail)
        {
            rankingPlayerNameText[index].text = data.playerName;
            rankingFinalPositionText[index].text = data.finalPosition;
            rankingRemainingTimeText[index].text = data.remainingTime.ToString();
            rankingTimeElaspedText[index].text = data.timeElapsed;
            index++;
        }
    }

    public void ShowInstruction()
    {
        instructionCanvas.SetActive(true);
        rankingCanvas.SetActive(false);
    }

    public void ShowRanking()
    {
        rankingCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
    }

    public void StartGame()
    {
        if(targetMap.Equals("")) { return; }
        SceneManager.LoadScene(targetMap);
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
