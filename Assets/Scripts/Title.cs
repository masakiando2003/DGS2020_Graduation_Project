using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] string soloPlayerInstructionSceneName;
    [SerializeField] string multiPlayerInstructionSceneName;
    [SerializeField] string creditSceneName;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] GameObject rocketMesh;

    public void SoloPlayerStart()
    {
        SceneManager.LoadScene(soloPlayerInstructionSceneName);
    }

    public void MultiPlayerStart()
    {
        SceneManager.LoadScene(multiPlayerInstructionSceneName);
    }

    public void Credit()
    {
        SceneManager.LoadScene(creditSceneName);
    }

    private void Update()
    {
        RotateRocketMeshContinously();
    }

    private void RotateRocketMeshContinously()
    {
        Time.timeScale = 1f;
        rocketMesh.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
