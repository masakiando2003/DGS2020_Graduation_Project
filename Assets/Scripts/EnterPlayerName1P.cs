using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterPlayerName1P : MonoBehaviour
{
    [SerializeField] string instructionMap;
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
            PlayerNameTempSave_1P.playerName = playerNameInput.text.ToString();
            if (instructionMap.Equals("")) { return; }
            SceneManager.LoadScene(instructionMap);
        }
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
