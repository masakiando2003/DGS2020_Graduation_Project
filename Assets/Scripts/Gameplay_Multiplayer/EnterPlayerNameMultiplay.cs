using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterPlayerNameMultiplay : MonoBehaviour
{
    [SerializeField] Localization multiplay_enter_name_EN, multiplay_enter_name_JP;
    [SerializeField] Font multiplayEnterNameENFont, multiplayEnterNameJPFont;
    [SerializeField] string difficultyScene;
    [SerializeField] string teamSelectionScene;
    [SerializeField] string titleMap;
    [SerializeField] Slider loadingSlider;
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
                enterPlayerNameTitleText.font = multiplayEnterNameENFont;
                proceedButtonText.text = multiplay_enter_name_EN.GetLabelContent("ProceedButtonText");
                proceedButtonText.font = multiplayEnterNameENFont;
                titleButtonText.text = multiplay_enter_name_EN.GetLabelContent("TitleButtonText");
                titleButtonText.font = multiplayEnterNameENFont;
                pleaseWaitLabelText.text = multiplay_enter_name_EN.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplayEnterNameENFont;
                player1LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player1LabelText");
                player1LabelText.font = multiplayEnterNameENFont;
                player2LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player2LabelText");
                player2LabelText.font = multiplayEnterNameENFont;
                player3LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player3LabelText");
                player3LabelText.font = multiplayEnterNameENFont;
                player4LabelText.text = multiplay_enter_name_EN.GetLabelContent("Player4LabelText");
                player4LabelText.font = multiplayEnterNameENFont;
                for(int i = 0; i < playerNameInput.Length; i++)
                {
                    playerNamePlaceHolderText[i].font = multiplayEnterNameENFont;
                    playerNameInput[i].textComponent.font = multiplayEnterNameENFont;
                }
                break;
            case Language.DisplayLanauge.Japanese:
                errorText.fontStyle = FontStyle.Bold;
                enterPlayerNameTitleText.text = multiplay_enter_name_JP.GetLabelContent("EnterPlayerNameTitleText");
                enterPlayerNameTitleText.font = multiplayEnterNameJPFont;
                enterPlayerNameTitleText.fontStyle = FontStyle.Bold;
                proceedButtonText.text = multiplay_enter_name_JP.GetLabelContent("ProceedButtonText");
                proceedButtonText.font = multiplayEnterNameJPFont;
                proceedButtonText.fontStyle = FontStyle.Bold;
                titleButtonText.text = multiplay_enter_name_JP.GetLabelContent("TitleButtonText");
                titleButtonText.font = multiplayEnterNameJPFont;
                titleButtonText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = multiplay_enter_name_JP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplayEnterNameJPFont;
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                player1LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player1LabelText");
                player1LabelText.font = multiplayEnterNameJPFont;
                player1LabelText.fontStyle = FontStyle.Bold;
                player2LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player2LabelText");
                player2LabelText.font = multiplayEnterNameJPFont;
                player2LabelText.fontStyle = FontStyle.Bold;
                player3LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player3LabelText");
                player3LabelText.font = multiplayEnterNameJPFont;
                player3LabelText.fontStyle = FontStyle.Bold;
                player4LabelText.text = multiplay_enter_name_JP.GetLabelContent("Player4LabelText");
                player4LabelText.font = multiplayEnterNameJPFont;
                player4LabelText.fontStyle = FontStyle.Bold;
                for (int i = 0; i < playerNameInput.Length; i++)
                {
                    playerNamePlaceHolderText[i].font = multiplayEnterNameJPFont;
                    playerNamePlaceHolderText[i].fontStyle = FontStyle.Bold;
                    playerNameInput[i].textComponent.font = multiplayEnterNameJPFont;
                    playerNameInput[i].textComponent.fontStyle = FontStyle.Bold;
                }
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
        playerNameInput[0].ActivateInputField();
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
                StartCoroutine(LoadLevelAsynchronously(difficultyScene));
            }
            else
            {
                StartCoroutine(LoadLevelAsynchronously(teamSelectionScene));
            }
        }
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
