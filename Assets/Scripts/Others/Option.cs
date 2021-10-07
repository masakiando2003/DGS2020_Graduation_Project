using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] Localization option_EN, option_JP;
    [SerializeField] Text optionTitleText, titleButtonText;
    [SerializeField] Text easyButtonText, normalButtonText, hardButtonText;
    [SerializeField] Text bgmVolumeText, seVolumeText, soloPlayRankingDataText;
    [SerializeField] Text bgmVolumeValueText, seVolumeValueText;
    [SerializeField] Slider bgmVolumeSlider, seVolumeSlider;
    [SerializeField] GameObject soloEasyRankingCanvas, soloNormalRankingCanvas, soloHardRankingCanvas;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                optionTitleText.text = option_EN.GetLabelContent("OptionTitleText");
                titleButtonText.text = option_EN.GetLabelContent("TitleButtonText");
                easyButtonText.text = option_EN.GetLabelContent("EasyButtonText");
                normalButtonText.text = option_EN.GetLabelContent("NormalButtonText");
                hardButtonText.text = option_EN.GetLabelContent("HardButtonText");
                bgmVolumeText.text = option_EN.GetLabelContent("BGMVolumeText");
                seVolumeText.text = option_EN.GetLabelContent("SEVolumeText");
                soloPlayRankingDataText.text = option_EN.GetLabelContent("SoloPlayRankingDataText").Replace("|", System.Environment.NewLine);
                break;
            case Language.DisplayLanauge.Japanese:
                optionTitleText.text = option_JP.GetLabelContent("OptionTitleText");
                titleButtonText.text = option_JP.GetLabelContent("TitleButtonText");
                easyButtonText.text = option_JP.GetLabelContent("EasyButtonText");
                normalButtonText.text = option_JP.GetLabelContent("NormalButtonText");
                hardButtonText.text = option_JP.GetLabelContent("HardButtonText");
                bgmVolumeText.text = option_JP.GetLabelContent("BGMVolumeText");
                seVolumeText.text = option_JP.GetLabelContent("SEVolumeText");
                soloPlayRankingDataText.text = option_JP.GetLabelContent("SoloPlayRankingDataText").Replace("|", System.Environment.NewLine);
                break;
        }
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            int bgmVolume = PlayerPrefs.GetInt("BGMVolume");
            bgmVolumeSlider.value = bgmVolume;

        }
        if (PlayerPrefs.HasKey("SEVolume"))
        {
            int seVolume = PlayerPrefs.GetInt("SEVolume");
            seVolumeSlider.value = seVolume;

        }
        soloEasyRankingCanvas.SetActive(false);
        soloNormalRankingCanvas.SetActive(false);
        soloHardRankingCanvas.SetActive(false);
    }

    public void ToTitle(string titleMap)
    {
        SceneManager.LoadScene(titleMap);
    }

    public void ChangeBGMVolume()
    {
        PlayerPrefs.SetInt("BGMVolume", (int)bgmVolumeSlider.value);
        bgmVolumeValueText.text = bgmVolumeSlider.value.ToString();
        BGMController.SetBGMVolume((int)bgmVolumeSlider.value);
    }
    public void ChangeSEVolume()
    {
        PlayerPrefs.SetInt("SEVolume", (int)seVolumeSlider.value);
        seVolumeValueText.text = seVolumeSlider.value.ToString();
        BGMController.SetSEVolume((int)seVolumeSlider.value);
    }
}
