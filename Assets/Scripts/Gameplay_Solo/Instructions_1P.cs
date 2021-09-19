using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManagerSolo;

public class Instructions_1P : MonoBehaviour
{
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] GameObject rankingCanvas;
    [SerializeField] GameObject pleaseWaitCanvas;
    [SerializeField] GameObject objectionSection, controlsSection, hintsSection;
    [SerializeField] GameObject keyboardSection, joystickSection;
    [SerializeField] GameObject hints1Section, hints2Section, hints3Section;
    [SerializeField] GameObject stageEasyImage, stageNormalImage, stageHardImage;
    [SerializeField] string targetEasyMap, targetNormalMap, targetHardMap;
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
        pleaseWaitCanvas.SetActive(false);
        objectionSection.SetActive(true);
        controlsSection.SetActive(false);
        keyboardSection.SetActive(false);
        joystickSection.SetActive(false);
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hintsSection.SetActive(false);
        switch (Difficulty_1P_TempSave.chosenDifficulty)
        {
            case "Easy":
                stageEasyImage.SetActive(true);
                stageNormalImage.SetActive(false);
                stageHardImage.SetActive(false);
                break;
            case "Normal":
                stageEasyImage.SetActive(false);
                stageNormalImage.SetActive(true);
                stageHardImage.SetActive(false);
                break;
            case "Hard":
                stageEasyImage.SetActive(false);
                stageNormalImage.SetActive(false);
                stageHardImage.SetActive(true);
                break;
        }
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
        pleaseWaitCanvas.SetActive(false);
    }

    public void ShowRanking()
    {
        rankingCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(false);
    }

    public void ShowObjectiveSection()
    {
        controlsSection.SetActive(false);
        hintsSection.SetActive(false);
        objectionSection.SetActive(true);
    }

    public void ShowControlsSection()
    {
        objectionSection.SetActive(false);
        hintsSection.SetActive(false);
        keyboardSection.SetActive(true);
        joystickSection.SetActive(false);
        controlsSection.SetActive(true);
    }

    public void ShowKeyboardSection()
    {
        joystickSection.SetActive(false);
        keyboardSection.SetActive(true);
    }

    public void ShowJoystickSection()
    {
        keyboardSection.SetActive(false);
        joystickSection.SetActive(true);
    }

    public void ShowHintsSection()
    {
        objectionSection.SetActive(false);
        controlsSection.SetActive(false);
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hintsSection.SetActive(true);
    }

    public void ShowHints1Section()
    {
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
    }

    public void ShowHints2Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(true);
        hints3Section.SetActive(false);
    }

    public void ShowHints3Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(true);
    }

    public void StartGame()
    {
        instructionCanvas.SetActive(false);
        rankingCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        switch (Difficulty_1P_TempSave.chosenDifficulty)
        {
            case "Easy":
                SceneManager.LoadScene(targetEasyMap);
                break;
            case "Normal":
                SceneManager.LoadScene(targetNormalMap);
                break;
            case "Hard":
                SceneManager.LoadScene(targetHardMap);
                break;
        }
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
