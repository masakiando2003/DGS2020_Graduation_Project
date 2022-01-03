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
    [SerializeField] GameObject hints1Section, hints2Section, hints3Section, hints4Section, hints5Section;
    [SerializeField] GameObject hint1ImageEN, hint2Image1EN, hint2Image2EN, hint3ImageEN, hint4ImageEN, hint5Image1EN, hint5Image2EN;
    [SerializeField] GameObject hint1ImageJP, hint2Image1JP, hint2Image2JP, hint3ImageJP, hint4ImageJP, hint5Image1JP, hint5Image2JP;
    [SerializeField] GameObject stageEasyImageEN, stageNormalImageEN, stageHardImageEN;
    [SerializeField] GameObject stageEasyImageJP, stageNormalImageJP, stageHardImageJP;
    [SerializeField] GameObject keyboardControlType1ImageEN, keyboardControlType1ImageJP, joystickImageEN, joystickImageJP;
    [SerializeField] GameObject keyboardControlType2mageEN, keyboardControlType2ImageJP;
    [SerializeField] string targetEasyMap, targetNormalMap, targetHardMap;
    [SerializeField] string titleMap;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text instructionTitleText, missionButtonText, controlsButtonText, hintsButtonText;
    [SerializeField] Text missionText, keyboardButtonText, joystickButtonText;
    [SerializeField] Text hint1ButtonText, hint2ButtonText, hint3ButtonText, hint4ButtonText, hint5ButtonText;
    [SerializeField] Text hint1Text, hint2Text, hint3Text, hint4Text, hint5Text;
    [SerializeField] Text startButtonText, rankingButtonText, titleButtonText, returnButtonText;
    [SerializeField] Text rankingLabelText, rankingPlayerNameLabelText, rankingPlayerFinalPositionText;
    [SerializeField] Text rankingPlayerRemainingTimeText, rankingPlayerTimeElapsedText, pleaseWaitLabelText;
    [SerializeField] Text[] rankingPlayerNameText;
    [SerializeField] Text[] rankingFinalPositionText;
    [SerializeField] Text[] rankingRemainingTimeText;
    [SerializeField] Text[] rankingTimeElaspedText;
    [SerializeField] TextAsset rankingDataEasyFile, rankingDataNormalFile, rankingDataHardFile;
    [SerializeField] Button btnMission, btnControl, btnHints, btnKeyboard, btnJoystick;
    [SerializeField] Button btnHints1, btnHints2, btnHints3, btnHints4, btnHints5;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

    TextAsset rankingDataFile;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
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
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
        hintsSection.SetActive(false);
        btnMission.GetComponent<Image>().color = buttonSelectedColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
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
                hint4ButtonText.text = instruction1P_EN.GetLabelContent("Hint4ButtonText");
                hint5ButtonText.text = instruction1P_EN.GetLabelContent("Hint5ButtonText");
                hint1Text.text = instruction1P_EN.GetLabelContent("Hint1Text").Replace("|", Environment.NewLine);
                hint2Text.text = instruction1P_EN.GetLabelContent("Hint2Text").Replace("|", Environment.NewLine);
                hint3Text.text = instruction1P_EN.GetLabelContent("Hint3Text").Replace("|", Environment.NewLine);
                hint4Text.text = instruction1P_EN.GetLabelContent("Hint4Text").Replace("|", Environment.NewLine);
                hint5Text.text = instruction1P_EN.GetLabelContent("Hint5Text").Replace("|", Environment.NewLine);
                hint1ImageEN.SetActive(true);
                hint2Image1EN.SetActive(true);
                hint2Image2EN.SetActive(true);
                hint3ImageEN.SetActive(true);
                hint4ImageEN.SetActive(true);
                hint5Image1EN.SetActive(true);
                hint5Image2EN.SetActive(true);
                hint1ImageJP.SetActive(false);
                hint2Image1JP.SetActive(false);
                hint2Image2JP.SetActive(false);
                hint3ImageJP.SetActive(false);
                hint4ImageJP.SetActive(false);
                hint5Image1JP.SetActive(false);
                hint5Image2JP.SetActive(false);
                switch (ControlType.chosenControlType){
                    case "Type1":
                        keyboardControlType1ImageEN.SetActive(true);
                        keyboardControlType1ImageJP.SetActive(false);
                        keyboardControlType2mageEN.SetActive(false);
                        keyboardControlType2ImageJP.SetActive(false);
                        break;
                    case "Type2":
                        keyboardControlType1ImageEN.SetActive(false);
                        keyboardControlType1ImageJP.SetActive(false);
                        keyboardControlType2mageEN.SetActive(true);
                        keyboardControlType2ImageJP.SetActive(false);
                        break;
                }
                joystickImageEN.SetActive(true);
                joystickImageJP.SetActive(false);
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
                instructionTitleText.fontStyle = FontStyle.Bold;
                missionButtonText.text = instruction1P_JP.GetLabelContent("MissionButtonText");
                missionButtonText.fontStyle = FontStyle.Bold;
                controlsButtonText.text = instruction1P_JP.GetLabelContent("ControlsButtonText");
                controlsButtonText.fontStyle = FontStyle.Bold;
                hintsButtonText.text = instruction1P_JP.GetLabelContent("HintsButtonText");
                hint1ButtonText.fontStyle = FontStyle.Bold;
                missionText.text = instruction1P_JP.GetLabelContent("MissionText").Replace("|", Environment.NewLine);
                missionText.fontStyle = FontStyle.Bold;
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
                hint4ButtonText.text = instruction1P_JP.GetLabelContent("Hint4ButtonText");
                hint4ButtonText.fontStyle = FontStyle.Bold;
                hint5ButtonText.text = instruction1P_JP.GetLabelContent("Hint5ButtonText");
                hint5ButtonText.fontStyle = FontStyle.Bold;
                hint1Text.text = instruction1P_JP.GetLabelContent("Hint1Text").Replace("|", Environment.NewLine);
                hint1Text.fontStyle = FontStyle.Bold;
                hint2Text.text = instruction1P_JP.GetLabelContent("Hint2Text").Replace("|", Environment.NewLine);
                hint2Text.fontStyle = FontStyle.Bold;
                hint3Text.text = instruction1P_JP.GetLabelContent("Hint3Text").Replace("|", Environment.NewLine);
                hint3Text.fontStyle = FontStyle.Bold;
                hint4Text.text = instruction1P_JP.GetLabelContent("Hint4Text").Replace("|", Environment.NewLine);
                hint4Text.fontStyle = FontStyle.Bold;
                hint5Text.text = instruction1P_JP.GetLabelContent("Hint5Text").Replace("|", Environment.NewLine);
                hint5Text.fontStyle = FontStyle.Bold;
                hint1ImageEN.SetActive(false);
                hint2Image1EN.SetActive(false);
                hint2Image2EN.SetActive(false);
                hint3ImageEN.SetActive(false);
                hint4ImageEN.SetActive(false);
                hint5Image1EN.SetActive(false);
                hint5Image2EN.SetActive(false);
                hint1ImageJP.SetActive(true);
                hint2Image1JP.SetActive(true);
                hint2Image2JP.SetActive(true);
                hint3ImageJP.SetActive(true);
                hint4ImageJP.SetActive(true);
                hint5Image1JP.SetActive(true);
                hint5Image2JP.SetActive(true);
                switch (ControlType.chosenControlType)
                {
                    case "Type1":
                        keyboardControlType1ImageEN.SetActive(false);
                        keyboardControlType1ImageJP.SetActive(true);
                        keyboardControlType2mageEN.SetActive(false);
                        keyboardControlType2ImageJP.SetActive(false);
                        break;
                    case "Type2":
                        keyboardControlType1ImageEN.SetActive(false);
                        keyboardControlType1ImageJP.SetActive(false);
                        keyboardControlType2mageEN.SetActive(false);
                        keyboardControlType2ImageJP.SetActive(true);
                        break;
                }
                joystickImageEN.SetActive(false);
                joystickImageJP.SetActive(true);
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
            if(PlayerPrefs.HasKey(Difficulty_1P_TempSave.chosenDifficulty + "_PlayerName_" + (index + 1)))
            {
                rankingPlayerNameText[index].text = PlayerPrefs.GetString(Difficulty_1P_TempSave.chosenDifficulty + "_PlayerName_" + (index + 1));
            }
            else
            {
                rankingPlayerNameText[index].text = data.playerName;
                PlayerPrefs.SetString(Difficulty_1P_TempSave.chosenDifficulty + "_PlayerName_" + (index+1), data.playerName);
            }
            rankingPlayerNameText[index].fontStyle = FontStyle.Bold;
            if (PlayerPrefs.HasKey(Difficulty_1P_TempSave.chosenDifficulty + "_FinalPosition_" + (index + 1)))
            {
                rankingFinalPositionText[index].text = PlayerPrefs.GetString(Difficulty_1P_TempSave.chosenDifficulty + "_FinalPosition_" + (index + 1));
            }
            else
            {
                rankingFinalPositionText[index].text = data.finalPosition;
                PlayerPrefs.SetString(Difficulty_1P_TempSave.chosenDifficulty + "_FinalPosition_" + (index + 1), data.finalPosition);
            }
            rankingFinalPositionText[index].fontStyle = FontStyle.Bold;
            if (PlayerPrefs.HasKey(Difficulty_1P_TempSave.chosenDifficulty + "_RemainingTime_" + (index + 1)))
            {
                rankingRemainingTimeText[index].text = PlayerPrefs.GetInt(Difficulty_1P_TempSave.chosenDifficulty + "_RemainingTime_" + (index + 1)).ToString();
            }
            else
            {
                rankingRemainingTimeText[index].text = data.remainingTime.ToString();
                PlayerPrefs.SetInt(Difficulty_1P_TempSave.chosenDifficulty + "_RemainingTime_" + (index + 1), data.remainingTime);
            }
            rankingRemainingTimeText[index].fontStyle = FontStyle.Bold;
            if (PlayerPrefs.HasKey(Difficulty_1P_TempSave.chosenDifficulty + "_TimeElapsed_" + (index + 1)))
            {
                rankingTimeElaspedText[index].text = PlayerPrefs.GetString(Difficulty_1P_TempSave.chosenDifficulty + "_TimeElapsed_" + (index + 1));
            }
            else
            {
                rankingTimeElaspedText[index].text = data.timeElapsed;
                PlayerPrefs.SetString(Difficulty_1P_TempSave.chosenDifficulty + "_TimeElapsed_" + (index + 1), data.timeElapsed);
            }
            rankingTimeElaspedText[index].fontStyle = FontStyle.Bold;
            index++;
        }
    }

    public void ShowInstruction()
    {
        instructionCanvas.SetActive(true);
        rankingCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonSelectedColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
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

        btnMission.GetComponent<Image>().color = buttonSelectedColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowControlsSection()
    {
        objectionSection.SetActive(false);
        hintsSection.SetActive(false);
        keyboardSection.SetActive(true);
        joystickSection.SetActive(false);
        controlsSection.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonSelectedColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnKeyboard.GetComponent<Image>().color = buttonSelectedColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowKeyboardSection()
    {
        joystickSection.SetActive(false);
        keyboardSection.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonSelectedColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnKeyboard.GetComponent<Image>().color = buttonSelectedColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowJoystickSection()
    {
        keyboardSection.SetActive(false);
        joystickSection.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonSelectedColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHintsSection()
    {
        objectionSection.SetActive(false);
        controlsSection.SetActive(false);
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
        hintsSection.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonSelectedColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints1Section()
    {
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonSelectedColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints2Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(true);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonSelectedColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints3Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(true);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonSelectedColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }
    public void ShowHints4Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(true);
        hints5Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonSelectedColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
    }
    public void ShowHints5Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnKeyboard.GetComponent<Image>().color = buttonNormalColor;
        btnJoystick.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonSelectedColor;
    }

    public void StartGame()
    {
        instructionCanvas.SetActive(false);
        rankingCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        switch (Difficulty_1P_TempSave.chosenDifficulty)
        {
            case "Easy":
                StartCoroutine(LoadLevelAsynchronously(targetEasyMap));
                break;
            case "Normal":
                StartCoroutine(LoadLevelAsynchronously(targetNormalMap));
                break;
            case "Hard":
                StartCoroutine(LoadLevelAsynchronously(targetHardMap));
                break;
        }
    }

    IEnumerator LoadLevelAsynchronously(string targetMap)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetMap);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            //float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log("operation progress: "+operation.progress);
            loadingSlider.value = operation.progress;

            yield return 0;
        }
        operation.allowSceneActivation = true;
        loadingSlider.value = 1f;
        yield return operation;
    }

    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
