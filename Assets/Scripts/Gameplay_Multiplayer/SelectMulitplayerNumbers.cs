using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMulitplayerNumbers : MonoBehaviour
{
    [SerializeField] GameObject numberOfPlayersCanvas, battleModeCanvas;
    [SerializeField] GameObject threePlayersButton, fourPlayersButton, proceedButton;
    [SerializeField] Text numOfPlayersText;
    [SerializeField] Text joysticksConnectedText;
    [SerializeField] Text notEnoughJoysticksText;
    [SerializeField] Text gameModeText;
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
        /*
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
        */
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
            battleModeCanvas.SetActive(true);
        }
    }

    public void SetGameMode(string selectedGameMode)
    {
        MultiplayPlayerMode.gameMode = selectedGameMode;
        Debug.Log("Game Mode: " + MultiplayPlayerMode.gameMode);
        battleModeCanvas.SetActive(false);
        numberOfPlayersCanvas.SetActive(true);
    }

    public void ProceedNextStage(string sceneName)
    {
        PlayerNameTempSaveMultiplay.numPlayers = numOfPlayers;
        if(sceneName == "") { return; }
        SceneManager.LoadScene(sceneName);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
