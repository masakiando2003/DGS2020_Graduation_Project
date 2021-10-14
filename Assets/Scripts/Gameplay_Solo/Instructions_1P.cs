using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManagerSolo;

public class Instructions_1P : MonoBehaviour
{
    [SerializeField] Localization instruction1P_EN, instruction1P_JP;
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] GameObject rankingCanvas;
    [SerializeField] GameObject pleaseWaitCanvas;
    [SerializeField] GameObject objectionSection, controlsSection, hintsSection;
    [SerializeField] GameObject keyboardSection, joystickSection;
    [SerializeField] GameObject hints1Section, hints2Section, hints3Section;
    [SerializeField] GameObject stageEasyImageEN, stageNormalImageEN, stageHardImageEN;
    [SerializeField] GameObject stageEasyImageJP, stageNormalImageJP, stageHardImageJP;
    [SerializeField] string targetEasyMap, targetNormalMap, targetHardMap;
    [SerializeField] string titleMap;
    [SerializeField] Text instructionTitleText, missionButtonText, controlsButtonText, hintsButtonText;
    [SerializeField] Text missionText, keyboardButtonText, joystickButtonText;
    [SerializeField] Text hint1ButtonText, hint2ButtonText, hint3ButtonText, hint1Text, hint2Text, hint3Text;
    [SerializeField] Text startButtonText, rankingButtonText, titleButtonText, returnButtonText;
    [SerializeField] Text rankingLabelText, rankingPlayerNameLabelText, rankingPlayerFinalPositionText;
    [SerializeField] Text rankingPlayerRemainingTimeText, rankingPlayerTimeElapsedText, pleaseWaitLabelText;
    [SerializeField] Text[] rankingPlayerNameText;
    [SerializeField] Text[] rankingFinalPositionText;
    [SerializeField] Text[] rankingRemainingTimeText;
    [SerializeField] Text[] rankingTimeElaspedText;
    [SerializeField] TextAsset rankingDataEasyFile, rankingDataNormalFile, rankingDataHardFile;

    TextAsset rankingDataFile;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.Japanese;
        }
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
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        stageEasyImageEN.SetActive(true);
                        stageEasyImageJP.SetActive(false);
                        break;
                    case Language.DisplayLanauge.Japanese:
                        stageEasyImageJP.SetActive(true);
                        stageEasyImageEN.SetActive(false);
                        break;
                }
                stageNormalImageEN.SetActive(false);
                stageNormalImageJP.SetActive(false);
                stageHardImageEN.SetActive(false);
                stageHardImageJP.SetActive(false);
                rankingDataFile = rankingDataEasyFile;
                break;
            case "Normal":
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        stageNormalImageEN.SetActive(true);
                        stageNormalImageJP.SetActive(false);
                        break;
                    case Language.DisplayLanauge.Japanese:
                        stageNormalImageJP.SetActive(true);
                        stageNormalImageEN.SetActive(false);
                        break;
                }
                stageEasyImageEN.SetActive(false);
                stageEasyImageJP.SetActive(false);
                stageHardImageEN.SetActive(false);
                stageHardImageJP.SetActive(false);
                rankingDataFile = rankingDataNormalFile;
                break;
            case "Hard":
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        stageHardImageEN.SetActive(true);
                        stageHardImageJP.SetActive(false);
                        break;
                    case Language.DisplayLanauge.Japanese:
                        stageHardImageJP.SetActive(true);
                        stageHardImageEN.SetActive(false);
                        break;
                }
                stageEasyImageEN.SetActive(false);
                stageEasyImageJP.SetActive(false);
                stageNormalImageEN.SetActive(false);
                stageNormalImageJP.SetActive(false);
                rankingDataFile = rankingDataHardFile;
                break;
        }
        ReadRankingData(rankingDataFile);
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                instructionTitleText.text = instruction1P_EN.GetLabelContent("InstructionTitleText");
                missionButtonText.text = instruction1P_EN.GetLabelContent("MissionButtonText");
                controlsButtonText.text = instruction1P_EN.GetLabelContent("ControlsButtonText");
                hintsButtonText.text = instruction1P_EN.GetLabelContent("HintsButtonText");
                missionText.text = instruction1P_EN.GetLabelContent("MissionText").Replace("|", Environment.NewLine);
                keyboardButtonText.text = instruction1P_EN.GetLabelContent("KeyboardButtonText");
                joystickButtonText.text = instruction1P_EN.GetLabelContent("JoystickButtonText");
                hint1ButtonText.text = instruction1P_EN.GetLabelContent("Hint1ButtonText");
                hint2ButtonText.text = instruction1P_EN.GetLabelContent("Hint2ButtonText");
                hint3ButtonText.text = instruction1P_EN.GetLabelContent("Hint3ButtonText");
                hint1Text.text = instruction1P_EN.GetLabelContent("Hint1Text").Replace("|", Environment.NewLine);
                hint2Text.text = instruction1P_EN.GetLabelContent("Hint2Text").Replace("|", Environment.NewLine);
                hint3Text.text = instruction1P_EN.GetLabelContent("Hint3Text").Replace("|", Environment.NewLine);
                startButtonText.text = instruction1P_EN.GetLabelContent("StartButtonText");
                rankingButtonText.text = instruction1P_EN.GetLabelContent("RankingButtonText");
                titleButtonText.text = instruction1P_EN.GetLabelContent("TitleButtonText");
                returnButtonText.text = instruction1P_EN.GetLabelContent("ReturnButtonText");
                rankingLabelText.text = instruction1P_EN.GetLabelContent("RankingLabelText");
                rankingPlayerNameLabelText.text = instruction1P_EN.GetLabelContent("RankingPlayerNameLabelText");
                rankingPlayerFinalPositionText.text = instruction1P_EN.GetLabelContent("RankingPlayerFinalPositionText");
                rankingPlayerRemainingTimeText.text = instruction1P_EN.GetLabelContent("RankingPlayerRemainingTimeText");
                rankingPlayerTimeElapsedText.text = instruction1P_EN.GetLabelContent("RankingPlayerTimeElapsedText");
                pleaseWaitLabelText.text = instruction1P_EN.GetLabelContent("PleaseWaitText");
                break;
            case Language.DisplayLanauge.Japanese:
                instructionTitleText.text = instruction1P_JP.GetLabelContent("InstructionTitleText");
                missionButtonText.text = instruction1P_JP.GetLabelContent("MissionButtonText");
                missionButtonText.fontStyle = FontStyle.Bold;
                controlsButtonText.text = instruction1P_JP.GetLabelContent("ControlsButtonText");
                controlsButtonText.fontStyle = FontStyle.Bold;
                hintsButtonText.text = instruction1P_JP.GetLabelContent("HintsButtonText");
                hint1ButtonText.fontStyle = FontStyle.Bold;
                missionText.text = instruction1P_JP.GetLabelContent("MissionText").Replace("|", Environment.NewLine);
                keyboardButtonText.text = instruction1P_JP.GetLabelContent("KeyboardButtonText");
                keyboardButtonText.fontStyle = FontStyle.Bold;
                joystickButtonText.text = instruction1P_JP.GetLabelContent("JoystickButtonText");
                joystickButtonText.fontStyle = FontStyle.Bold;
                hint1ButtonText.text = instruction1P_JP.GetLabelContent("Hint1ButtonText");
                hint1ButtonText.fontStyle = FontStyle.Bold;
                hint2ButtonText.text = instruction1P_JP.GetLabelContent("Hint2ButtonText");
                hint2ButtonText.fontStyle = FontStyle.Bold;
                hint3ButtonText.text = instruction1P_JP.GetLabelContent("Hint3ButtonText");
                hint3ButtonText.fontStyle = FontStyle.Bold;
                hint1Text.text = instruction1P_JP.GetLabelContent("Hint1Text").Replace("|", Environment.NewLine);
                hint2Text.text = instruction1P_JP.GetLabelContent("Hint2Text").Replace("|", Environment.NewLine);
                hint3Text.text = instruction1P_JP.GetLabelContent("Hint3Text").Replace("|", Environment.NewLine);
                startButtonText.text = instruction1P_JP.GetLabelContent("StartButtonText");
                startButtonText.fontStyle = FontStyle.Bold;
                rankingButtonText.text = instruction1P_JP.GetLabelContent("RankingButtonText");
                rankingButtonText.fontStyle = FontStyle.Bold;
                titleButtonText.text = instruction1P_JP.GetLabelContent("TitleButtonText");
                titleButtonText.fontStyle = FontStyle.Bold;
                returnButtonText.text = instruction1P_JP.GetLabelContent("ReturnButtonText");
                returnButtonText.fontStyle = FontStyle.Bold;
                rankingLabelText.text = instruction1P_JP.GetLabelContent("RankingLabelText");
                rankingPlayerNameLabelText.text = instruction1P_JP.GetLabelContent("RankingPlayerNameLabelText");
                rankingPlayerFinalPositionText.text = instruction1P_JP.GetLabelContent("RankingPlayerFinalPositionText");
                rankingPlayerRemainingTimeText.text = instruction1P_JP.GetLabelContent("RankingPlayerRemainingTimeText");
                rankingPlayerTimeElapsedText.text = instruction1P_JP.GetLabelContent("RankingPlayerTimeElapsedText");
                pleaseWaitLabelText.text = instruction1P_JP.GetLabelContent("PleaseWaitText");
                break;
        }
    }

    private void ReadRankingData(TextAsset rankingDataFile)
    {
        if (rankingDataFile == null) { return; }
        int index = 0;
        RankingJson rankingJson = JsonUtility.FromJson<RankingJson>(rankingDataFile.text);
        foreach (RankingData data in rankingJson.detail)
        {
            rankingPlayerNameText[index].text = data.playerName;
            rankingPlayerNameText[index].fontStyle = FontStyle.Bold;
            rankingFinalPositionText[index].text = data.finalPosition;
            rankingFinalPositionText[index].fontStyle = FontStyle.Bold;
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
