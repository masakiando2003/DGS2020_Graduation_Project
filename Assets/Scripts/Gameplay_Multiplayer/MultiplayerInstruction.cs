using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerInstruction : MonoBehaviour
{
    [SerializeField] Localization multiplayerInstruction_EN, multiplayerInstruction_JP;
    [SerializeField] Font multiplayerInstructionENFont, multiplayerInstructionJPFont;
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] GameObject pleaseWaitCanvas;
    [SerializeField] GameObject objectionSection, controlsSection, hintsSection, itemsSection;
    [SerializeField] GameObject hints1Section, hints2Section, hints3Section, hints4Section, hints5Section, hints6Section;
    [SerializeField] GameObject joystickImageEN, joystickImageJP;
    [SerializeField] GameObject joystickControlEN, joystickControlJP;
    [SerializeField] GameObject hint1ImageEN, hint2Image1EN, hint2Image2EN, hint3ImageEN, hint4ImageEN, hint5Image1EN, hint5Image2EN, hint6ImageEN;
    [SerializeField] GameObject hint1ImageJP, hint2Image1JP, hint2Image2JP, hint3ImageJP, hint4ImageJP, hint5Image1JP, hint5Image2JP, hint6ImageJP;
    [SerializeField] GameObject stageEasyViewImageObj, stageNormalViewImageObj, stageHardViewImageObj;
    [SerializeField] GameObject stageEasy2PViewImageEN, stageNormal2PViewImageEN, stageHard2PViewImageEN;
    [SerializeField] GameObject stageEasy3PViewImageEN, stageNormal3PViewImageEN, stageHard3PViewImageEN;
    [SerializeField] GameObject stageEasy4PViewImageEN, stageNormal4PViewImageEN, stageHard4PViewImageEN;
    [SerializeField] GameObject stageEasy2PViewImageJP, stageNormal2PViewImageJP, stageHard2PViewImageJP;
    [SerializeField] GameObject stageEasy3PViewImageJP, stageNormal3PViewImageJP, stageHard3PViewImageJP;
    [SerializeField] GameObject stageEasy4PViewImageJP, stageNormal4PViewImageJP, stageHard4PViewImageJP;
    [SerializeField] string titleMap;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text winConditionButtonText, controlButtonText, hintsButtonText, itemsButtonText;
    [SerializeField] Text hint1ButtonText, hint2ButtonText, hint3ButtonText, hint4ButtonText, hint5ButtonText, hint6ButtonText;
    [SerializeField] Text instructionTitleText, startButtonText, titleButtonText, pleaseWaitLabelText;
    [SerializeField] Text winConditionText, hint1Text, hint2Text, hint3Text, hint4Text, hint5Text, hint6Text;
    [SerializeField] Text attackItemText, defenceItemText, abnormalItemText;
    [SerializeField] Text boostCanItemText, reduceSpeedItemText, reduceBoostItemText;
    [SerializeField] Button btnMission, btnControl, btnHints;
    [SerializeField] Button btnHints1, btnHints2, btnHints3, btnHints4, btnHints5, btnHints6;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;


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
        hints6Section.SetActive(false);
        hintsSection.SetActive(false);
        itemsSection.SetActive(false);
        stageEasy2PViewImageEN.SetActive(false);
        stageNormal2PViewImageEN.SetActive(false);
        stageHard2PViewImageEN.SetActive(false);
        stageEasy3PViewImageEN.SetActive(false);
        stageNormal3PViewImageEN.SetActive(false);
        stageHard3PViewImageEN.SetActive(false);
        stageEasy4PViewImageEN.SetActive(false);
        stageNormal4PViewImageEN.SetActive(false);
        stageHard4PViewImageEN.SetActive(false);
        stageEasy2PViewImageJP.SetActive(false);
        stageNormal2PViewImageJP.SetActive(false);
        stageHard2PViewImageJP.SetActive(false);
        stageEasy3PViewImageJP.SetActive(false);
        stageNormal3PViewImageJP.SetActive(false);
        stageHard3PViewImageJP.SetActive(false);
        stageEasy4PViewImageJP.SetActive(false);
        stageNormal4PViewImageJP.SetActive(false);
        stageHard4PViewImageJP.SetActive(false);
        btnMission.GetComponent<Image>().color = buttonSelectedColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (PlayerNameTempSaveMultiplay.numPlayers)
        {
            case 2:
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        switch (MultiplayPlayerMode.chosenDifficulty)
                        {
                            case "Easy":
                                stageEasy2PViewImageEN.SetActive(true);
                                break;
                            case "Normal":
                                stageNormal2PViewImageEN.SetActive(true);
                                break;
                            case "Hard":
                                stageHard2PViewImageEN.SetActive(true);
                                break;
                        }
                        break;
                    case Language.DisplayLanauge.Japanese:
                        switch (MultiplayPlayerMode.chosenDifficulty)
                        {
                            case "Easy":
                                stageEasy2PViewImageJP.SetActive(true);
                                break;
                            case "Normal":
                                stageNormal2PViewImageJP.SetActive(true);
                                break;
                            case "Hard":
                                stageHard2PViewImageJP.SetActive(true);
                                break;
                        }
                        break;
                }
                break;
            case 3:
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        switch (MultiplayPlayerMode.chosenDifficulty)
                        {
                            case "Easy":
                                stageEasy3PViewImageEN.SetActive(true);
                                break;
                            case "Normal":
                                stageNormal3PViewImageEN.SetActive(true);
                                break;
                            case "Hard":
                                stageHard3PViewImageEN.SetActive(true);
                                break;
                        }
                        break;
                    case Language.DisplayLanauge.Japanese:
                        switch (MultiplayPlayerMode.chosenDifficulty)
                        {
                            case "Easy":
                                stageEasy3PViewImageJP.SetActive(true);
                                break;
                            case "Normal":
                                stageNormal3PViewImageJP.SetActive(true);
                                break;
                            case "Hard":
                                stageHard3PViewImageJP.SetActive(true);
                                break;
                        }
                        break;
                }
                break;
            case 4:
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        switch (MultiplayPlayerMode.chosenDifficulty)
                        {
                            case "Easy":
                                stageEasy4PViewImageEN.SetActive(true);
                                break;
                            case "Normal":
                                stageNormal4PViewImageEN.SetActive(true);
                                break;
                            case "Hard":
                                stageHard4PViewImageEN.SetActive(true);
                                break;
                        }
                        break;
                    case Language.DisplayLanauge.Japanese:
                        switch (MultiplayPlayerMode.chosenDifficulty)
                        {
                            case "Easy":
                                stageEasy4PViewImageJP.SetActive(true);
                                break;
                            case "Normal":
                                stageNormal4PViewImageJP.SetActive(true);
                                break;
                            case "Hard":
                                stageHard4PViewImageJP.SetActive(true);
                                break;
                        }
                        break;
                }
                break;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                instructionTitleText.text = multiplayerInstruction_EN.GetLabelContent("InstructionTitleText");
                instructionTitleText.font = multiplayerInstructionENFont;
                winConditionButtonText.text = multiplayerInstruction_EN.GetLabelContent("WinConditionButtonText");
                winConditionButtonText.font = multiplayerInstructionENFont;
                controlButtonText.text = multiplayerInstruction_EN.GetLabelContent("ControlButtonText");
                controlButtonText.font = multiplayerInstructionENFont;
                hintsButtonText.text = multiplayerInstruction_EN.GetLabelContent("HintsButtonText");
                hintsButtonText.font = multiplayerInstructionENFont;
                itemsButtonText.text = multiplayerInstruction_EN.GetLabelContent("ItemsButtonText");
                itemsButtonText.font = multiplayerInstructionENFont;
                hint1ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint1ButtonText");
                hint1ButtonText.font = multiplayerInstructionENFont;
                hint2ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint2ButtonText");
                hint2ButtonText.font = multiplayerInstructionENFont;
                hint3ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint3ButtonText");
                hint3ButtonText.font = multiplayerInstructionENFont;
                hint4ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint4ButtonText");
                hint4ButtonText.font = multiplayerInstructionENFont;
                hint5ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint5ButtonText");
                hint5ButtonText.font = multiplayerInstructionENFont;
                hint6ButtonText.text = multiplayerInstruction_EN.GetLabelContent("Hint6ButtonText");
                hint6ButtonText.font = multiplayerInstructionENFont;
                startButtonText.text = multiplayerInstruction_EN.GetLabelContent("StartButtonText");
                startButtonText.font = multiplayerInstructionENFont;
                titleButtonText.text = multiplayerInstruction_EN.GetLabelContent("TitleButtonText");
                titleButtonText.font = multiplayerInstructionENFont;
                pleaseWaitLabelText.text = multiplayerInstruction_EN.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplayerInstructionENFont;
                winConditionText.text = multiplayerInstruction_EN.GetLabelContent("WinConditionText").Replace("|", System.Environment.NewLine);
                winConditionText.font = multiplayerInstructionENFont;
                hint1Text.text = multiplayerInstruction_EN.GetLabelContent("Hint1Text").Replace("|", System.Environment.NewLine);
                hint1Text.font = multiplayerInstructionENFont;
                hint2Text.text = multiplayerInstruction_EN.GetLabelContent("Hint2Text").Replace("|", System.Environment.NewLine);
                hint2Text.font = multiplayerInstructionENFont;
                hint3Text.text = multiplayerInstruction_EN.GetLabelContent("Hint3Text").Replace("|", System.Environment.NewLine);
                hint3Text.font = multiplayerInstructionENFont;
                hint4Text.text = multiplayerInstruction_EN.GetLabelContent("Hint4Text").Replace("|", System.Environment.NewLine);
                hint4Text.font = multiplayerInstructionENFont;
                hint5Text.text = multiplayerInstruction_EN.GetLabelContent("Hint5Text").Replace("|", System.Environment.NewLine);
                hint5Text.font = multiplayerInstructionENFont;
                hint6Text.text = multiplayerInstruction_EN.GetLabelContent("Hint6Text").Replace("|", System.Environment.NewLine);
                hint6Text.font = multiplayerInstructionENFont;
                hint1ImageEN.SetActive(true);
                hint2Image1EN.SetActive(true);
                hint2Image2EN.SetActive(true);
                hint3ImageEN.SetActive(true);
                hint4ImageEN.SetActive(true);
                hint5Image1EN.SetActive(true);
                hint5Image2EN.SetActive(true);
                hint6ImageEN.SetActive(true);
                hint1ImageJP.SetActive(false);
                hint2Image1JP.SetActive(false);
                hint2Image2JP.SetActive(false);
                hint3ImageJP.SetActive(false);
                hint4ImageJP.SetActive(false);
                hint5Image1JP.SetActive(false);
                hint5Image2JP.SetActive(false);
                hint6ImageJP.SetActive(false);
                attackItemText.text = multiplayerInstruction_EN.GetLabelContent("AttackItemText");
                attackItemText.font = multiplayerInstructionENFont;
                defenceItemText.text = multiplayerInstruction_EN.GetLabelContent("DefenceItemText");
                defenceItemText.font = multiplayerInstructionENFont;
                abnormalItemText.text = multiplayerInstruction_EN.GetLabelContent("AbnormalItemText");
                abnormalItemText.font = multiplayerInstructionENFont;
                boostCanItemText.text = multiplayerInstruction_EN.GetLabelContent("BoostCanItemText");
                boostCanItemText.font = multiplayerInstructionENFont;
                reduceSpeedItemText.text = multiplayerInstruction_EN.GetLabelContent("ReduceSpeedItemText");
                reduceSpeedItemText.font = multiplayerInstructionENFont;
                reduceBoostItemText.text = multiplayerInstruction_EN.GetLabelContent("ReduceBoostItemText");
                reduceBoostItemText.font = multiplayerInstructionENFont;
                joystickControlEN.SetActive(true);
                joystickControlJP.SetActive(false);
                break;
            case Language.DisplayLanauge.Japanese:
                instructionTitleText.text = multiplayerInstruction_JP.GetLabelContent("InstructionTitleText");
                instructionTitleText.font = multiplayerInstructionJPFont;
                instructionTitleText.fontStyle = FontStyle.Bold;
                winConditionButtonText.text = multiplayerInstruction_JP.GetLabelContent("WinConditionButtonText");
                winConditionButtonText.font = multiplayerInstructionJPFont;
                winConditionButtonText.fontStyle = FontStyle.Bold;
                controlButtonText.text = multiplayerInstruction_JP.GetLabelContent("ControlButtonText");
                controlButtonText.font = multiplayerInstructionJPFont;
                controlButtonText.fontStyle = FontStyle.Bold;
                hintsButtonText.text = multiplayerInstruction_JP.GetLabelContent("HintsButtonText");
                hintsButtonText.font = multiplayerInstructionJPFont;
                hintsButtonText.fontStyle = FontStyle.Bold;
                itemsButtonText.text = multiplayerInstruction_JP.GetLabelContent("ItemsButtonText");
                itemsButtonText.font = multiplayerInstructionJPFont;
                itemsButtonText.fontStyle = FontStyle.Bold;
                hint1ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint1ButtonText");
                hint1ButtonText.font = multiplayerInstructionJPFont;
                hint1ButtonText.fontStyle = FontStyle.Bold;
                hint2ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint2ButtonText");
                hint2ButtonText.font = multiplayerInstructionJPFont;
                hint2ButtonText.fontStyle = FontStyle.Bold;
                hint3ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint3ButtonText");
                hint3ButtonText.font = multiplayerInstructionJPFont;
                hint3ButtonText.fontStyle = FontStyle.Bold;
                hint4ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint4ButtonText");
                hint4ButtonText.font = multiplayerInstructionJPFont;
                hint4ButtonText.fontStyle = FontStyle.Bold;
                hint5ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint5ButtonText");
                hint5ButtonText.font = multiplayerInstructionJPFont;
                hint5ButtonText.fontStyle = FontStyle.Bold;
                hint6ButtonText.text = multiplayerInstruction_JP.GetLabelContent("Hint6ButtonText");
                hint6ButtonText.font = multiplayerInstructionJPFont;
                hint6ButtonText.fontStyle = FontStyle.Bold;
                startButtonText.text = multiplayerInstruction_JP.GetLabelContent("StartButtonText");
                startButtonText.font = multiplayerInstructionJPFont;
                startButtonText.fontStyle = FontStyle.Bold;
                titleButtonText.text = multiplayerInstruction_JP.GetLabelContent("TitleButtonText");
                titleButtonText.font = multiplayerInstructionJPFont;
                titleButtonText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = multiplayerInstruction_JP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplayerInstructionJPFont;
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                winConditionText.text = multiplayerInstruction_JP.GetLabelContent("WinConditionText").Replace("|", System.Environment.NewLine);
                winConditionText.font = multiplayerInstructionJPFont;
                winConditionText.fontStyle = FontStyle.Bold;
                hint1Text.text = multiplayerInstruction_JP.GetLabelContent("Hint1Text").Replace("|", System.Environment.NewLine);
                hint1Text.font = multiplayerInstructionJPFont;
                hint1Text.fontStyle = FontStyle.Bold;
                hint2Text.text = multiplayerInstruction_JP.GetLabelContent("Hint2Text").Replace("|", System.Environment.NewLine);
                hint2Text.font = multiplayerInstructionJPFont;
                hint2Text.fontStyle = FontStyle.Bold;
                hint3Text.text = multiplayerInstruction_JP.GetLabelContent("Hint3Text").Replace("|", System.Environment.NewLine);
                hint3Text.font = multiplayerInstructionJPFont;
                hint3Text.fontStyle = FontStyle.Bold;
                hint4Text.text = multiplayerInstruction_JP.GetLabelContent("Hint4Text").Replace("|", System.Environment.NewLine);
                hint4Text.font = multiplayerInstructionJPFont;
                hint4Text.fontStyle = FontStyle.Bold;
                hint5Text.text = multiplayerInstruction_JP.GetLabelContent("Hint5Text").Replace("|", System.Environment.NewLine);
                hint5Text.font = multiplayerInstructionJPFont;
                hint5Text.fontStyle = FontStyle.Bold;
                hint6Text.text = multiplayerInstruction_JP.GetLabelContent("Hint6Text").Replace("|", System.Environment.NewLine);
                hint6Text.font = multiplayerInstructionJPFont;
                hint6Text.fontStyle = FontStyle.Bold;
                hint1ImageEN.SetActive(false);
                hint2Image1EN.SetActive(false);
                hint2Image2EN.SetActive(false);
                hint3ImageEN.SetActive(false);
                hint4ImageEN.SetActive(false);
                hint5Image1EN.SetActive(false);
                hint5Image2EN.SetActive(false);
                hint6ImageEN.SetActive(false);
                hint1ImageJP.SetActive(true);
                hint2Image1JP.SetActive(true);
                hint2Image2JP.SetActive(true);
                hint3ImageJP.SetActive(true);
                hint4ImageJP.SetActive(true);
                hint5Image1JP.SetActive(true);
                hint5Image2JP.SetActive(true);
                hint6ImageJP.SetActive(true);
                attackItemText.text = multiplayerInstruction_JP.GetLabelContent("AttackItemText");
                attackItemText.font = multiplayerInstructionJPFont;
                attackItemText.fontStyle = FontStyle.Bold;
                defenceItemText.text = multiplayerInstruction_JP.GetLabelContent("DefenceItemText");
                defenceItemText.font = multiplayerInstructionJPFont;
                defenceItemText.fontStyle = FontStyle.Bold;
                abnormalItemText.text = multiplayerInstruction_JP.GetLabelContent("AbnormalItemText");
                abnormalItemText.font = multiplayerInstructionJPFont;
                abnormalItemText.fontStyle = FontStyle.Bold;
                boostCanItemText.text = multiplayerInstruction_JP.GetLabelContent("BoostCanItemText");
                boostCanItemText.font = multiplayerInstructionJPFont;
                boostCanItemText.fontStyle = FontStyle.Bold;
                reduceSpeedItemText.text = multiplayerInstruction_JP.GetLabelContent("ReduceSpeedItemText");
                reduceSpeedItemText.font = multiplayerInstructionJPFont;
                reduceSpeedItemText.fontStyle = FontStyle.Bold;
                reduceBoostItemText.text = multiplayerInstruction_JP.GetLabelContent("ReduceBoostItemText");
                reduceBoostItemText.font = multiplayerInstructionJPFont;
                reduceBoostItemText.fontStyle = FontStyle.Bold;
                joystickControlEN.SetActive(false);
                joystickControlJP.SetActive(true);
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

        btnMission.GetComponent<Image>().color = buttonSelectedColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowObjectiveSection()
    {
        controlsSection.SetActive(false);
        hintsSection.SetActive(false);
        itemsSection.SetActive(false);
        objectionSection.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonSelectedColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowControlsSection()
    {
        objectionSection.SetActive(false);
        hintsSection.SetActive(false);
        itemsSection.SetActive(false);
        controlsSection.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonSelectedColor;
        btnHints.GetComponent<Image>().color = buttonNormalColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
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
        hints6Section.SetActive(false);
        hintsSection.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonSelectedColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints1Section()
    {
        hints1Section.SetActive(true);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
        hints6Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonSelectedColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints2Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(true);
        hints3Section.SetActive(false);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
        hints6Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonSelectedColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints3Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(true);
        hints4Section.SetActive(false);
        hints5Section.SetActive(false);
        hints6Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonSelectedColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints4Section()
    {
        hints1Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints4Section.SetActive(true);
        hints5Section.SetActive(false);
        hints6Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonSelectedColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints5Section()
    {
        hints4Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints1Section.SetActive(false);
        hints5Section.SetActive(true);
        hints6Section.SetActive(false);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonSelectedColor;
        btnHints6.GetComponent<Image>().color = buttonNormalColor;
    }

    public void ShowHints6Section()
    {
        hints4Section.SetActive(false);
        hints2Section.SetActive(false);
        hints3Section.SetActive(false);
        hints1Section.SetActive(false);
        hints5Section.SetActive(false);
        hints6Section.SetActive(true);

        btnMission.GetComponent<Image>().color = buttonNormalColor;
        btnControl.GetComponent<Image>().color = buttonNormalColor;
        btnHints.GetComponent<Image>().color = buttonSelectedColor;
        btnHints1.GetComponent<Image>().color = buttonNormalColor;
        btnHints2.GetComponent<Image>().color = buttonNormalColor;
        btnHints3.GetComponent<Image>().color = buttonNormalColor;
        btnHints4.GetComponent<Image>().color = buttonNormalColor;
        btnHints5.GetComponent<Image>().color = buttonNormalColor;
        btnHints6.GetComponent<Image>().color = buttonSelectedColor;
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
        StartCoroutine(LoadLevelAsynchronously(mapName));
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
}
