using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject developersSection, unityAssetsSection, othersSection;
    [SerializeField] string titleMap;

    public void ToTitle()
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }

    public void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        developersSection.SetActive(true);
        unityAssetsSection.SetActive(false);
        othersSection.SetActive(false);
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
}
