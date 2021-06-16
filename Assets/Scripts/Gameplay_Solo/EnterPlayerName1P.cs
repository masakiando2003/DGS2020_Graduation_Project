using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterPlayerName1P : MonoBehaviour
{
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
            SceneManager.LoadScene(difficultyScene);
        }
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
