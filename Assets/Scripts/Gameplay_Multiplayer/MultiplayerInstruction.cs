using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerInstruction : MonoBehaviour
{
    [SerializeField] GameObject instructionCanvas;
    [SerializeField] string titleMap;

    public void StartGame()
    {
        switch (MultiplayPlayerMode.chosenDifficulty)
        {
            case "Easy":
                LoadMapBaseOnSettings("Easy", PlayerNameTempSaveMultiplay.numPlayers);
                break;
            case "Normal":
                LoadMapBaseOnSettings("Normal", PlayerNameTempSaveMultiplay.numPlayers);
                break;
            case "Hard":
                LoadMapBaseOnSettings("Hard", PlayerNameTempSaveMultiplay.numPlayers);
                break;
        }
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }

    private void LoadMapBaseOnSettings(string difficulty, int numOfPlayers)
    {
        string mapName = "Multiplay_Stage_" + difficulty + "_" + numOfPlayers + "P";
        SceneManager.LoadScene(mapName);
    }
}
