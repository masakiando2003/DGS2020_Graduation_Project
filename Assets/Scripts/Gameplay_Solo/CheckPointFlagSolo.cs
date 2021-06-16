using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointFlagSolo : MonoBehaviour
{
    [SerializeField] string checkPointName;
    [SerializeField] int checkPointIndex, recoverPlayerLifes;
    [SerializeField] float recoverRemainingTime;
    [SerializeField] Material checkPointFlagMaterial;
    private Material originalMaterial;
    private bool isChecked;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        originalMaterial = GetComponent<Renderer>().material;
        isChecked = false;
    }

    public void PassCheckPoint()
    {
        GetComponent<Renderer>().material = checkPointFlagMaterial;
        FindObjectOfType<GameManagerSolo>().UpdateFinalPosition(checkPointName, checkPointIndex);
    }
    public void PassGoal ()
    {
        GetComponent<Renderer>().material = checkPointFlagMaterial;
        FindObjectOfType<GameManagerSolo>().UpdateFinalPosition("Goal", 99);
    }

    public bool IsChecked()
    {
        return isChecked;
    }

    public void SetToChecked()
    {
        isChecked = true;
    }

    public void ResetCheckPointMaterial()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }

    public int GetRecoverPlayerLifes()
    {
        return recoverPlayerLifes;
    }

    public float GetRecoverRemainingTime()
    {
        return recoverRemainingTime;
    }
}
