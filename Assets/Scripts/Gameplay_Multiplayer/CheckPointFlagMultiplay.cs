using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointFlagMultiplay : MonoBehaviour
{
    [SerializeField] string checkPointName;
    [SerializeField] int checkPointIndex;
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
    }
    public void PassGoal()
    {
        GetComponent<Renderer>().material = checkPointFlagMaterial;
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
}
