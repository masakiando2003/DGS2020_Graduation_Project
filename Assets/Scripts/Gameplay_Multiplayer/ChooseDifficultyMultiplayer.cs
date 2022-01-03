using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficultyMultiplayer : MonoBehaviour
{
    [SerializeField] Localization chooseDifficultyMultiplay_EN, chooseDifficultyMultiplay_JP;
    [SerializeField] GameObject difficultyCanvas, pleaseWaitCanvas;
    [SerializeField] int estimatedEasyTime = 2, estimatedNormalTime = 5, estimatedHardTime = 10;
    [SerializeField] string introductionScene;
    [SerializeField] string titleMap;
    [SerializeField] string difficulty = "Easy";
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text chooseDifficultyTitleText, difficultyText, pleaseWaitLabelText;
    [SerializeField] Text easyButtonText, normalButtonText, hardButtonText, proceedButtonText, titleButtonText;
    [SerializeField] Text estimatedTimeLabelText, estimatedTimeText;
    [SerializeField] GameObject stageEasyViewImageObj, stageNormalViewImageObj, stageHardViewImageObj;
    [SerializeField] Image stageEasyViewImage, stageNormalViewImage, stageHardViewImage;
    [SerializeField] Image stageEasy2PViewImage, stageNormal2PViewImage, stageHard2PViewImage;
    [SerializeField] Image stageEasy3PViewImage, stageNormal3PViewImage, stageHard3PViewImage;
    [SerializeField] Image stageEasy4PViewImage, stageNormal4PViewImage, stageHard4PViewImage;
    [SerializeField] Button btnEasy, btnNormal, btnHard;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

    Image stageView;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        difficultyCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
        if(Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                chooseDifficultyTitleText.text = chooseDifficultyMultiplay_EN.GetLabelContent("ChooseDifficultyTitleText");
                pleaseWaitLabelText.text = chooseDifficultyMultiplay_EN.GetLabelContent("PleaseWaitLabelText");
                easyButtonText.text = chooseDifficultyMultiplay_EN.GetLabelContent("EasyButtonText");
                normalButtonText.text = chooseDifficultyMultiplay_EN.GetLabelContent("NormalButtonText");
                hardButtonText.text = chooseDifficultyMultiplay_EN.GetLabelContent("HardButtonText");
                proceedButtonText.text = chooseDifficultyMultiplay_EN.GetLabelContent("ProceedButtonText");
                titleButtonText.text = chooseDifficultyMultiplay_EN.GetLabelContent("TitleButtonText");
                estimatedTimeLabelText.text = chooseDifficultyMultiplay_EN.GetLabelContent("EstimatedTimeLabelText");
                estimatedTimeText.text = estimatedEasyTime.ToString() + " " + chooseDifficultyMultiplay_EN.GetLabelContent("EstimatedTimeText");
                break;
            case Language.DisplayLanauge.Japanese:
                chooseDifficultyTitleText.text = chooseDifficultyMultiplay_JP.GetLabelContent("ChooseDifficultyTitleText");
                pleaseWaitLabelText.text = chooseDifficultyMultiplay_JP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                easyButtonText.text = chooseDifficultyMultiplay_JP.GetLabelContent("EasyButtonText");
                easyButtonText.fontStyle = FontStyle.Bold;
                normalButtonText.text = chooseDifficultyMultiplay_JP.GetLabelContent("NormalButtonText");
                normalButtonText.fontStyle = FontStyle.Bold;
                hardButtonText.text = chooseDifficultyMultiplay_JP.GetLabelContent("HardButtonText");
                hardButtonText.fontStyle = FontStyle.Bold;
                proceedButtonText.text = chooseDifficultyMultiplay_JP.GetLabelContent("ProceedButtonText");
                proceedButtonText.fontStyle = FontStyle.Bold;
                titleButtonText.text = chooseDifficultyMultiplay_JP.GetLabelContent("TitleButtonText");
                titleButtonText.fontStyle = FontStyle.Bold;
                estimatedTimeLabelText.text = chooseDifficultyMultiplay_JP.GetLabelContent("EstimatedTimeLabelText");
                estimatedTimeLabelText.fontStyle = FontStyle.Bold;
                estimatedTimeText.text = estimatedEasyTime.ToString() + chooseDifficultyMultiplay_JP.GetLabelContent("EstimatedTimeText");
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
                difficultyText.text = difficulty;
                break;
            case Language.DisplayLanauge.Japanese:
                switch (difficulty)
                {
                    case "Easy":
                        difficultyText.text = chooseDifficultyMultiplay_JP.GetLabelContent("EasyButtonText");
                        break;
                    case "Normal":
                        difficultyText.text = chooseDifficultyMultiplay_JP.GetLabelContent("NormalButtonText");
                        break;
                    case "Hard":
                        difficultyText.text = chooseDifficultyMultiplay_JP.GetLabelContent("HardButtonText");
                        break;
                }
                break;
        }
    }

    private void UpdateStageImageView()
    {
        //Debug.Log("Num of players: "+ PlayerNameTempSaveMultiplay.numPlayers+ ", difficulty: "+ difficulty);
        switch (PlayerNameTempSaveMultiplay.numPlayers)
        {
            case 2:
                stageEasyViewImage.sprite = stageEasy2PViewImage.sprite;
                stageNormalViewImage.sprite = stageNormal2PViewImage.sprite;
                stageHardViewImage.sprite = stageHard2PViewImage.sprite;
                break;
            case 3:
                stageEasyViewImage.sprite = stageEasy3PViewImage.sprite;
                stageNormalViewImage.sprite = stageNormal3PViewImage.sprite;
                stageHardViewImage.sprite = stageHard3PViewImage.sprite;
                break;
            case 4:
                stageEasyViewImage.sprite = stageEasy4PViewImage.sprite;
                stageNormalViewImage.sprite = stageNormal4PViewImage.sprite;
                stageHardViewImage.sprite = stageHard4PViewImage.sprite;
                break;
        }
        switch (difficulty)
        {
            case "Easy":
                stageNormalViewImageObj.SetActive(false);
                stageHardViewImageObj.SetActive(false);
                stageEasyViewImageObj.SetActive(true);
                btnEasy.GetComponent<Image>().color = buttonSelectedColor;
                btnNormal.GetComponent<Image>().color = buttonNormalColor;
                btnHard.GetComponent<Image>().color = buttonNormalColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        estimatedTimeText.text = estimatedEasyTime.ToString() + " " + chooseDifficultyMultiplay_EN.GetLabelContent("EstimatedTimeText");
                        break;
                    case Language.DisplayLanauge.Japanese:
                        estimatedTimeText.text = estimatedEasyTime.ToString() + chooseDifficultyMultiplay_JP.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            case "Normal":
                stageEasyViewImageObj.SetActive(false);
                stageHardViewImageObj.SetActive(false);
                stageNormalViewImageObj.SetActive(true);
                btnEasy.GetComponent<Image>().color = buttonNormalColor;
                btnNormal.GetComponent<Image>().color = buttonSelectedColor;
                btnHard.GetComponent<Image>().color = buttonNormalColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        estimatedTimeText.text = estimatedNormalTime.ToString() + " " + chooseDifficultyMultiplay_EN.GetLabelContent("EstimatedTimeText");
                        break;
                    case Language.DisplayLanauge.Japanese:
                        estimatedTimeText.text = estimatedNormalTime.ToString() + chooseDifficultyMultiplay_JP.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            case "Hard":
                stageEasyViewImageObj.SetActive(false);
                stageNormalViewImageObj.SetActive(false);
                stageHardViewImageObj.SetActive(true);
                btnEasy.GetComponent<Image>().color = buttonNormalColor;
                btnNormal.GetComponent<Image>().color = buttonNormalColor;
                btnHard.GetComponent<Image>().color = buttonSelectedColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        estimatedTimeText.text = estimatedHardTime.ToString() + " " + chooseDifficultyMultiplay_EN.GetLabelContent("EstimatedTimeText");
                        break;
                    case Language.DisplayLanauge.Japanese:
                        estimatedTimeText.text = estimatedNormalTime.ToString() + chooseDifficultyMultiplay_JP.GetLabelContent("EstimatedTimeText");
                        estimatedTimeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
        }
    }

    public void SetDifficulty(string chosenDifficulty)
    {
        difficulty = chosenDifficulty;
        MultiplayPlayerMode.chosenDifficulty = chosenDifficulty;
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
        difficultyCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        SceneManager.LoadScene(titleMap);
    }
}
