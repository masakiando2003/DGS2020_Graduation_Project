using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MultiplayTeamSelection : MonoBehaviour
{
    [SerializeField] GameObject difficultyCanvas, pleaseWaitCanvas;
    [SerializeField] Text teamNotSelectedFinishedYetText;
    [SerializeField] Text[] playerNameTexts;
    [SerializeField] Button[] teamAPlayerButtons, teamBPlayerButtons;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        difficultyCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
        teamNotSelectedFinishedYetText.enabled = false;
        List<int> teamAPlayerIDs = MultiplayPlayerMode.TeamAPlayerIDs;
        List<int> teamBPlayerIDs = MultiplayPlayerMode.TeamBPlayerIDs;

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = PlayerNameTempSaveMultiplay.playerName[i];
        }
        for (int i = 0; i < teamAPlayerButtons.Length; i++)
        {
            int playerID = i + 1;
            if (teamAPlayerIDs.Contains(playerID))
            {
                teamAPlayerButtons[i].GetComponent<Image>().color = buttonSelectedColor;
                teamBPlayerButtons[i].interactable = false;
            }
            else
            {
                teamAPlayerButtons[i].GetComponent<Image>().color = buttonNormalColor;
            }
        }
        for (int i = 0; i < teamBPlayerButtons.Length; i++)
        {
            int playerID = i + 1;
            if (teamBPlayerIDs.Contains(playerID))
            {
                teamBPlayerButtons[i].GetComponent<Image>().color = buttonSelectedColor;
                teamAPlayerButtons[i].interactable = false;
            }
            else
            {
                teamBPlayerButtons[i].GetComponent<Image>().color = buttonNormalColor;
            }
        }
    }

    public void SetTeam(string playerID_Team)
    {
        string[] playerID_Team_Info = playerID_Team.Split('_');
        int playerID = int.Parse(playerID_Team_Info[0]);
        int playerIndex = playerID - 1;
        string team = playerID_Team_Info[1];

        if (team.Equals("TeamA"))
        {
            if(MultiplayPlayerMode.TeamAPlayerIDs.Count >= 2)
            {
                teamNotSelectedFinishedYetText.text = "Team A has already 2 members!";
                teamNotSelectedFinishedYetText.enabled = true;
            }
            else if(!MultiplayPlayerMode.TeamAPlayerIDs.Contains(playerID))
            {
                teamNotSelectedFinishedYetText.enabled = false;
                MultiplayPlayerMode.TeamAPlayerIDs.Add(playerID);
                teamAPlayerButtons[playerIndex].GetComponent<Image>().color = buttonSelectedColor;
                teamBPlayerButtons[playerIndex].interactable = false;

            }
        }
        else
        {
            if (MultiplayPlayerMode.TeamBPlayerIDs.Count >= 2)
            {
                teamNotSelectedFinishedYetText.text = "Team B has already 2 members!";
                teamNotSelectedFinishedYetText.enabled = true;
            }
            else if (!MultiplayPlayerMode.TeamBPlayerIDs.Contains(playerID))
            {
                teamNotSelectedFinishedYetText.enabled = false;
                MultiplayPlayerMode.TeamBPlayerIDs.Add(playerID);
                teamBPlayerButtons[playerIndex].GetComponent<Image>().color = buttonSelectedColor;
                teamAPlayerButtons[playerIndex].interactable = false;

            }
        }
    }

    public void ResetTeam()
    {
        MultiplayPlayerMode.TeamAPlayerIDs = new List<int>();
        MultiplayPlayerMode.TeamBPlayerIDs = new List<int>();

        for (int i = 0; i < teamAPlayerButtons.Length; i++)
        {
            teamAPlayerButtons[i].GetComponent<Image>().color = buttonNormalColor;
            teamAPlayerButtons[i].interactable = true;
        }

        for (int i = 0; i < teamBPlayerButtons.Length; i++)
        {
            teamBPlayerButtons[i].GetComponent<Image>().color = buttonNormalColor;
            teamBPlayerButtons[i].interactable = true;
        }

        teamNotSelectedFinishedYetText.enabled = false;
    }

    public void RandomTeam()
    {
        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            teamAPlayerButtons[i].GetComponent<Image>().color = buttonNormalColor;
            teamAPlayerButtons[i].interactable = true;

            teamBPlayerButtons[i].GetComponent<Image>().color = buttonNormalColor;
            teamBPlayerButtons[i].interactable = true;
        }

        MultiplayPlayerMode.TeamAPlayerIDs = new List<int>();
        MultiplayPlayerMode.TeamBPlayerIDs = new List<int>();

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            int playerID = i + 1;
            int randomedTeam = Random.Range(1, 3);
            if (randomedTeam == 1)
            {
                if (MultiplayPlayerMode.TeamAPlayerIDs.Count < 2 && !MultiplayPlayerMode.TeamAPlayerIDs.Contains(playerID))
                {
                    MultiplayPlayerMode.TeamAPlayerIDs.Add(playerID);
                    teamAPlayerButtons[i].GetComponent<Image>().color = buttonSelectedColor;
                    teamBPlayerButtons[i].interactable = false;
                }
                else
                {
                    MultiplayPlayerMode.TeamBPlayerIDs.Add(playerID);
                    teamBPlayerButtons[i].GetComponent<Image>().color = buttonSelectedColor;
                    teamAPlayerButtons[i].interactable = false;
                }
            }
            else if(randomedTeam == 2)
            {

                if (MultiplayPlayerMode.TeamBPlayerIDs.Count < 2 && !MultiplayPlayerMode.TeamBPlayerIDs.Contains(playerID))
                {
                    MultiplayPlayerMode.TeamBPlayerIDs.Add(playerID);
                    teamBPlayerButtons[i].GetComponent<Image>().color = buttonSelectedColor;
                    teamAPlayerButtons[i].interactable = false;
                }
                else
                {
                    MultiplayPlayerMode.TeamAPlayerIDs.Add(playerID);
                    teamAPlayerButtons[i].GetComponent<Image>().color = buttonSelectedColor;
                    teamBPlayerButtons[i].interactable = false;
                }
            }
        }
        ShowListContentsInTheDebugLog(MultiplayPlayerMode.TeamAPlayerIDs);
        ShowListContentsInTheDebugLog(MultiplayPlayerMode.TeamBPlayerIDs);
        teamNotSelectedFinishedYetText.enabled = false;
    }

    public void Proceed(string chooseDifficultyScene)
    {
        List<int> teamAPlayerIDs = MultiplayPlayerMode.TeamAPlayerIDs;
        List<int> teamBPlayerIDs = MultiplayPlayerMode.TeamBPlayerIDs;
        ShowListContentsInTheDebugLog(MultiplayPlayerMode.TeamAPlayerIDs);
        ShowListContentsInTheDebugLog(MultiplayPlayerMode.TeamBPlayerIDs);
        if (teamAPlayerIDs.Count < 2 || teamBPlayerIDs.Count < 2)
        {
            teamNotSelectedFinishedYetText.enabled = true;
        }

        if (chooseDifficultyScene.Equals("")) { return; }
        difficultyCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        SceneManager.LoadScene(chooseDifficultyScene);
    }

    private void ShowListContentsInTheDebugLog<T>(List<T> list)
    {
        string log = "";

        foreach (var content in list.Select((val, idx) => new { val, idx }))
        {
            if (content.idx == list.Count - 1)
                log += content.val.ToString();
            else
                log += content.val.ToString() + ", ";
        }

        Debug.Log(log);
    }
}
