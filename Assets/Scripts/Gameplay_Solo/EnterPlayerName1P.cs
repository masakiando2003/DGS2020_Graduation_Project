using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterPlayerName1P : MonoBehaviour
{
    [SerializeField] GameObject playerNameCanvas, pleaseWaitCanvas;
    [SerializeField] string difficultyScene;
    [SerializeField] string titleMap;
    [SerializeField] InputField playerNameInput;
    [SerializeField] Text errorText;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        playerNameCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
        errorText.enabled = false;
    }

    public void Proceed()
    {
        if (playerNameInput.text == "" || playerNameInput.text == null)
        {
            if(errorText == null) { return; }
            errorText.enabled = true;
            errorText.text = "Your name cannot be empty!";
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
