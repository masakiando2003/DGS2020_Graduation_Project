using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficultySolo : MonoBehaviour
{
    [SerializeField] string introductionScene;
    [SerializeField] string titleMap;
    [SerializeField] string difficulty = "Easy";
    [SerializeField] Text difficultyText;
    [SerializeField] Image stageEasyViewImage, stageNormalViewImage, stageHardViewImage;

    Image stageView;

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
        SceneManager.LoadScene(introductionScene);
    }

    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
