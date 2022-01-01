using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlTypeManager : MonoBehaviour
{
    [SerializeField] Localization controlTypeEN, controlTypeJP;
    [SerializeField] GameObject contronType1ImageEN, controlType1ImageJP;
    [SerializeField] GameObject contronType2ImageEN, controlType2ImageJP;
    [SerializeField] GameObject controlTypeCanvas, pleaseWaitCanvas;
    [SerializeField] string chooseDifficultyScene;
    [SerializeField] string titleMap;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text controlTypeTitleText, chosenControlTypeText, joystickHintText, proceedText, titleText, pleaseWaitLabelText;
    [SerializeField] Text btnType1Text, btnType2Text;
    [SerializeField] Button btnType1, btnType2;
    [SerializeField] Color buttonNormalColor, buttonSelectedColor;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (Language.gameDisplayLanguage == Language.DisplayLanauge.None)
        {
            Language.gameDisplayLanguage = Language.DisplayLanauge.English;
        }
        controlTypeCanvas.SetActive(true);
        pleaseWaitCanvas.SetActive(false);

        switch (ControlType.chosenControlType)
        {
            case "Type1":
                btnType1.GetComponent<Image>().color = buttonSelectedColor;
                btnType2.GetComponent<Image>().color = buttonNormalColor;
                break;
            case "Type2":
                btnType1.GetComponent<Image>().color = buttonNormalColor;
                btnType2.GetComponent<Image>().color = buttonSelectedColor;
                break;
        }


        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                contronType1ImageEN.SetActive(true);
                contronType2ImageEN.SetActive(false);
                controlType1ImageJP.SetActive(false);
                controlType2ImageJP.SetActive(false);
                break;
            case Language.DisplayLanauge.Japanese:
                contronType1ImageEN.SetActive(false);
                contronType2ImageEN.SetActive(false);
                controlType1ImageJP.SetActive(true);
                controlType2ImageJP.SetActive(false);
                break;
        }


        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                controlTypeTitleText.text = controlTypeEN.GetLabelContent("ControlTypeTitleText");
                joystickHintText.text = "("+controlTypeEN.GetLabelContent("JoystickHintText")+")";
                proceedText.text = controlTypeEN.GetLabelContent("ProceedText");
                titleText.text = controlTypeEN.GetLabelContent("TitleText");
                pleaseWaitLabelText.text = controlTypeEN.GetLabelContent("PleaseWaitLabelText");
                btnType1Text.text = controlTypeEN.GetLabelContent("Type1ButtonText");
                btnType2Text.text = controlTypeEN.GetLabelContent("Type2ButtonText");
                chosenControlTypeText.text = controlTypeEN.GetLabelContent("Type1Text");
                break;
            case Language.DisplayLanauge.Japanese:
                controlTypeTitleText.text = controlTypeJP.GetLabelContent("ControlTypeTitleText");
                controlTypeTitleText.fontStyle = FontStyle.Bold;
                joystickHintText.text = "(" + controlTypeJP.GetLabelContent("JoystickHintText") + ")";
                joystickHintText.fontStyle = FontStyle.Bold;
                proceedText.text = controlTypeJP.GetLabelContent("ProceedText");
                proceedText.fontStyle = FontStyle.Bold;
                titleText.text = controlTypeJP.GetLabelContent("TitleText");
                titleText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = controlTypeJP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                btnType1Text.text = controlTypeJP.GetLabelContent("Type1ButtonText");
                btnType1Text.fontStyle = FontStyle.Bold;
                btnType2Text.text = controlTypeJP.GetLabelContent("Type2ButtonText");
                btnType2Text.fontStyle = FontStyle.Bold;
                chosenControlTypeText.text = controlTypeJP.GetLabelContent("Type1Text");
                chosenControlTypeText.fontStyle = FontStyle.Bold;
                break;
        }
    }

    public void SetControlType(string controlType)
    {
        switch (controlType)
        {
            case "Type1":
                ControlType.chosenControlType = "Type1";
                btnType1.GetComponent<Image>().color = buttonSelectedColor;
                btnType2.GetComponent<Image>().color = buttonNormalColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        contronType1ImageEN.SetActive(true);
                        contronType2ImageEN.SetActive(false);
                        controlType1ImageJP.SetActive(false);
                        controlType2ImageJP.SetActive(false);
                        chosenControlTypeText.text = controlTypeEN.GetLabelContent("Type1Text");
                        break;
                    case Language.DisplayLanauge.Japanese:
                        contronType1ImageEN.SetActive(false);
                        contronType2ImageEN.SetActive(false);
                        controlType1ImageJP.SetActive(true);
                        controlType2ImageJP.SetActive(false);
                        chosenControlTypeText.text = controlTypeJP.GetLabelContent("Type1Text");
                        chosenControlTypeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            case "Type2":
                ControlType.chosenControlType = "Type2";
                btnType1.GetComponent<Image>().color = buttonNormalColor;
                btnType2.GetComponent<Image>().color = buttonSelectedColor;
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        contronType1ImageEN.SetActive(false);
                        contronType2ImageEN.SetActive(true);
                        controlType1ImageJP.SetActive(false);
                        controlType2ImageJP.SetActive(false);
                        chosenControlTypeText.text = controlTypeEN.GetLabelContent("Type2Text");
                        break;
                    case Language.DisplayLanauge.Japanese:
                        contronType1ImageEN.SetActive(false);
                        contronType2ImageEN.SetActive(false);
                        controlType1ImageJP.SetActive(false);
                        controlType2ImageJP.SetActive(true);
                        chosenControlTypeText.text = controlTypeJP.GetLabelContent("Type2Text");
                        chosenControlTypeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void ProcedToChooseDifficultyScene(string chooseDifficultyScene)
    {
        if (chooseDifficultyScene.Equals("")) { return; }
        controlTypeCanvas.SetActive(false);
        pleaseWaitCanvas.SetActive(true);
        StartCoroutine(LoadLevelAsynchronously(chooseDifficultyScene));
    }

    IEnumerator LoadLevelAsynchronously(string targetMap)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetMap);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            //float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log("operation progress: "+operation.progress);
            loadingSlider.value = operation.progress;

            yield return 0;
        }
        operation.allowSceneActivation = true;
        loadingSlider.value = 1f;
        yield return operation;
    }

    public void ToTitle(string titleMap)
    {
        if (titleMap.Equals("")) { return; }
        SceneManager.LoadScene(titleMap);
    }
}
