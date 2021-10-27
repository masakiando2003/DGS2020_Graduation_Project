using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] Localization creditEN, creditJP;
    [SerializeField] GameObject developersSection, unityAssetsSection, othersSection;
    [SerializeField] GameObject developersDetailTextEN, developersDetailTextJP;
    [SerializeField] GameObject unityAssetsDetailTextEN, unityAssetsDetailTextJP;
    [SerializeField] GameObject othersDetailTextEN, othersDetailTextJP;
    [SerializeField] Text developersButtonText, unityAssetsButtonText, othersButtonText, returnButtonText;
    [SerializeField] string titleMap;

    public void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        developersSection.SetActive(true);
        unityAssetsSection.SetActive(false);
        othersSection.SetActive(false);
        if(Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                developersButtonText.text = creditEN.GetLabelContent("DevelopersButtonText");
                unityAssetsButtonText.text = creditEN.GetLabelContent("UnityAssetsButtonText");
                othersButtonText.text = creditEN.GetLabelContent("OthersButtonText");
                returnButtonText.text = creditEN.GetLabelContent("ReturnButtonText");
                developersDetailTextEN.SetActive(true);
                unityAssetsDetailTextEN.SetActive(true);
                othersDetailTextEN.SetActive(true);
                developersDetailTextJP.SetActive(false);
                unityAssetsDetailTextJP.SetActive(false);
                othersDetailTextJP.SetActive(false);
                break;
            case Language.DisplayLanauge.Japanese:
                developersButtonText.text = creditJP.GetLabelContent("DevelopersButtonText");
                developersButtonText.fontStyle = FontStyle.Bold;
                unityAssetsButtonText.text = creditJP.GetLabelContent("UnityAssetsButtonText");
                unityAssetsButtonText.fontStyle = FontStyle.Bold;
                othersButtonText.text = creditJP.GetLabelContent("OthersButtonText");
                othersButtonText.fontStyle = FontStyle.Bold;
                returnButtonText.text = creditJP.GetLabelContent("ReturnButtonText");
                returnButtonText.fontStyle = FontStyle.Bold;
                developersDetailTextEN.SetActive(false);
                unityAssetsDetailTextEN.SetActive(false);
                othersDetailTextEN.SetActive(false);
                developersDetailTextJP.SetActive(true);
                unityAssetsDetailTextJP.SetActive(true);
                othersDetailTextJP.SetActive(true);
                break;
        }
    }

    public void ShowDevelopersSection()
    {
        developersSection.SetActive(true);
        unityAssetsSection.SetActive(false);
        othersSection.SetActive(false);
    }

    public void ShowUnityAssetsSection()
    {
        developersSection.SetActive(false);
        unityAssetsSection.SetActive(true);
        othersSection.SetActive(false);
    }
    public void ShowOthersSection()
    {
        developersSection.SetActive(false);
        unityAssetsSection.SetActive(false);
        othersSection.SetActive(true);
    }
    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }

}
