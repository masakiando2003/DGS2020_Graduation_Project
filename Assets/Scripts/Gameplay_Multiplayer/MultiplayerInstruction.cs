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
    [SerializeField] GameObject hints1Section, hints2Section, hints3Section, hints4Section, hints5Section;
    [SerializeField] GameObject joystickImageEN, joystickImageJP;
    [SerializeField] GameObject hint1ImageEN, hint2Image1EN, hint2Image2EN, hint3ImageEN, hint4ImageEN, hint5ImageEN;
    [SerializeField] GameObject hint1ImageJP, hint2Image1JP, hint2Image2JP, hint3ImageJP, hint4ImageJP, hint5ImageJP;
    [SerializeField] GameObject stageEasyViewImageObj, stageNormalViewImageObj, stageHardViewImageObj;
    [SerializeField] Image stageEasyViewImage, stageNormalViewImage, stageHardViewImage;
    [SerializeField] Image stageEasy2PViewImageEN, stageNormal2PViewImageEN, stageHard2PViewImageEN;
    [SerializeField] Image stageEasy3PViewImageEN, stageNormal3PViewImageEN, stageHard3PViewImageEN;
    [SerializeField] Image stageEasy4PViewImageEN, stageNormal4PViewImageEN, stageHard4PViewImageEN;
    [SerializeField] Image stageEasy2PViewImageJP, stageNormal2PViewImageJP, stageHard2PViewImageJP;
    [SerializeField] Image stageEasy3PViewImageJP, stageNormal3PViewImageJP, stageHard3PViewImageJP;
    [SerializeField] Image stageEasy4PViewImageJP, stageNormal4PViewImageJP, stageHard4PViewImageJP;
    [SerializeField] string titleMap;
    [SerializeField] Text winConditionButtonText, controlButtonText, hintsButtonText, itemsButtonText;
    [SerializeField] Text hint1ButtonText, hint2ButtonText, hint3ButtonText, hint4ButtonText, hint5ButtonText;
    [SerializeField] Text instructionTitleText, startButtonText, titleButtonText, pleaseWaitLabelText;
    [SerializeField] Text winConditionText, hint1Text, hint2Text, hint3Text, hint4Text, hint5Text;
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
        hints5Section.SetActive(false);
        hintsSection.SetActive(false);
        itemsSection.SetActive(false);
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.Japanese;
        }
        switch (PlayerNameTempSaveMultiplay.numPlayers)
        {
            case 2:
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        stageEasyViewImage.sprite = stageEasy2PViewImageEN.sprite;
                        stageNormalViewImage.sprite = stageNormal2PViewImageEN.sprite;
                        stageHardViewImage.sprite = stageHard2PViewImageEN.sprite;
                        stageEasy2PViewImageJP.enabled = false;
                        stageNormal2PViewImageJP.enabled = false;
                        stageHard2PViewImageJP.enabled = false;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        stageEasyViewImage.sprite = stageEasy2PViewImageJP.sprite;
                        stageNormalViewImage.sprite = stageNormal2PViewImageJP.sprite;
                        stageHardViewImage.sprite = stageHard2PViewImageJP.sprite;
                        stageEasy2PViewImageEN.enabled = false;
                        stageNormal2PViewImageEN.enabled = false;
                        stageHard2PViewImageEN.enabled = false;
                        break;
                }
                break;
            case 3:
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        stageEasyViewImage.sprite = stageEasy3PViewImageEN.sprite;
                        stageNormalViewImage.sprite = stageNormal3PViewImageEN.sprite;
                        stageHardViewImage.sprite = stageHard3PViewImageEN.sprite;
                        stageEasy3PViewImageJP.enabled = false;
                        stageNormal3PViewImageJP.enabled = false;
                        stageHard3PViewImageJP.enabled = false;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        stageEasyViewImage.sprite = stageEasy3PViewImageJP.sprite;
                        stageNormalViewImage.sprite = stageNormal3PViewImageJP.sprite;
                        stageHardViewImage.sprite = stageHard3PViewImageJP.sprite;
                        stageEasy3PViewImageEN.enabled = false;
                        stageNormal3PViewImageEN.enabled = false;
                        stageHard3PViewImageEN.enabled = false;
                        break;
                }
                break;
            case 4:
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        stageEasyViewImage.sprite = stageEasy4PViewImageEN.sprite;
                        stageNormalViewImage.sprite = stageNormal4PViewImageEN.sprite;
                        stageHardViewImage.sprite = stageHard4PViewImageEN.sprite;
                        stageEasy4PViewImageJP.enabled = false;
                        stageNormal4PViewImageJP.enabled = false;
                        stageHard4PViewImageJP.enabled = false;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        stageEasyViewImage.sprite = stageEasy4PViewImageJP.sprite;
                        stageNormalViewImage.sprite = stageNormal4PViewImageJP.sprite;
                        stageHardViewImage.sprite = stageHard4PViewImageJP.sprite;
                        stageEasy4PViewImageEN.enabled = false;
                        stageNormal4PViewImageEN.enabled = false;
                        stageHard4PViewImageEN.enabled = false;
                        break;
                }
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
                hint5ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint5ButtonText");
                startButtonText.text = multiplayerInstruction_EN.GetLabelContent("StartButtonText");
                titleButtonText.text = multiplayerInstruction_EN.GetLabelContent("TitleButtonText");
                pleaseWaitLabelText.text = multiplayerInstruction_EN.GetLabelContent("PleaseWaitLabelText");
                winConditionText.text = multiplayerInstruction_EN.GetLabelContent("WinConditionText").Replace("|", System.Environment.NewLine);
                hint1Text.text = multiplayerInstruction_EN.GetLabelContent("Hint1Text").Replace("|", System.Environment.NewLine);
                hint2Text.text = multiplayerInstruction_EN.GetLabelContent("Hint2Text").Replace("|", System.Environment.NewLine);
                hint3Text.text = multiplayerInstruction_EN.GetLabelContent("Hint3Text").Replace("|", System.Environment.NewLine);
                hint4Text.text = multiplayerInstruction_EN.GetLabelContent("Hint4Text").Replace("|", System.Environment.NewLine);
                hint5Text.text = multiplayerInstruction_EN.GetLabelContent("Hint5Text").Replace("|", System.Environment.NewLine);
                hint1ImageEN.SetActive(true);
                hint2Image1EN.SetActive(true);
                hint2Image2EN.SetActive(true);
                hint3ImageEN.SetActive(true);
                hint4ImageEN.SetActive(true);
                hint5ImageEN.SetActive(true);
                hint1ImageJP.SetActive(false);
                hint2Image1JP.SetActive(false);
                hint2Image2JP.SetActive(false);
                hint3ImageJP.SetActive(false);
                hint4ImageJP.SetActive(false);
                hint5ImageJP.SetActive(false);
                attackItemText.text = multiplayerInstruction_EN.GetLabelContent("AttackItemText");
                defenceItemText.text = multiplayerInstruction_EN.GetLabelContent("DefenceItemText");
                abnormalItemText.text = multiplayerInstruction_EN.GetLabelContent("AbnormalItemText");
                boostCanItemText.text = multiplayerInstruction_EN.GetLabelContent("BoostCanItemText");
                reduceSpeedItemText.text = multiplayerInstruction_EN.GetLabelContent("ReduceSpeedItemText");
                reduceBoostItemText.text = multiplayerInstruction_EN.GetLabelContent("ReduceBoostItemText");
                joystickImageEN.SetActive(true);
                joystickImageJP.SetActive(false);
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
                hint5ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint5ButtonText");
                hint5ButtonText.fontStyle = FontStyle.Bold;
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
                hint5Text.text = multiplayerInstruction_JP.GetLabelContent("Hint5Text").Replace("|", System.Environment.NewLine);
                hint1ImageEN.SetActive(false);
                hint2Image1EN.SetActive(false);
                hint2Image2EN.SetActive(false);
                hint3ImageEN.SetActive(false);
                hint4ImageEN.SetActive(false);
                hint5ImageEN.SetActive(false);
                hint1ImageJP.SetActive(true);
                hint2Image1JP.SetActive(true);
                hint2Image2JP.SetActive(true);
                hint3ImageJP.SetActive(true);
                hint4ImageJP.SetActive(true);
                hint5ImageJP.SetActive(true);
                attackItemText.text = multiplayerInstruction_JP.GetLabelContent("AttackItemText");
                defenceItemText.text = multiplayerInstruction_JP.GetLabelContent("DefenceItemText");
                abnormalItemText.text = multiplayerInstruction_JP.GetLabelContent("AbnormalItemText");
                boostCanItemText.text = multiplayerInstruction_JP.GetLabelContent("BoostCanItemText");
                reduceSpeedItemText.text = multiplayerInstruction_JP.GetLabelContent("ReduceSpeedItemText");
                reduceBoostItemText.text = multiplayerInstruction_JP.GetLabelContent("ReduceBoostItemText");
                joystickImageEN.SetActive(false);
                joystickImageJP.SetActive(true);
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
        hints5Section.SetActive(false);
        hintsSection.SetActive(true);
    }

    public void ShowHints1Section()
    {
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
    }

    public void ShowHints2Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(true);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
    }

    public void ShowHints3Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(true);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
    }

    public void ShowHints4Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(true);
        hints5Section.SetActive(false);
    }
    public void ShowHints5Section()
    {
        hints4Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints1Section.SetActive(false);
        hints5Section.SetActive(true);
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
