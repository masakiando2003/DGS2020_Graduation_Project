using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] Localization title_EN, title_JP;
    [SerializeField] string soloPlayerInstructionSceneName;
    [SerializeField] string multiPlayerInstructionSceneName;
    [SerializeField] string creditSceneName;
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] GameObject rocketMesh;
    [SerializeField] Text soloPlayerStartText, multiPlayerStartText, creditsText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("currentLanaguage"))
        {
            Language.displayLanguage = PlayerPrefs.GetString("currentLanguage");
        }
        else
        {
            // Default as English
            Language.displayLanguage = "English";
            PlayerPrefs.SetString("currentLanguage", "English");
            PlayerPrefs.Save();
        }
    }

    public void SoloPlayerStart()
    {
        SceneManager.LoadScene(soloPlayerInstructionSceneName);
    }

    public void MultiPlayerStart()
    {
        SceneManager.LoadScene(multiPlayerInstructionSceneName);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditSceneName);
    }

    private void Update()
    {
        RotateRocketMeshContinously();
        SelectLanguage();
    }

    private void SelectLanguage()
    {
        switch (Language.displayLanguage)
        {
            case "English":
                soloPlayerStartText.text = title_EN.GetLabelContent("SoloPlay");
                multiPlayerStartText.text = title_EN.GetLabelContent("MultiPlay");
                creditsText.text = title_EN.GetLabelContent("Credits");
                break;
            case "Japanese":
                soloPlayerStartText.text = title_JP.GetLabelContent("SoloPlay");
                multiPlayerStartText.text = title_JP.GetLabelContent("MultiPlay");
                creditsText.text = title_JP.GetLabelContent("Credits");
                break;
        }
    }

    private void RotateRocketMeshContinously()
    {
        Time.timeScale = 1f;
        rocketMesh.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public void SetCurrentLanguage(string selectedLanauge)
    {
        Language.displayLanguage = selectedLanauge;
        PlayerPrefs.SetString("currentLanguage", selectedLanauge);
        PlayerPrefs.Save();
    }
}
