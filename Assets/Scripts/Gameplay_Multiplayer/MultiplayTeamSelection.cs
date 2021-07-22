using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayTeamSelection : MonoBehaviour
{
    [SerializeField] Text[] playerNameTexts;
    [SerializeField] Button[] teamAPlayerButtons, teamBPlayerButtons;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = PlayerNameTempSaveMultiplay.playerName[i];
        }
        for (int i = 0; i < teamAPlayerButtons.Length; i++)
        {
            var buttonColors = teamAPlayerButtons[i].GetComponent<Button>().colors;
            buttonColors.normalColor = buttonNormalColor;
            teamAPlayerButtons[i].GetComponent<Button>().colors = buttonColors;
        }
        for (int i = 0; i < teamBPlayerButtons.Length; i++)
        {
            var buttonColors = teamBPlayerButtons[i].GetComponent<Button>().colors;
            buttonColors.normalColor = buttonNormalColor;
            teamBPlayerButtons[i].GetComponent<Button>().colors = buttonColors;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int[] teamAIDs = MultiplayPlayerMode.TeamAPlayerIDs;
        int[] teamBIDs = MultiplayPlayerMode.TeamBPlayerIDs;

        for(int i = 0; i < teamAPlayerButtons.Length; i++)
        {
            int playerID = i + 1;
            Debug.Log("Team A player ID: "+playerID);
            Debug.Log("Array Index Of Player ID "+playerID+": "+Array.IndexOf(teamAIDs, playerID));
            if (Array.IndexOf(teamAIDs, playerID) >= 0)
            {
                var buttonColors = teamAPlayerButtons[i].GetComponent<Button>().colors;
                buttonColors.normalColor = buttonSelectedColor;
                buttonColors.highlightedColor = buttonSelectedColor;
                teamAPlayerButtons[i].GetComponent<Button>().colors = buttonColors;
            }
            else
            {
                var buttonColors = teamAPlayerButtons[i].GetComponent<Button>().colors;
                buttonColors.normalColor = buttonNormalColor;
                buttonColors.highlightedColor = buttonNormalColor;
                teamAPlayerButtons[i].GetComponent<Button>().colors = buttonColors;
            }
        }

        for (int i = 0; i < teamBPlayerButtons.Length; i++)
        {
            int playerID = i + 1;
            Debug.Log("Team B player ID: " + playerID);
            Debug.Log("Array Index Of Player ID " + playerID + ": " + Array.IndexOf(teamBIDs, playerID));
            if (Array.IndexOf(teamBIDs, playerID) >= 0)
            {
                var buttonColors = teamBPlayerButtons[i].GetComponent<Button>().colors;
                buttonColors.normalColor = buttonSelectedColor;
                buttonColors.highlightedColor = buttonSelectedColor;
                teamBPlayerButtons[i].GetComponent<Button>().colors = buttonColors;
            }
            else
            {
                var buttonColors = teamBPlayerButtons[i].GetComponent<Button>().colors;
                buttonColors.normalColor = buttonNormalColor;
                buttonColors.highlightedColor = buttonNormalColor;
                teamBPlayerButtons[i].GetComponent<Button>().colors = buttonColors;
            }
        }
    }

    public void SetTeam(string team, int playerID)
    {
        if (team.Equals("Team A"))
        {

        }
    }

    public void ResetTeam()
    {
        MultiplayPlayerMode.TeamAPlayerIDs = new int[2];
        MultiplayPlayerMode.TeamBPlayerIDs = new int[2];
    }
}
