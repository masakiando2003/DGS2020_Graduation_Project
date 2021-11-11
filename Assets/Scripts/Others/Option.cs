using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManagerSolo;

public class Option : MonoBehaviour
{
    [SerializeField] Localization option_EN, option_JP;
    [SerializeField] Text optionTitleText, titleButtonText;
    [SerializeField] Text easyButtonText, normalButtonText, hardButtonText;
    [SerializeField] Text bgmVolumeText, seVolumeText, soloPlayRankingDataText;
    [SerializeField] Text bgmVolumeValueText, seVolumeValueText;
    [SerializeField] Text rankingTitleEasyLabelText, rankingTitleNormalLabelText, rankingTitleHardLabelText;
    [SerializeField] Text rankingEasyReturnButtonText, rankingNormalReturnButtonText, rankingHardReturnButtonText;
    [SerializeField] Text rankingEasyResetDataButtonText, rankingNormalResetDataButtonText, rankingHardResetDataButtonText;
    [SerializeField] Text rankingEasyPlayerNameLabelText, rankingEasyFinalPositionLabelText, rankingEasyRemainingTimeLabelText, rankingvEasyTimeElapsedLabelText;
    [SerializeField] Text rankingNormalPlayerNameLabelText, rankingNormalFinalPositionLabelText, rankingNormalRemainingTimeLabelText, rankingNormalTimeElapsedLabelText;
    [SerializeField] Text rankingHardPlayerNameLabelText, rankingHardFinalPositionLabelText, rankingHardRemainingTimeLabelText, rankingHardTimeElapsedLabelText;
    [SerializeField] Text confirmResetDataLabelText, resetDataYesButtonText, resetDataNoButtonText;
    [SerializeField] Slider bgmVolumeSlider, seVolumeSlider;
    [SerializeField] GameObject mainOptionCanvas, soloEasyRankingCanvas, soloNormalRankingCanvas, soloHardRankingCanvas, resetRankingDataCanvas;
    [SerializeField] TextAsset rankingDataEasyFile, rankingDataNormalFile, rankingDataHardFile;
    [SerializeField] TextAsset rankingDefaultDataEasyFile, rankingDefaultDataNormalFile, rankingDefaultDataHardFile;
    [SerializeField] Text[] rankingEasyPlayerNameText, rankingEasyFinalPositionText, rankingEasyRemainingTimeText, rankingEasyTimeElaspedText;
    [SerializeField] Text[] rankingNormalPlayerNameText, rankingNormalFinalPositionText, rankingNormalRemainingTimeText, rankingNormalTimeElaspedText;
    [SerializeField] Text[] rankingHardPlayerNameText, rankingHardFinalPositionText, rankingHardRemainingTimeText, rankingHardTimeElaspedText;


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


