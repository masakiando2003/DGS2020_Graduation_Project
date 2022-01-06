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
    [SerializeField] Localization multiplayTeamSelection_EN, multiplayTeamSelection_JP;
    [SerializeField] Font multiplayTeamSelectionENFont, multiplayTeamSelectionJPFont;
    [SerializeField] GameObject difficultyCanvas, pleaseWaitCanvas;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text selectTeamText, teamNotSelectedFinishedYetText, pleaseWaitLabelText;
    [SerializeField] Text proceedButtonText, resetButtonText, randomTeamButtonText;
    [SerializeField] Text[] playerNameTexts;
    [SerializeField] Text[] teamAButtonText, teamBButtonText;
    [SerializeField] Button[] teamAPlayerButtons, teamBPlayerButtons;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.Japanese;
        }
        difficultyCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);
        teamNotSelectedFinishedYetText.enabled = false;
        List<int> teamAPlayerIDs = MultiplayPlayerMode.TeamAPlayerIDs;
        List<int> teamBPlayerIDs = MultiplayPlayerMode.TeamBPlayerIDs;

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = PlayerNameTempSaveMultiplay.playerName[i];
            switch (Language.gameDisplayLanguage)
            {
                case Language.DisplayLanauge.English:
                    playerNameTexts[i].font = multiplayTeamSelectionENFont;
                    break;
                case Language.DisplayLanauge.Japanese:
                    playerNameTexts[i].font = multiplayTeamSelectionJPFont;
                    playerNameTexts[i].fontStyle = FontStyle.Bold;
                    break;
                default:
                    break;
            }
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
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                proceedButtonText.text = multiplayTeamSelection_EN.GetLabelContent("ProceedButtonText");
                proceedButtonText.font = multiplayTeamSelectionENFont;
                resetButtonText.text = multiplayTeamSelection_EN.GetLabelContent("ResetButtonText");
                resetButtonText.font = multiplayTeamSelectionENFont;
                randomTeamButtonText.text = multiplayTeamSelection_EN.GetLabelContent("RandomTeamButtonText");
                randomTeamButtonText.font = multiplayTeamSelectionENFont;
                selectTeamText.text = multiplayTeamSelection_EN.GetLabelContent("SelectTeamText");
                selectTeamText.font = multiplayTeamSelectionENFont;
                teamNotSelectedFinishedYetText.text = multiplayTeamSelection_EN.GetLabelContent("TeamNotSelectedFinishedYetText");
                teamNotSelectedFinishedYetText.font = multiplayTeamSelectionENFont;
                pleaseWaitLabelText.text = multiplayTeamSelection_EN.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplayTeamSelectionENFont;
                for (int i = 0; i < teamAButtonText.Length; i++)
                {
                    teamAButtonText[i].text = multiplayTeamSelection_EN.GetLabelContent("TeamAButtonText");
                    teamAButtonText[i].font = multiplayTeamSelectionENFont;
                }
                for (int i = 0; i < teamBButtonText.Length; i++)
                {
                    teamBButtonText[i].text = multiplayTeamSelection_EN.GetLabelContent("TeamBButtonText");
                    teamBButtonText[i].font = multiplayTeamSelectionENFont;
                }
                break;
            case Language.DisplayLanauge.Japanese:
                proceedButtonText.text = multiplayTeamSelection_JP.GetLabelContent("ProceedButtonText");
                proceedButtonText.font = multiplayTeamSelectionJPFont;
                proceedButtonText.fontStyle = FontStyle.Bold;
                resetButtonText.text = multiplayTeamSelection_JP.GetLabelContent("ResetButtonText");
                resetButtonText.font = multiplayTeamSelectionJPFont;
                resetButtonText.fontStyle = FontStyle.Bold;
                randomTeamButtonText.text = multiplayTeamSelection_JP.GetLabelContent("RandomTeamButtonText");
                randomTeamButtonText.font = multiplayTeamSelectionJPFont;
                randomTeamButtonText.fontStyle = FontStyle.Bold;
                selectTeamText.text = multiplayTeamSelection_JP.GetLabelContent("SelectTeamText");
                selectTeamText.font = multiplayTeamSelectionJPFont;
                selectTeamText.fontStyle = FontStyle.Bold;
                teamNotSelectedFinishedYetText.text = multiplayTeamSelection_JP.GetLabelContent("TeamNotSelectedFinishedYetText");
                teamNotSelectedFinishedYetText.font = multiplayTeamSelectionJPFont;
                teamNotSelectedFinishedYetText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = multiplayTeamSelection_JP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = multiplayTeamSelectionJPFont;
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                for (int i = 0; i < teamAButtonText.Length; i++)
                {
                    teamAButtonText[i].text = multiplayTeamSelection_JP.GetLabelContent("TeamAButtonText");
                    teamAButtonText[i].font = multiplayTeamSelectionJPFont;
                    teamAButtonText[i].fontStyle = FontStyle.Bold;
                }
                for (int i = 0; i < teamBButtonText.Length; i++)
                {
                    teamBButtonText[i].text = multiplayTeamSelection_JP.GetLabelContent("TeamBButtonText");
                    teamBButtonText[i].font = multiplayTeamSelectionJPFont;
                    teamBButtonText[i].fontStyle = FontStyle.Bold;
                }
                break;
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
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        teamNotSelectedFinishedYetText.text = "Team A " + multiplayTeamSelection_EN.GetLabelContent("FullMembersText");
                        teamNotSelectedFinishedYetText.font = multiplayTeamSelectionENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        teamNotSelectedFinishedYetText.text = "チームA " + multiplayTeamSelection_JP.GetLabelContent("FullMembersText");
                        teamNotSelectedFinishedYetText.font = multiplayTeamSelectionJPFont;
                        teamNotSelectedFinishedYetText.fontStyle = FontStyle.Bold;
                        break;
                }
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
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        teamNotSelectedFinishedYetText.text = "Team B " + multiplayTeamSelection_EN.GetLabelContent("FullMembersText");
                        teamNotSelectedFinishedYetText.font = multiplayTeamSelectionENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        teamNotSelectedFinishedYetText.text = "チームB " + multiplayTeamSelection_JP.GetLabelContent("FullMembersText");
                        teamNotSelectedFinishedYetText.font = multiplayTeamSelectionJPFont;
                        teamNotSelectedFinishedYetText.fontStyle = FontStyle.Bold;
                        break;
                }
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
        StartCoroutine(LoadLevelAsynchronously(chooseDifficultyScene));
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

        //Debug.Log(log);
    }
}
