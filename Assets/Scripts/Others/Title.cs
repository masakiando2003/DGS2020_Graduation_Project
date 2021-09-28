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
            string getLanguage = PlayerPrefs.GetString("currentLanguage");
            switch (getLanguage)
            {
                case "English":
                    Language.gameDisplayLanguage = Language.DisplayLanauge.English;
                    break;
                case "Japanese":
                    Language.gameDisplayLanguage = Language.DisplayLanauge.Japanese;
                    break;
            }
        }
        else
        {
            // Default as English
            if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
            {
                Language.gameDisplayLanguage = Language.DisplayLanauge.English;
                PlayerPrefs.SetString("currentLanguage", "English");
                PlayerPrefs.Save();
            }
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
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                soloPlayerStartText.text = title_EN.GetLabelContent("SoloPlay");
                soloPlayerStartText.fontStyle = FontStyle.Normal;
                multiPlayerStartText.text = title_EN.GetLabelContent("MultiPlay");
                multiPlayerStartText.fontStyle = FontStyle.Normal;
                creditsText.text = title_EN.GetLabelContent("Credits");
                creditsText.fontStyle = FontStyle.Normal;
                break;
            case Language.DisplayLanauge.Japanese:
                soloPlayerStartText.text = title_JP.GetLabelContent("SoloPlay");
                soloPlayerStartText.fontStyle = FontStyle.Bold;
                multiPlayerStartText.text = title_JP.GetLabelContent("MultiPlay");
                multiPlayerStartText.fontStyle = FontStyle.Bold;
                creditsText.text = title_JP.GetLabelContent("Credits");
                creditsText.fontStyle = FontStyle.Bold;
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
        switch (selectedLanauge)
        {
            case "English":
                Language.gameDisplayLanguage = Language.DisplayLanauge.English;
                break;
            case "Japanese":
                Language.gameDisplayLanguage = Language.DisplayLanauge.Japanese;
                break;
        }
        PlayerPrefs.SetString("currentLanguage", selectedLanauge);
        PlayerPrefs.Save();
    }
}
