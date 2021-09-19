using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficultyMultiplayer : MonoBehaviour
{
    [SerializeField] GameObject difficultyCanvas, pleaseWaitCanvas;
    [SerializeField] string introductionScene;
    [SerializeField] string titleMap;
    [SerializeField] string difficulty = "Easy";
    [SerializeField] Text difficultyText;
    [SerializeField] GameObject stageEasyViewImageObj, stageNormalViewImageObj, stageHardViewImageObj;
    [SerializeField] Image stageEasyViewImage, stageNormalViewImage, stageHardViewImage;
    [SerializeField] Image stageEasy2PViewImage, stageNormal2PViewImage, stageHard2PViewImage;
    [SerializeField] Image stageEasy3PViewImage, stageNormal3PViewImage, stageHard3PViewImage;
    [SerializeField] Image stageEasy4PViewImage, stageNormal4PViewImage, stageHard4PViewImage;

    Image stageView;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        difficultyCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficultyText();
        UpdateStageImageView();
    }

    private void UpdateDifficultyText()
    {
        difficultyText.text = difficulty;
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
                break;
            case "Normal":
                stageEasyViewImageObj.SetActive(false);
                stageHardViewImageObj.SetActive(false);
                stageNormalViewImageObj.SetActive(true);
                break;
            case "Hard":
                stageEasyViewImageObj.SetActive(false);
                stageNormalViewImageObj.SetActive(false);
                stageHardViewImageObj.SetActive(true);
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
        SceneManager.LoadScene(introductionScene);
    }

    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        difficultyCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        SceneManager.LoadScene(titleMap);
    }
}
