using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterPlayerNameMultiplay : MonoBehaviour
{
    [SerializeField] Localization multiplay_enter_name_EN, multiplay_enter_name_JP;
    [SerializeField] string difficultyScene;
    [SerializeField] string teamSelectionScene;
    [SerializeField] string titleMap;
    [SerializeField] GameObject enterPlayerNameCanvas, pleaseWaitCanvas;
    [SerializeField] GameObject[] playerNameObjects;
    [SerializeField] InputField[] playerNameInput;
    [SerializeField] Text[] playerNamePlaceHolderText;
    [SerializeField] Text errorText, enterPlayerNameTitleText, proceedButtonText, titleButtonText, pleaseWaitLabelText;
    [SerializeField] Text player1LabelText, player2LabelText, player3LabelText, player4LabelText;

    private int numOfPlayers;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        numOfPlayers = PlayerNameTempSaveMultiplay.numPlayers;
        for(int playerIndex = playerNameObjects.Length - 1; playerIndex >= numOfPlayers ; playerIndex--)
        {
            playerNameObjects[playerIndex].SetActive(false);
        }
        errorText.enabled = false;
        enterPlayerNameCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);

        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                enterPlayerNameTitleText.text = multiplay_enter_name_EN.GetLabelContent("EnterPlayerNameTitleText");
                proceedButtonText.text = multiplay_enter_name_EN.GetLabelContent("ProceedButtonText");
                titleButtonText.text = multiplay_enter_name_EN.GetLabelContent("TitleButtonText");
                pleaseWaitLabelText.text = multiplay_enter_name_EN.GetLabelContent("PleaseWaitLabelText");
                player1LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player1LabelText");
                player2LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player2LabelText");
                player3LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player3LabelText");
                player4LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player4LabelText");
                break;
            case Language.DisplayLanauge.Japanese:
                errorText.fontStyle = FontStyle.Bold;
                enterPlayerNameTitleText.text = multiplay_enter_name_JP.GetLabelContent("EnterPlayerNameTitleText");
                enterPlayerNameTitleText.fontStyle = FontStyle.Bold;
                proceedButtonText.text = multiplay_enter_name_JP.GetLabelContent("ProceedButtonText");
                proceedButtonText.fontStyle = FontStyle.Bold;
                titleButtonText.text = multiplay_enter_name_JP.GetLabelContent("TitleButtonText");
                titleButtonText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = multiplay_enter_name_JP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                player1LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player1LabelText");
                player1LabelText.fontStyle = FontStyle.Bold;
                player2LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player2LabelText");
                player2LabelText.fontStyle = FontStyle.Bold;
                player3LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player3LabelText");
                player3LabelText.fontStyle = FontStyle.Bold;
                player4LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player4LabelText");
                player4LabelText.fontStyle = FontStyle.Bold;
                break;
        }
        for(int i = 0; i < playerNamePlaceHolderText.Length; i++)
        {
            switch (Language.gameDisplayLanguage)
            {
                case Language.DisplayLanauge.English:
                    playerNamePlaceHolderText[i].text = multiplay_enter_name_EN.GetLabelContent("PlayerNamePlaceholderText");
                    break;
                case Language.DisplayLanauge.Japanese:
                    playerNamePlaceHolderText[i].text = multiplay_enter_name_JP.GetLabelContent("PlayerNamePlaceholderText");
                    break;
            }
        }
    }

    public void Proceed()
    {
        bool proceedFlag = true;
        for(int playerIndex = 0; playerIndex < numOfPlayers; playerIndex++)
        {
            if (playerNameInput[playerIndex].text == "" || playerNameInput[playerIndex].text == null)
            {
                if (errorText == null) { return; }
                errorText.enabled = true;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        errorText.text = "Player " + (playerIndex + 1) + multiplay_enter_name_EN.GetLabelContent("ErrorText");
                        break;
                    case Language.DisplayLanauge.Japanese:
                        errorText.text = "ƒvƒŒƒCƒ„[ " + (playerIndex + 1) + multiplay_enter_name_JP.GetLabelContent("ErrorText");
                        break;
                }
                proceedFlag = false;
                break;
            }
        }
        
        if(proceedFlag)
        {
            if (errorText == null) { return; }
            errorText.enabled = false;
            errorText.text = "";
            for (int playerIndex = 0; playerIndex < numOfPlayers; playerIndex++)
            {
                PlayerNameTempSaveMultiplay.playerName[playerIndex] = playerNameInput[playerIndex].text.ToString();
            }
            if (difficultyScene.Equals("") || teamSelectionScene.Equals("")) { return; }
            pleaseWaitCanvas.SetActive(true);
            enterPlayerNameCanvas.SetActive(false);
            if (MultiplayPlayerMode.gameMode.Equals("BattleRoyale"))
            {
                SceneManager.LoadScene(difficultyScene);
            }
            else
            {
                SceneManager.LoadScene(teamSelectionScene);
            }
        }
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
