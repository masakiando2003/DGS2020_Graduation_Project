using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficultySolo : MonoBehaviour
{
    [SerializeField] Localization chooseDifficultySolo_EN, chooseDifficultySolo_JP;
    [SerializeField] GameObject difficultyCanvas, pleaseWaitCanvas;
    [SerializeField] string introductionScene;
    [SerializeField] string titleMap;
    [SerializeField] string difficulty = "Easy";
    [SerializeField] Text difficultyText, difficultyTitleText, easyText, normalText, hardText, proceedText, titleText, pleaseWaitLabelText;
    [SerializeField] Image stageEasyViewImage, stageNormalViewImage, stageHardViewImage;

    Image stageView;

    private void Awake()
    {
        Initialization();
        SwitchLanguage();
    }

    private void Initialization()
    {
        difficultyCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
    }
    private void SwitchLanguage()
    {
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                difficultyTitleText.text = chooseDifficultySolo_EN.GetLabelContent("ChosenDifficultyText");
                easyText.text = chooseDifficultySolo_EN.GetLabelContent("EasyText");
                normalText.text = chooseDifficultySolo_EN.GetLabelContent("NormalText");
                hardText.text = chooseDifficultySolo_EN.GetLabelContent("HardText");
                proceedText.text = chooseDifficultySolo_EN.GetLabelContent("ProceedText");
                titleText.text = chooseDifficultySolo_EN.GetLabelContent("TitleText");
                pleaseWaitLabelText.text = chooseDifficultySolo_EN.GetLabelContent("PleaseWaitLabelText");
                break;
            case Language.DisplayLanauge.Japanese:
                difficultyTitleText.text = chooseDifficultySolo_JP.GetLabelContent("ChosenDifficultyText");
                easyText.text = chooseDifficultySolo_JP.GetLabelContent("EasyText");
                easyText.fontStyle = FontStyle.Bold;
                normalText.text = chooseDifficultySolo_JP.GetLabelContent("NormalText");
                normalText.fontStyle = FontStyle.Bold;
                hardText.text = chooseDifficultySolo_JP.GetLabelContent("HardText");
                hardText.fontStyle = FontStyle.Bold;
                proceedText.text = chooseDifficultySolo_JP.GetLabelContent("ProceedText");
                proceedText.fontStyle = FontStyle.Bold;
                titleText.text = chooseDifficultySolo_JP.GetLabelContent("TitleText");
                titleText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = chooseDifficultySolo_JP.GetLabelContent("PleaseWaitLabelText");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficultyText();
        UpdateStageImageView();
    }

    private void UpdateDifficultyText()
    {
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                difficultyText.text = chooseDifficultySolo_EN.GetLabelContent(difficulty+"Text");
                break;
            case Language.DisplayLanauge.Japanese:
                difficultyText.text = chooseDifficultySolo_JP.GetLabelContent(difficulty + "Text");
                break;
        }
    }

    private void UpdateStageImageView()
    {
        switch (difficulty)
        {
            case "Easy":
                stageEasyViewImage.enabled = true;
                stageNormalViewImage.enabled = false;
                stageHardViewImage.enabled = false;
                break;
            case "Normal":
                stageNormalViewImage.enabled = true;
                stageEasyViewImage.enabled = false;
                stageHardViewImage.enabled = false;
                break;
            case "Hard":
                stageHardViewImage.enabled = true;
                stageEasyViewImage.enabled = false;
                stageNormalViewImage.enabled = false;
                break;
        }
    }

    public void SetDifficulty(string chosenDifficulty)
    {
        difficulty = chosenDifficulty;
        Difficulty_1P_TempSave.chosenDifficulty = chosenDifficulty;
    }

    public void ProceedToIntroduction()
    {
        if (introductionScene.Equals("")) { return; }
        difficultyCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        SceneManager.LoadScene(introductionScene);
    }

    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
