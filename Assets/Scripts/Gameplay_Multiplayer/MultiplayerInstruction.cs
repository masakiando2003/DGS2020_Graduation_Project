using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerInstruction : MonoBehaviour
{
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] GameObject pleaseWaitCanvas;
    [SerializeField] GameObject objectionSection, controlsSection, hintsSection, itemsSection;
    [SerializeField] GameObject hints1Section, hints2Section, hints3Section, hints4Section;
    [SerializeField] GameObject stageEasyViewImageObj, stageNormalViewImageObj, stageHardViewImageObj;
    [SerializeField] Image stageEasyViewImage, stageNormalViewImage, stageHardViewImage;
    [SerializeField] Image stageEasy2PViewImage, stageNormal2PViewImage, stageHard2PViewImage;
    [SerializeField] Image stageEasy3PViewImage, stageNormal3PViewImage, stageHard3PViewImage;
    [SerializeField] Image stageEasy4PViewImage, stageNormal4PViewImage, stageHard4PViewImage;
    [SerializeField] string titleMap;


    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        instructionCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
        objectionSection.SetActive(true);
        controlsSection.SetActive(false);
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hintsSection.SetActive(false);
        itemsSection.SetActive(false);
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
        switch (MultiplayPlayerMode.chosenDifficulty)
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

    public void StartGame()
    {
        switch (MultiplayPlayerMode.chosenDifficulty)
        {
            case "Easy":
                LoadMapBaseOnSettings("Easy", PlayerNameTempSaveMultiplay.numPlayers);
                break;
            case "Normal":
                LoadMapBaseOnSettings("Normal", PlayerNameTempSaveMultiplay.numPlayers);
                break;
            case "Hard":
                LoadMapBaseOnSettings("Hard", PlayerNameTempSaveMultiplay.numPlayers);
                break;
        }
    }

    public void ShowInstruction()
    {
        pleaseWaitCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    public void ShowObjectiveSection()
    {
        controlsSection.SetActive(false);
        hintsSection.SetActive(false);
        itemsSection.SetActive(false);
        objectionSection.SetActive(true);
    }

    public void ShowControlsSection()
    {
        objectionSection.SetActive(false);
        hintsSection.SetActive(false);
        itemsSection.SetActive(false);
        controlsSection.SetActive(true);
    }

    public void ShowHintsSection()
    {
        objectionSection.SetActive(false);
        controlsSection.SetActive(false);
        itemsSection.SetActive(false);
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hintsSection.SetActive(true);
    }

    public void ShowHints1Section()
    {
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
    }

    public void ShowHints2Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(true);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
    }

    public void ShowHints3Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(true);
        hints4Section.SetActive(false);
    }

    public void ShowHints4Section()
    {
        hints4Section.SetActive(true);
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
    }

    public void ShowItemsSection()
    {
        objectionSection.SetActive(false);
        hintsSection.SetActive(false);
        controlsSection.SetActive(false);
        itemsSection.SetActive(true);
    }

    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }

    private void LoadMapBaseOnSettings(string difficulty, int numOfPlayers)
    {
        instructionCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        //Debug.Log("difficulty: "+ difficulty+", numOfPlayers: "+numOfPlayers+", game mode"+MultiplayPlayerMode.gameMode);
        string mapName = "Multiplay_Stage_" + difficulty + "_" + numOfPlayers + "P";
        SceneManager.LoadScene(mapName);
    }
}
