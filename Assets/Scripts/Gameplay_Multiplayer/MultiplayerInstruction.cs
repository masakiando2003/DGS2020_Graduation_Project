using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerInstruction : MonoBehaviour
{
    [SerializeField] Localization multiplayerInstruction_EN, multiplayerInstruction_JP;
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
    [SerializeField] Text winConditionButtonText, controlButtonText, hintsButtonText, itemsButtonText;
    [SerializeField] Text hint1ButtonText, hint2ButtonText, hint3ButtonText, hint4ButtonText;
    [SerializeField] Text instructionTitleText, startButtonText, titleButtonText, pleaseWaitLabelText;
    [SerializeField] Text winConditionText, hint1Text, hint2Text, hint3Text, hint4Text;
    [SerializeField] Text attackItemText, defenceItemText, abnormalItemText;
    [SerializeField] Text boostCanItemText, reduceSpeedItemText, reduceBoostItemText;


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
        if(Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.Japanese;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                instructionTitleText.text = multiplayerInstruction_EN.GetLabelContent("InstructionTitleText");
                winConditionButtonText.text = multiplayerInstruction_EN.GetLabelContent("WinConditionButtonText");
                controlButtonText.text = multiplayerInstruction_EN.GetLabelContent("ControlButtonText");
                hintsButtonText.text = multiplayerInstruction_EN.GetLabelContent("HintsButtonText");
                itemsButtonText.text = multiplayerInstruction_EN.GetLabelContent("ItemsButtonText");
                hint1ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint1ButtonText");
                hint2ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint2ButtonText");
                hint3ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint3ButtonText");
                hint4ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint4ButtonText");
                startButtonText.text = multiplayerInstruction_EN.GetLabelContent("StartButtonText");
                titleButtonText.text = multiplayerInstruction_EN.GetLabelContent("TitleButtonText");
                pleaseWaitLabelText.text = multiplayerInstruction_EN.GetLabelContent("PleaseWaitLabelText");
                winConditionText.text = multiplayerInstruction_EN.GetLabelContent("WinConditionText").Replace("|", System.Environment.NewLine);
                hint1Text.text = multiplayerInstruction_EN.GetLabelContent("Hint1Text").Replace("|", System.Environment.NewLine);
                hint2Text.text = multiplayerInstruction_EN.GetLabelContent("Hint2Text").Replace("|", System.Environment.NewLine);
                hint3Text.text = multiplayerInstruction_EN.GetLabelContent("Hint3Text").Replace("|", System.Environment.NewLine);
                hint4Text.text = multiplayerInstruction_EN.GetLabelContent("Hint4Text").Replace("|", System.Environment.NewLine);
                attackItemText.text = multiplayerInstruction_EN.GetLabelContent("AttackItemText");
                defenceItemText.text = multiplayerInstruction_EN.GetLabelContent("DefenceItemText");
                abnormalItemText.text = multiplayerInstruction_EN.GetLabelContent("AbnormalItemText");
                boostCanItemText.text = multiplayerInstruction_EN.GetLabelContent("BoostCanItemText");
                reduceSpeedItemText.text = multiplayerInstruction_EN.GetLabelContent("ReduceSpeedItemText");
                reduceBoostItemText.text = multiplayerInstruction_EN.GetLabelContent("ReduceBoostItemText");
                break;
            case Language.DisplayLanauge.Japanese:
                instructionTitleText.text = multiplayerInstruction_JP.GetLabelContent("InstructionTitleText");
                winConditionButtonText.text = multiplayerInstruction_JP.GetLabelContent("WinConditionButtonText");
                winConditionButtonText.fontStyle = FontStyle.Bold;
                controlButtonText.text = multiplayerInstruction_JP.GetLabelContent("ControlButtonText");
                controlButtonText.fontStyle = FontStyle.Bold;
                hintsButtonText.text = multiplayerInstruction_JP.GetLabelContent("HintsButtonText");
                hintsButtonText.fontStyle = FontStyle.Bold;
                itemsButtonText.text = multiplayerInstruction_JP.GetLabelContent("ItemsButtonText");
                itemsButtonText.fontStyle = FontStyle.Bold;
                hint1ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint1ButtonText");
                hint1ButtonText.fontStyle = FontStyle.Bold;
                hint2ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint2ButtonText");
                hint2ButtonText.fontStyle = FontStyle.Bold;
                hint3ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint3ButtonText");
                hint3ButtonText.fontStyle = FontStyle.Bold;
                hint4ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint4ButtonText");
                hint4ButtonText.fontStyle = FontStyle.Bold;
                startButtonText.text = multiplayerInstruction_JP.GetLabelContent("StartButtonText");
                startButtonText.fontStyle = FontStyle.Bold;
                titleButtonText.text = multiplayerInstruction_JP.GetLabelContent("TitleButtonText");
                titleButtonText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = multiplayerInstruction_JP.GetLabelContent("PleaseWaitLabelText");
                winConditionText.text = multiplayerInstruction_JP.GetLabelContent("WinConditionText").Replace("|", System.Environment.NewLine);
                hint1Text.text = multiplayerInstruction_JP.GetLabelContent("Hint1Text").Replace("|", System.Environment.NewLine);
                hint2Text.text = multiplayerInstruction_JP.GetLabelContent("Hint2Text").Replace("|", System.Environment.NewLine);
                hint3Text.text = multiplayerInstruction_JP.GetLabelContent("Hint3Text").Replace("|", System.Environment.NewLine);
                hint4Text.text = multiplayerInstruction_JP.GetLabelContent("Hint4Text").Replace("|", System.Environment.NewLine);
                attackItemText.text = multiplayerInstruction_JP.GetLabelContent("AttackItemText");
                defenceItemText.text = multiplayerInstruction_JP.GetLabelContent("DefenceItemText");
                abnormalItemText.text = multiplayerInstruction_JP.GetLabelContent("AbnormalItemText");
                boostCanItemText.text = multiplayerInstruction_JP.GetLabelContent("BoostCanItemText");
                reduceSpeedItemText.text = multiplayerInstruction_JP.GetLabelContent("ReduceSpeedItemText");
                reduceBoostItemText.text = multiplayerInstruction_JP.GetLabelContent("ReduceBoostItemText");
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
