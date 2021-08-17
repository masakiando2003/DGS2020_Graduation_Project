using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterPlayerNameMultiplay : MonoBehaviour
{
    [SerializeField] string difficultyScene;
    [SerializeField] string teamSelectionScene;
    [SerializeField] string titleMap;
    [SerializeField] GameObject[] playerNameObjects;
    [SerializeField] InputField[] playerNameInput;
    [SerializeField] Text errorText;

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
                errorText.text = "Player "+(playerIndex+1)+" 's name cannot be empty!";
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
            if(MultiplayPlayerMode.gameMode.Equals("BattleRoyale"))
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
