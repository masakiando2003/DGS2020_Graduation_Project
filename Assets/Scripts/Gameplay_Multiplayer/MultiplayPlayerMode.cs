using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayPlayerMode : MonoBehaviour
{
    public static string gameMode = "BattleRoyale";
    public static string chosenDifficulty = "Easy";
    public static List<int> TeamAPlayerIDs = new List<int>() { 1, 2 };
    public static List<int> TeamBPlayerIDs = new List<int>() { 3, 4 };
}