    string selectedDifficulty;
    int numPlayers;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        numPlayers = 5;
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.Japanese;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                optionTitleText.text = option_EN.GetLabelContent("OptionTitleText");
                titleButtonText.text = option_EN.GetLabelContent("TitleButtonText");
                easyButtonText.text = option_EN.GetLabelContent("EasyButtonText");
                normalButtonText.text = option_EN.GetLabelContent("NormalButtonText");
                hardButtonText.text = option_EN.GetLabelContent("HardButtonText");
                bgmVolumeText.text = option_EN.GetLabelContent("BGMVolumeText");
                seVolumeText.text = option_EN.GetLabelContent("SEVolumeText");
                soloPlayRankingDataText.text = option_EN.GetLabelContent("SoloPlayRankingDataText").Replace("|", System.Environment.NewLine);
                rankingTitleEasyLabelText.text = option_EN.GetLabelContent("RankingTitleEasyLabelText");
                rankingTitleNormalLabelText.text = option_EN.GetLabelContent("RankingTitleNormalLabelText");
                rankingTitleHardLabelText.text = option_EN.GetLabelContent("RankingTitleHardLabelText");
                rankingEasyReturnButtonText.text = option_EN.GetLabelContent("ReturnButtonText");
                rankingNormalReturnButtonText.text = option_EN.GetLabelContent("ReturnButtonText");
                rankingHardReturnButtonText.text = option_EN.GetLabelContent("ReturnButtonText");
                rankingEasyPlayerNameLabelText.text = option_EN.GetLabelContent("PlayerNameText");
                rankingEasyFinalPositionLabelText.text = option_EN.GetLabelContent("FinalPositionText");
                rankingEasyRemainingTimeLabelText.text = option_EN.GetLabelContent("RemainingTimeText");
                rankingvEasyTimeElapsedLabelText.text = option_EN.GetLabelContent("TimeElapsedText");
                rankingNormalPlayerNameLabelText.text = option_EN.GetLabelContent("PlayerNameText");
                rankingNormalFinalPositionLabelText.text = option_EN.GetLabelContent("FinalPositionText");
                rankingNormalRemainingTimeLabelText.text = option_EN.GetLabelContent("RemainingTimeText");
                rankingNormalTimeElapsedLabelText.text = option_EN.GetLabelContent("TimeElapsedText");
                rankingHardPlayerNameLabelText.text = option_EN.GetLabelContent("PlayerNameText");
                rankingHardFinalPositionLabelText.text = option_EN.GetLabelContent("FinalPositionText");
                rankingHardRemainingTimeLabelText.text = option_EN.GetLabelContent("RemainingTimeText");
                rankingHardTimeElapsedLabelText.text = option_EN.GetLabelContent("TimeElapsedText");
                rankingEasyResetDataButtonText.text = option_EN.GetLabelContent("ResetDataButtonText");
                rankingNormalResetDataButtonText.text = option_EN.GetLabelContent("ResetDataButtonText");
                rankingHardResetDataButtonText.text = option_EN.GetLabelContent("ResetDataButtonText");
                resetDataYesButtonText.text = option_EN.GetLabelContent("YesButtonText");
                resetDataNoButtonText.text = option_EN.GetLabelContent("NoButtonText");
                break;
            case Language.DisplayLanauge.Japanese:
                optionTitleText.text = option_JP.GetLabelContent("OptionTitleText");
                titleButtonText.text = option_JP.GetLabelContent("TitleButtonText");
                titleButtonText.fontStyle = FontStyle.Bold;
                easyButtonText.text = option_JP.GetLabelContent("EasyButtonText");
                easyButtonText.fontStyle = FontStyle.Bold;
                normalButtonText.text = option_JP.GetLabelContent("NormalButtonText");
                normalButtonText.fontStyle = FontStyle.Bold;
                hardButtonText.text = option_JP.GetLabelContent("HardButtonText");
                hardButtonText.fontStyle = FontStyle.Bold;
                bgmVolumeText.text = option_JP.GetLabelContent("BGMVolumeText");
                seVolumeText.text = option_JP.GetLabelContent("SEVolumeText");
                soloPlayRankingDataText.text = option_JP.GetLabelContent("SoloPlayRankingDataText").Replace("|", System.Environment.NewLine);
                rankingTitleEasyLabelText.text = option_JP.GetLabelContent("RankingTitleEasyLabelText");
                rankingTitleNormalLabelText.text = option_JP.GetLabelContent("RankingTitleNormalLabelText");
                rankingTitleHardLabelText.text = option_JP.GetLabelContent("RankingTitleHardLabelText");
                rankingEasyReturnButtonText.text = option_JP.GetLabelContent("ReturnButtonText");
                rankingEasyReturnButtonText.fontStyle = FontStyle.Bold;
                rankingNormalReturnButtonText.text = option_JP.GetLabelContent("ReturnButtonText");
                rankingNormalReturnButtonText.fontStyle = FontStyle.Bold;
                rankingHardReturnButtonText.text = option_JP.GetLabelContent("ReturnButtonText");
                rankingHardReturnButtonText.fontStyle = FontStyle.Bold;
                rankingEasyPlayerNameLabelText.text = option_JP.GetLabelContent("PlayerNameText");
                rankingEasyFinalPositionLabelText.text = option_JP.GetLabelContent("FinalPositionText");
                rankingEasyRemainingTimeLabelText.text = option_JP.GetLabelContent("RemainingTimeText");
                rankingvEasyTimeElapsedLabelText.text = option_JP.GetLabelContent("TimeElapsedText");
                rankingNormalPlayerNameLabelText.text = option_JP.GetLabelContent("PlayerNameText");
                rankingNormalFinalPositionLabelText.text = option_JP.GetLabelContent("FinalPositionText");
                rankingNormalRemainingTimeLabelText.text = option_JP.GetLabelContent("RemainingTimeText");
                rankingNormalTimeElapsedLabelText.text = option_JP.GetLabelContent("TimeElapsedText");
                rankingHardPlayerNameLabelText.text = option_JP.GetLabelContent("PlayerNameText");
                rankingHardFinalPositionLabelText.text = option_JP.GetLabelContent("FinalPositionText");
                rankingHardRemainingTimeLabelText.text = option_JP.GetLabelContent("RemainingTimeText");
                rankingHardTimeElapsedLabelText.text = option_JP.GetLabelContent("TimeElapsedText");
                rankingEasyResetDataButtonText.text = option_JP.GetLabelContent("ResetDataButtonText");
                rankingEasyResetDataButtonText.fontStyle = FontStyle.Bold;
                rankingNormalResetDataButtonText.text = option_JP.GetLabelContent("ResetDataButtonText");
                rankingNormalResetDataButtonText.fontStyle = FontStyle.Bold;
                rankingHardResetDataButtonText.text = option_JP.GetLabelContent("ResetDataButtonText");
                rankingHardResetDataButtonText.fontStyle = FontStyle.Bold;
                resetDataYesButtonText.text = option_JP.GetLabelContent("YesButtonText");
                resetDataYesButtonText.fontStyle = FontStyle.Bold;
                resetDataNoButtonText.text = option_JP.GetLabelContent("NoButtonText");
                resetDataNoButtonText.fontStyle = FontStyle.Bold;
                break;
        }
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            int bgmVolume = PlayerPrefs.GetInt("BGMVolume");
            bgmVolumeSlider.value = bgmVolume;

        }
        if (PlayerPrefs.HasKey("SEVolume"))
        {
            int seVolume = PlayerPrefs.GetInt("SEVolume");
            seVolumeSlider.value = seVolume;

        }
        ReadRankingData("Easy");
        ReadRankingData("Normal");
        ReadRankingData("Hard");
        mainOptionCanvas.SetActive(true);
        soloEasyRankingCanvas.SetActive(false);
        soloNormalRankingCanvas.SetActive(false);
        soloHardRankingCanvas.SetActive(false);
        resetRankingDataCanvas.SetActive(false);
        selectedDifficulty = "Easy";
    }

    public void ToTitle(string titleMap)
    {
        SceneManager.LoadScene(titleMap);
    }

    public void ChangeBGMVolume()
    {
        PlayerPrefs.SetInt("BGMVolume", (int)bgmVolumeSlider.value);
        bgmVolumeValueText.text = bgmVolumeSlider.value.ToString();
        BGMController.SetBGMVolume((int)bgmVolumeSlider.value);
    }
    public void ChangeSEVolume()
    {
        PlayerPrefs.SetInt("SEVolume", (int)seVolumeSlider.value);
        seVolumeValueText.text = seVolumeSlider.value.ToString();
        BGMController.SetSEVolume((int)seVolumeSlider.value);
    }

    public void Show1PlayerRankingData(string difficulty)
    {
        mainOptionCanvas.SetActive(false);
        switch (difficulty)
        {
            case "Easy":
                soloNormalRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(false);
                soloEasyRankingCanvas.SetActive(true);
                break;
            case "Normal":
                soloEasyRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(false);
                soloNormalRankingCanvas.SetActive(true);
                break;
            case "Hard":
                soloEasyRankingCanvas.SetActive(false);
                soloNormalRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(true);
                break;
        }
    }

    private void ReadRankingData(string difficulty)
    {
        int index = 0;
        TextAsset rankingDataFile = null;
        switch (difficulty)
        {
            case "Easy":
                rankingDataFile = rankingDataEasyFile;
                break;
            case "Normal":
                rankingDataFile = rankingDataNormalFile;
                break;
            case "Hard":
                rankingDataFile = rankingDataHardFile;
                break;
        }
        RankingJson rankingJson = JsonUtility.FromJson<RankingJson>(rankingDataFile.text);
        foreach (RankingData data in rankingJson.detail)
        {
            switch (difficulty)
            {
                case "Easy":
                    rankingEasyPlayerNameText[index].text = data.playerName;
                    rankingEasyPlayerNameText[index].fontStyle = FontStyle.Bold;
                    switch (Language.gameDisplayLanguage)
                    {
                        case Language.DisplayLanauge.English:
                            rankingEasyFinalPositionText[index].text = data.finalPosition.Replace("Goal", option_EN.GetLabelContent("GoalText"))
                                                                                         .Replace("Check Point", option_EN.GetLabelContent("CheckPointText"))
                                                                                         .Replace("Start", option_EN.GetLabelContent("StartText"));
                            break;
                        case Language.DisplayLanauge.Japanese:
                            rankingEasyFinalPositionText[index].text = data.finalPosition.Replace("Goal", option_JP.GetLabelContent("GoalText"))
                                                                                         .Replace("Check Point", option_JP.GetLabelContent("CheckPointText"))
                                                                                         .Replace("Start", option_JP.GetLabelContent("StartText"));
                            rankingEasyFinalPositionText[index].fontStyle = FontStyle.Bold;
                            break;
                    }
                    rankingEasyRemainingTimeText[index].text = data.remainingTime.ToString();
                    rankingEasyTimeElaspedText[index].text = data.timeElapsed;
                    break;
                case "Normal":
                    rankingNormalPlayerNameText[index].text = data.playerName;
                    rankingNormalPlayerNameText[index].fontStyle = FontStyle.Bold;
                    switch (Language.gameDisplayLanguage)
                    {
                        case Language.DisplayLanauge.English:
                            rankingNormalFinalPositionText[index].text = data.finalPosition.Replace("Goal", option_EN.GetLabelContent("GoalText"))
                                                                                         .Replace("Check Point", option_EN.GetLabelContent("CheckPointText"))
                                                                                         .Replace("Start", option_EN.GetLabelContent("StartText"));
                            break;
                        case Language.DisplayLanauge.Japanese:
                            rankingNormalFinalPositionText[index].text = data.finalPosition.Replace("Goal", option_JP.GetLabelContent("GoalText"))
                                                                                         .Replace("Check Point", option_JP.GetLabelContent("CheckPointText"))
                                                                                         .Replace("Start", option_JP.GetLabelContent("StartText"));
                            rankingNormalFinalPositionText[index].fontStyle = FontStyle.Bold;
                            break;
                    }
                    rankingNormalRemainingTimeText[index].text = data.remainingTime.ToString();
                    rankingNormalTimeElaspedText[index].text = data.timeElapsed;
                    break;
                case "Hard":
                    rankingHardPlayerNameText[index].text = data.playerName;
                    rankingHardPlayerNameText[index].fontStyle = FontStyle.Bold;
                    switch (Language.gameDisplayLanguage)
                    {
                        case Language.DisplayLanauge.English:
                            rankingHardFinalPositionText[index].text = data.finalPosition.Replace("Goal", option_EN.GetLabelContent("GoalText"))
                                                                                         .Replace("Check Point", option_EN.GetLabelContent("CheckPointText"))
                                                                                         .Replace("Start", option_EN.GetLabelContent("StartText"));
                            break;
                        case Language.DisplayLanauge.Japanese:
                            rankingHardFinalPositionText[index].text = data.finalPosition.Replace("Goal", option_JP.GetLabelContent("GoalText"))
                                                                                         .Replace("Check Point", option_JP.GetLabelContent("CheckPointText"))
                                                                                         .Replace("Start", option_JP.GetLabelContent("StartText"));
                            rankingHardFinalPositionText[index].fontStyle = FontStyle.Bold;
                            break;
                    }
                    rankingHardRemainingTimeText[index].text = data.remainingTime.ToString();
                    rankingHardTimeElaspedText[index].text = data.timeElapsed;
                    break;
            }
            index++;
        }
    }

    public void ReturnToMainOption()
    {
        resetRankingDataCanvas.SetActive(false);
        soloEasyRankingCanvas.SetActive(false);
        soloNormalRankingCanvas.SetActive(false);
        soloHardRankingCanvas.SetActive(false);
        mainOptionCanvas.SetActive(true);
    }

    public void ConfirmResetData(string difficulty)
    {
        selectedDifficulty = difficulty;
        mainOptionCanvas.SetActive(false);
        soloEasyRankingCanvas.SetActive(false);
        soloNormalRankingCanvas.SetActive(false);
        soloHardRankingCanvas.SetActive(false);
        resetRankingDataCanvas.SetActive(true);
        switch (difficulty)
        {
            case "Easy":
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        confirmResetDataLabelText.text = option_EN.GetLabelContent("ConfirmResetDataText") + "\"" + option_EN.GetLabelContent("EasyButtonText") + "\"?";
                        break;
                    case Language.DisplayLanauge.Japanese:
                        confirmResetDataLabelText.text = "\"" + option_JP.GetLabelContent("EasyButtonText") + "\"" + option_JP.GetLabelContent("ConfirmResetDataText") +  "?";
                        break;
                }
                break;
            case "Normal":
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        confirmResetDataLabelText.text = option_EN.GetLabelContent("ConfirmResetDataText") + "\"" + option_EN.GetLabelContent("NormalButtonText") + "\"?";
                        break;
                    case Language.DisplayLanauge.Japanese:
                        confirmResetDataLabelText.text = "\"" + option_JP.GetLabelContent("NormalButtonText") + "\"" + option_JP.GetLabelContent("ConfirmResetDataText") + "?";
                        break;
                }
                break;
            case "Hard":
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        confirmResetDataLabelText.text = option_EN.GetLabelContent("ConfirmResetDataText") + "\"" + option_EN.GetLabelContent("HardButtonText") + "\"?";
                        break;
                    case Language.DisplayLanauge.Japanese:
                        confirmResetDataLabelText.text = "\"" + option_JP.GetLabelContent("HardButtonText") + "\"" + option_JP.GetLabelContent("ConfirmResetDataText") + "?";
                        break;
                }
                break;
        }

    }
    public void CancelResetData()
    {
        resetRankingDataCanvas.SetActive(false);
        mainOptionCanvas.SetActive(false);
        switch (selectedDifficulty)
        {
            case "Easy":
                soloNormalRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(false);
                soloEasyRankingCanvas.SetActive(true);
                break;
            case "Normal":
                soloEasyRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(false);
                soloNormalRankingCanvas.SetActive(true);
                break;
            case "Hard":
                soloEasyRankingCanvas.SetActive(false);
                soloNormalRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(true);
                break;
        }
    }

    public void ResetData()
    {
        TextAsset rankingDataFile = null;
        switch (selectedDifficulty)
        {
            case "Easy":
                rankingDataFile = rankingDefaultDataEasyFile;
                break;
            case "Normal":
                rankingDataFile = rankingDefaultDataNormalFile;
                break;
            case "Hard":
                rankingDataFile = rankingDefaultDataHardFile;
                break;
        }
        RankingJson rankingJson = JsonUtility.FromJson<RankingJson>(rankingDataFile.text);
        string saveRankingData = JsonUtility.ToJson(rankingJson, true);
        Debug.Log(saveRankingData);
        for (int i = 1; i <= numPlayers; i++)
        {
            string playerNameKey = Difficulty_1P_TempSave.chosenDifficulty + "_PlayerName_" + i.ToString();
            PlayerPrefs.SetString(playerNameKey, rankingJson.detail[i - 1].playerName);
            string playerFinalPositionKey = Difficulty_1P_TempSave.chosenDifficulty + "_FinalPosition_" + i.ToString();
            PlayerPrefs.SetString(playerFinalPositionKey, rankingJson.detail[i - 1].finalPosition);
            string playerCheckPointIndexKey = Difficulty_1P_TempSave.chosenDifficulty + "_CheckPointIndex_" + i.ToString();
            PlayerPrefs.SetInt(playerCheckPointIndexKey, rankingJson.detail[i - 1].checkPointIndex);
            string playerRemainingTimeKey = Difficulty_1P_TempSave.chosenDifficulty + "_RemainingTime_" + i.ToString();
            PlayerPrefs.SetInt(playerRemainingTimeKey, rankingJson.detail[i - 1].remainingTime);
            string playerTimeElapsedKey = Difficulty_1P_TempSave.chosenDifficulty + "_TimeElapsed_" + i.ToString();
            PlayerPrefs.SetString(playerTimeElapsedKey, rankingJson.detail[i - 1].timeElapsed);
        }
        /*
        File.WriteAllText(Application.dataPath + "/Resources/ranking_1P_" + selectedDifficulty + ".json", saveRankingData);
        #if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        #endif
        */
        resetRankingDataCanvas.SetActive(false);
        switch (selectedDifficulty)
        {
            case "Easy":
                ReadRankingData("Easy");
                soloNormalRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(false);
                soloEasyRankingCanvas.SetActive(true);
                break;
            case "Normal":
                ReadRankingData("Normal");
                soloEasyRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(false);
                soloNormalRankingCanvas.SetActive(true);
                break;
            case "Hard":
                ReadRankingData("Hard");
                soloEasyRankingCanvas.SetActive(false);
                soloNormalRankingCanvas.SetActive(false);
                soloHardRankingCanvas.SetActive(true);
                break;
        }
    }
}
