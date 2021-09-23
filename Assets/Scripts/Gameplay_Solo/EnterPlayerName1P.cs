using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterPlayerName1P : MonoBehaviour
{
    [SerializeField] Localization enterPlayerName1P_EN, enterPlayerName1P_JP;
    [SerializeField] GameObject playerNameCanvas, pleaseWaitCanvas;
    [SerializeField] string difficultyScene;
    [SerializeField] string titleMap;
    [SerializeField] InputField playerNameInput;
    [SerializeField] Text errorText, playerNameTitleText, placeholderText, pleaseWaitLabelText, proceedText, titleText;

    private void Start()
    {
        Initialization();
        SwitchLanguage();
    }

    private void Initialization()
    {
        playerNameCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
        errorText.enabled = false;
    }

    private void SwitchLanguage()
    {
        Debug.Log("Display Language: "+Language.displayLanguage);
        switch (Language.displayLanguage)
        {
            case "English":
                errorText.text = enterPlayerName1P_EN.GetLabelContent("ErrorText");
                playerNameTitleText.text = enterPlayerName1P_EN.GetLabelContent("PlayerNameTitleText");
                placeholderText.text = enterPlayerName1P_EN.GetLabelContent("PlaceholderText");
                pleaseWaitLabelText.text = enterPlayerName1P_EN.GetLabelContent("PleaseWaitLabelText");
                proceedText.text = enterPlayerName1P_EN.GetLabelContent("ProceedText");
                titleText.text = enterPlayerName1P_EN.GetLabelContent("TitleText");
                break;
            case "Japanese":
                errorText.text = enterPlayerName1P_JP.GetLabelContent("ErrorText");
                playerNameTitleText.text = enterPlayerName1P_JP.GetLabelContent("PlayerNameTitleText");
                placeholderText.text = enterPlayerName1P_JP.GetLabelContent("PlaceholderText");
                pleaseWaitLabelText.text = enterPlayerName1P_JP.GetLabelContent("PleaseWaitLabelText");
                proceedText.text = enterPlayerName1P_JP.GetLabelContent("ProceedText");
                titleText.text = enterPlayerName1P_JP.GetLabelContent("TitleText");
                break;
        }
    }

    public void Proceed()
    {
        if (playerNameInput.text == "" || playerNameInput.text == null)
        {
            if(errorText == null) { return; }
            errorText.enabled = true;
        }
        else
        {
            if (errorText == null) { return; }
            errorText.enabled = false;
            errorText.text = "";
            PlayerNameTempSaveSolo.playerName = playerNameInput.text.ToString();
            if (difficultyScene.Equals("")) { return; }
            playerNameCanvas.SetActive(false);
            pleaseWaitCanvas.SetActive(true);
            SceneManager.LoadScene(difficultyScene);
        }
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
