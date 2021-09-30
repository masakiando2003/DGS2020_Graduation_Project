using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMulitplayerNumbers : MonoBehaviour
{
    [SerializeField] Localization multiplaySelectNumPlayers_EN, multiplaySelectNumPlayers_JP;
    [SerializeField] GameObject numberOfPlayersCanvas, battleModeCanvas, pleaseWaitCanvas;
    [SerializeField] GameObject threePlayersButton, fourPlayersButton, proceedButton;
    [SerializeField] Text numOfPlayersLabelText, numOfPlayersText;
    [SerializeField] Text joysticksConnectedLabelText, joysticksConnectedText;
    [SerializeField] Text notEnoughJoysticksText;
    [SerializeField] Text gameModeLabelText, gameModeText, selectGameModeText;
    [SerializeField] Text players_2_Button_Text, players_3_Button_Text, players_4_Button_Text;
    [SerializeField] Text proceedButtonText, titleButtonText, pleaseWaitLabelText;
    [SerializeField] Text battleRoyaleButtonText, teamPlayButtonText;
    [SerializeField] bool debugFlag;

    int numOfPlayers;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        numOfPlayers = 2;
        notEnoughJoysticksText.enabled = false;
        numberOfPlayersCanvas.SetActive(true);
        battleModeCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(false);

        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                players_2_Button_Text.text = multiplaySelectNumPlayers_EN.GetLabelContent("2PlayersText");
                players_3_Button_Text.text = multiplaySelectNumPlayers_EN.GetLabelContent("3PlayersText");
                players_4_Button_Text.text = multiplaySelectNumPlayers_EN.GetLabelContent("4PlayersText");
                joysticksConnectedLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("JoysticksConnectedLabelText");
                notEnoughJoysticksText.text = multiplaySelectNumPlayers_EN.GetLabelContent("NotEnoughJoysticksText");
                selectGameModeText.text = multiplaySelectNumPlayers_EN.GetLabelContent("SelectGameModeText");
                gameModeLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("GameModeLabelText");
                proceedButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("ProceedButtonText");
                titleButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("TitleButtonText");
                numOfPlayersLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("NumOfPlayersLabelText");
                battleRoyaleButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("BattleRoyaleButtonText");
                teamPlayButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("TeamPlayButtonText");
                pleaseWaitLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("PleaseWaitLabelText");
                break;
            case Language.DisplayLanauge.Japanese:
                players_2_Button_Text.text = multiplaySelectNumPlayers_JP.GetLabelContent("2PlayersText");
                players_3_Button_Text.text = multiplaySelectNumPlayers_JP.GetLabelContent("3PlayersText");
                players_4_Button_Text.text = multiplaySelectNumPlayers_JP.GetLabelContent("4PlayersText");
                joysticksConnectedLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("JoysticksConnectedLabelText");
                notEnoughJoysticksText.text = multiplaySelectNumPlayers_JP.GetLabelContent("NotEnoughJoysticksText");
                selectGameModeText.text = multiplaySelectNumPlayers_JP.GetLabelContent("SelectGameModeText");
                gameModeLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("GameModeLabelText");
                proceedButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("ProceedButtonText");
                titleButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("TitleButtonText");
                numOfPlayersLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("NumOfPlayersLabelText");
                battleRoyaleButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("BattleRoyaleButtonText");
                teamPlayButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("TeamPlayButtonText");
                pleaseWaitLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("PleaseWaitLabelText");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNumOfPlayers();
        DisplayGameMode();
        if (!debugFlag)
        {
            CheckNumOfJoysticksConnected();
        }
        else
        {
            DisableCheckNumOfJoysticksConnected();
        }
    }

    private void DisableCheckNumOfJoysticksConnected()
    {
        int numJoysticksConnected = 0;
        string[] names = Input.GetJoystickNames();
        foreach (string name in names)
        {
            if (!string.IsNullOrEmpty(name))
            {
                numJoysticksConnected++;
            }
        }
        joysticksConnectedText.text = numJoysticksConnected.ToString();
        notEnoughJoysticksText.enabled = false;
        proceedButton.SetActive(true);
    }

    private void CheckNumOfJoysticksConnected()
    {
        int numJoysticksConnected = 0;
        string[] names = Input.GetJoystickNames();
        foreach (string name in names)
        {
            if (!string.IsNullOrEmpty(name))
            {
                numJoysticksConnected++;
            }
        }
        if (numJoysticksConnected < 2)
        {
            notEnoughJoysticksText.enabled = true;
            proceedButton.SetActive(false);
        }
        joysticksConnectedText.text = numJoysticksConnected.ToString();
        if (numJoysticksConnected < 4)
        {
            fourPlayersButton.SetActive(false);
            if(numJoysticksConnected < 3)
            {
                threePlayersButton.SetActive(false);
            }
            else
            {
                threePlayersButton.SetActive(true);
            }
        }
        else
        {
            fourPlayersButton.SetActive(true);
        }
    }

    private void UpdateNumOfPlayers()
    {
        numOfPlayersText.text = numOfPlayers.ToString();
    }

    private void DisplayGameMode()
    {
        if(gameModeText == null) { return; }
        Debug.Log("Display Game Mode: " + MultiplayPlayerMode.gameMode);
        gameModeText.text = MultiplayPlayerMode.gameMode.ToString();
    }

    public void SetNumOfPlayers(int numPlayers)
    {
        numOfPlayers = numPlayers;
        if(numPlayers == 4)
        {
            numberOfPlayersCanvas.SetActive(false);
            pleaseWaitCanvas.SetActive(false);
            battleModeCanvas.SetActive(true);
        }
    }

    public void SetGameMode(string selectedGameMode)
    {
        MultiplayPlayerMode.gameMode = selectedGameMode;
        Debug.Log("Game Mode: " + MultiplayPlayerMode.gameMode);
        battleModeCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(false);
        numberOfPlayersCanvas.SetActive(true);
    }

    public void ProceedNextStage(string sceneName)
    {
        PlayerNameTempSaveMultiplay.numPlayers = numOfPlayers;
        if(sceneName == "") { return; }
        numberOfPlayersCanvas.SetActive(false);
        battleModeCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
