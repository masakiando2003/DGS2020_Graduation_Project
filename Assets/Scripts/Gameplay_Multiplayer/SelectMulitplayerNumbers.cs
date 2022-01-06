using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMulitplayerNumbers : MonoBehaviour
{
    [SerializeField] Localization multiplaySelectNumPlayers_EN, multiplaySelectNumPlayers_JP;
    [SerializeField] Font multiplaySelectNumPlayersENFont, multiplaySelectNumPlayersJPFont;
    [SerializeField] GameObject numberOfPlayersCanvas, battleModeCanvas, pleaseWaitCanvas;
    [SerializeField] GameObject threePlayersButton, fourPlayersButton, proceedButton;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text numOfPlayersLabelText, numOfPlayersText;
    [SerializeField] Text joysticksConnectedLabelText, joysticksConnectedText;
    [SerializeField] Text notEnoughJoysticksText;
    [SerializeField] Text gameModeLabelText, gameModeText, selectGameModeText;
    [SerializeField] Text players_2_Button_Text, players_3_Button_Text, players_4_Button_Text;
    [SerializeField] Text proceedButtonText, titleButtonText, pleaseWaitLabelText;
    [SerializeField] Text battleRoyaleButtonText, teamPlayButtonText;
    [SerializeField] bool debugFlag;
    [SerializeField] Button btn2P, btn3P, btn4P;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

    int numOfPlayers;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        numOfPlayers = 2;
        btn2P.GetComponent<Image>().color = buttonSelectedColor;
        btn3P.GetComponent<Image>().color = buttonNormalColor;
        btn4P.GetComponent<Image>().color = buttonNormalColor;
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
                players_2_Button_Text.font = multiplaySelectNumPlayersENFont;
                players_3_Button_Text.text = multiplaySelectNumPlayers_EN.GetLabelContent("3PlayersText");
                players_3_Button_Text.font = multiplaySelectNumPlayersENFont;
                players_4_Button_Text.text = multiplaySelectNumPlayers_EN.GetLabelContent("4PlayersText");
                players_4_Button_Text.font = multiplaySelectNumPlayersENFont;
                joysticksConnectedLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("JoysticksConnectedLabelText");
                joysticksConnectedLabelText.font = multiplaySelectNumPlayersENFont;
                notEnoughJoysticksText.text = multiplaySelectNumPlayers_EN.GetLabelContent("NotEnoughJoysticksText");
                notEnoughJoysticksText.font = multiplaySelectNumPlayersENFont;
                selectGameModeText.text = multiplaySelectNumPlayers_EN.GetLabelContent("SelectGameModeText");
                selectGameModeText.font = multiplaySelectNumPlayersENFont;
                gameModeLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("GameModeLabelText");
                gameModeLabelText.font = multiplaySelectNumPlayersENFont;
                proceedButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("ProceedButtonText");
                proceedButtonText.font = multiplaySelectNumPlayersENFont;
                titleButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("TitleButtonText");
                titleButtonText.font = multiplaySelectNumPlayersENFont;
                numOfPlayersLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("NumOfPlayersLabelText");
                numOfPlayersLabelText.font = multiplaySelectNumPlayersENFont;
                battleRoyaleButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("BattleRoyaleButtonText");
                battleRoyaleButtonText.font = multiplaySelectNumPlayersENFont;
                teamPlayButtonText.text = multiplaySelectNumPlayers_EN.GetLabelContent("TeamPlayButtonText");
                teamPlayButtonText.font = multiplaySelectNumPlayersENFont;
                pleaseWaitLabelText.text = multiplaySelectNumPlayers_EN.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplaySelectNumPlayersENFont;
                break;
            case Language.DisplayLanauge.Japanese:
                players_2_Button_Text.text = multiplaySelectNumPlayers_JP.GetLabelContent("2PlayersText");
                players_2_Button_Text.font = multiplaySelectNumPlayersJPFont;
                players_2_Button_Text.fontStyle = FontStyle.Bold;
                players_3_Button_Text.text = multiplaySelectNumPlayers_JP.GetLabelContent("3PlayersText");
                players_3_Button_Text.font = multiplaySelectNumPlayersJPFont;
                players_3_Button_Text.fontStyle = FontStyle.Bold;
                players_4_Button_Text.text = multiplaySelectNumPlayers_JP.GetLabelContent("4PlayersText");
                players_4_Button_Text.font = multiplaySelectNumPlayersJPFont;
                players_4_Button_Text.fontStyle = FontStyle.Bold;
                joysticksConnectedLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("JoysticksConnectedLabelText");
                joysticksConnectedLabelText.font = multiplaySelectNumPlayersJPFont;
                joysticksConnectedLabelText.fontStyle = FontStyle.Bold;
                notEnoughJoysticksText.text = multiplaySelectNumPlayers_JP.GetLabelContent("NotEnoughJoysticksText");
                notEnoughJoysticksText.font = multiplaySelectNumPlayersJPFont;
                notEnoughJoysticksText.fontStyle = FontStyle.Bold;
                selectGameModeText.text = multiplaySelectNumPlayers_JP.GetLabelContent("SelectGameModeText");
                selectGameModeText.font = multiplaySelectNumPlayersJPFont;
                selectGameModeText.fontStyle = FontStyle.Bold;
                gameModeLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("GameModeLabelText");
                gameModeLabelText.font = multiplaySelectNumPlayersJPFont;
                gameModeLabelText.fontStyle = FontStyle.Bold;
                proceedButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("ProceedButtonText");
                proceedButtonText.font = multiplaySelectNumPlayersJPFont;
                proceedButtonText.fontStyle = FontStyle.Bold;
                titleButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("TitleButtonText");
                titleButtonText.font = multiplaySelectNumPlayersJPFont;
                titleButtonText.fontStyle = FontStyle.Bold;
                numOfPlayersLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("NumOfPlayersLabelText");
                numOfPlayersLabelText.font = multiplaySelectNumPlayersJPFont;
                numOfPlayersLabelText.fontStyle = FontStyle.Bold;
                battleRoyaleButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("BattleRoyaleButtonText");
                battleRoyaleButtonText.font = multiplaySelectNumPlayersJPFont;
                battleRoyaleButtonText.fontStyle = FontStyle.Bold;
                teamPlayButtonText.text = multiplaySelectNumPlayers_JP.GetLabelContent("TeamPlayButtonText");
                teamPlayButtonText.font = multiplaySelectNumPlayersJPFont;
                teamPlayButtonText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = multiplaySelectNumPlayers_JP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplaySelectNumPlayersJPFont;
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
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
        if (numJoysticksConnected < numOfPlayers)
        {
            notEnoughJoysticksText.enabled = true;
            proceedButton.SetActive(false);
        }
        joysticksConnectedText.text = numJoysticksConnected.ToString();
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                joysticksConnectedText.font = multiplaySelectNumPlayersENFont;
                break;
            case Language.DisplayLanauge.Japanese:
                joysticksConnectedText.font = multiplaySelectNumPlayersJPFont;
                joysticksConnectedText.fontStyle = FontStyle.Bold;
                break;
        }
    }

    private void UpdateNumOfPlayers()
    {
        numOfPlayersText.text = numOfPlayers.ToString();
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                numOfPlayersText.font = multiplaySelectNumPlayersENFont;
                break;
            case Language.DisplayLanauge.Japanese:
                numOfPlayersText.font = multiplaySelectNumPlayersJPFont;
                numOfPlayersText.fontStyle = FontStyle.Bold;
                break;
        }
    }

    private void DisplayGameMode()
    {
        if(gameModeText == null) { return; }
        Debug.Log("Display Game Mode: " + MultiplayPlayerMode.gameMode);
        switch (MultiplayPlayerMode.gameMode)
        {
            case "BattleRoyale":
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        gameModeText.text = multiplaySelectNumPlayers_EN.GetLabelContent("BattleRoyale");
                        gameModeText.font = multiplaySelectNumPlayersENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        gameModeText.text = multiplaySelectNumPlayers_JP.GetLabelContent("BattleRoyale");
                        gameModeText.font = multiplaySelectNumPlayersJPFont;
                        gameModeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            case "TeamPlay":
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        gameModeText.text = multiplaySelectNumPlayers_EN.GetLabelContent("TeamPlay");
                        gameModeText.font = multiplaySelectNumPlayersENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        gameModeText.text = multiplaySelectNumPlayers_JP.GetLabelContent("TeamPlay");
                        gameModeText.font = multiplaySelectNumPlayersJPFont;
                        gameModeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
        }
    }

    public void SetNumOfPlayers(int numPlayers)
    {
        numOfPlayers = numPlayers;
        switch (numOfPlayers)
        {
            case 2:
                btn2P.GetComponent<Image>().color = buttonSelectedColor;
                btn3P.GetComponent<Image>().color = buttonNormalColor;
                btn4P.GetComponent<Image>().color = buttonNormalColor;
                break;
            case 3:
                btn2P.GetComponent<Image>().color = buttonNormalColor;
                btn3P.GetComponent<Image>().color = buttonSelectedColor;
                btn4P.GetComponent<Image>().color = buttonNormalColor;
                break;
            case 4:
                btn2P.GetComponent<Image>().color = buttonNormalColor;
                btn3P.GetComponent<Image>().color = buttonNormalColor;
                btn4P.GetComponent<Image>().color = buttonSelectedColor;
                numberOfPlayersCanvas.SetActive(false);
                pleaseWaitCanvas.SetActive(false);
                battleModeCanvas.SetActive(true);
                break;
        }
    }

    public void SetGameMode(string selectedGameMode)
    {
        MultiplayPlayerMode.gameMode = selectedGameMode;
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
        StartCoroutine(LoadLevelAsynchronously(sceneName));
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
        SceneManager.LoadScene("Title");
    }
}
