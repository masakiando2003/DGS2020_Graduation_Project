using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficultySolo : MonoBehaviour
{
    [SerializeField] Localization chooseDifficultySolo_EN, chooseDifficultySolo_JP;
    [SerializeField] Font chooseDifficultySoloENFont, chooseDifficultySoloJPFont;
    [SerializeField] GameObject difficultyCanvas, pleaseWaitCanvas;
    [SerializeField] int estimatedEasyTime = 1, estimatedNormalTime = 3, estimatedHardTime = 7;
    [SerializeField] string introductionScene;
    [SerializeField] string titleMap;
    [SerializeField] string difficulty = "Easy";
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text difficultyText, difficultyTitleText, easyText, normalText, hardText, proceedText, titleText, pleaseWaitLabelText;
    [SerializeField] Text estimatedTimeLabelText, estimatedTimeText;
    [SerializeField] Image stageEasyViewImage, stageNormalViewImage, stageHardViewImage;
    [SerializeField] Button btnEasy, btnNormal, btnHard;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

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
                difficultyTitleText.font = chooseDifficultySoloENFont;
                easyText.text = chooseDifficultySolo_EN.GetLabelContent("EasyText");
                easyText.font = chooseDifficultySoloENFont;
                normalText.text = chooseDifficultySolo_EN.GetLabelContent("NormalText");
                normalText.font = chooseDifficultySoloENFont;
                hardText.text = chooseDifficultySolo_EN.GetLabelContent("HardText");
                hardText.font = chooseDifficultySoloENFont;
                proceedText.text = chooseDifficultySolo_EN.GetLabelContent("ProceedText");
                proceedText.font = chooseDifficultySoloENFont;
                titleText.text = chooseDifficultySolo_EN.GetLabelContent("TitleText");
                titleText.font = chooseDifficultySoloENFont;
                pleaseWaitLabelText.text = chooseDifficultySolo_EN.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = chooseDifficultySoloENFont;
                estimatedTimeLabelText.text = chooseDifficultySolo_EN.GetLabelContent("EstimatedTimeLabelText");
                estimatedTimeLabelText.font = chooseDifficultySoloENFont;
                estimatedTimeText.text = estimatedEasyTime.ToString() + " " + chooseDifficultySolo_EN.GetLabelContent("EstimatedTimeText");
                estimatedTimeText.font = chooseDifficultySoloENFont;
                break;
            case Language.DisplayLanauge.Japanese:
                difficultyTitleText.text = chooseDifficultySolo_JP.GetLabelContent("ChosenDifficultyText");
                difficultyTitleText.font = chooseDifficultySoloJPFont;
                difficultyTitleText.fontStyle = FontStyle.Bold;
                easyText.text = chooseDifficultySolo_JP.GetLabelContent("EasyText");
                easyText.font = chooseDifficultySoloJPFont;
                easyText.fontStyle = FontStyle.Bold;
                normalText.text = chooseDifficultySolo_JP.GetLabelContent("NormalText");
                normalText.font = chooseDifficultySoloJPFont;
                normalText.fontStyle = FontStyle.Bold;
                hardText.text = chooseDifficultySolo_JP.GetLabelContent("HardText");
                hardText.font = chooseDifficultySoloJPFont;
                hardText.fontStyle = FontStyle.Bold;
                proceedText.text = chooseDifficultySolo_JP.GetLabelContent("ProceedText");
                proceedText.font = chooseDifficultySoloJPFont;
                proceedText.fontStyle = FontStyle.Bold;
                titleText.text = chooseDifficultySolo_JP.GetLabelContent("TitleText");
                titleText.font = chooseDifficultySoloJPFont;
                titleText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = chooseDifficultySolo_JP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = chooseDifficultySoloJPFont;
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                estimatedTimeLabelText.text = chooseDifficultySolo_JP.GetLabelContent("EstimatedTimeLabelText");
                estimatedTimeLabelText.font = chooseDifficultySoloJPFont;
                estimatedTimeLabelText.fontStyle = FontStyle.Bold;
                estimatedTimeText.text = estimatedEasyTime.ToString() + chooseDifficultySolo_JP.GetLabelContent("EstimatedTimeText");
                estimatedTimeText.font = chooseDifficultySoloJPFont;
                estimatedTimeText.fontStyle = FontStyle.Bold;
                break;
        }
        btnEasy.GetComponent<Image>().color = buttonSelectedColor;
        btnNormal.GetComponent<Image>().color = buttonNormalColor;
        btnHard.GetComponent<Image>().color = buttonNormalColor;
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
                difficultyText.font = chooseDifficultySoloENFont;
                break;
            case Language.DisplayLanauge.Japanese:
                difficultyText.text = chooseDifficultySolo_JP.GetLabelContent(difficulty + "Text");
                difficultyText.font = chooseDifficultySoloJPFont;
                difficultyText.fontStyle = FontStyle.Bold;
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
                btnEasy.GetComponent<Image>().color = buttonSelectedColor;
                btnNormal.GetComponent<Image>().color = buttonNormalColor;
                btnHard.GetComponent<Image>().color = buttonNormalColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        estimatedTimeText.text = estimatedEasyTime.ToString() + " " + chooseDifficultySolo_EN.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.font = chooseDifficultySoloENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        estimatedTimeText.text = estimatedEasyTime.ToString() + chooseDifficultySolo_JP.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.font = chooseDifficultySoloJPFont;
                        estimatedTimeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            case "Normal":
                stageNormalViewImage.enabled = true;
                stageEasyViewImage.enabled = false;
                stageHardViewImage.enabled = false;
                btnEasy.GetComponent<Image>().color = buttonNormalColor;
                btnNormal.GetComponent<Image>().color = buttonSelectedColor;
                btnHard.GetComponent<Image>().color = buttonNormalColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        estimatedTimeText.text = estimatedNormalTime.ToString() + " " + chooseDifficultySolo_EN.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.font = chooseDifficultySoloENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        estimatedTimeText.text = estimatedNormalTime.ToString() + chooseDifficultySolo_JP.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.font = chooseDifficultySoloJPFont;
                        estimatedTimeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            case "Hard":
                stageHardViewImage.enabled = true;
                stageEasyViewImage.enabled = false;
                stageNormalViewImage.enabled = false;
                btnEasy.GetComponent<Image>().color = buttonNormalColor;
                btnNormal.GetComponent<Image>().color = buttonNormalColor;
                btnHard.GetComponent<Image>().color = buttonSelectedColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        estimatedTimeText.text = estimatedHardTime.ToString() + " " + chooseDifficultySolo_EN.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.font = chooseDifficultySoloENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        estimatedTimeText.text = estimatedHardTime.ToString() + chooseDifficultySolo_JP.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.font = chooseDifficultySoloJPFont;
                        estimatedTimeText.fontStyle = FontStyle.Bold;
                        break;
                }
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
        StartCoroutine(LoadLevelAsynchronously(introductionScene));
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
