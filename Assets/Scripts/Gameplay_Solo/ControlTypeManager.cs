using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlTypeManager : MonoBehaviour
{
    [SerializeField] Localization controlTypeEN, controlTypeJP;
    [SerializeField] Font controlTypeENFont, controlTypeJPFont;
    [SerializeField] GameObject contronType1Keyboard, controlType1Mouse, controlType2Keyboard;
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
                contronType1Keyboard.SetActive(true);
                controlType1Mouse.SetActive(true);
                controlType2Keyboard.SetActive(false);
                break;
            case "Type2":
                btnType1.GetComponent<Image>().color = buttonNormalColor;
                btnType2.GetComponent<Image>().color = buttonSelectedColor;
                contronType1Keyboard.SetActive(false);
                controlType1Mouse.SetActive(false);
                controlType2Keyboard.SetActive(true);
                break;
        }


        switch (Language.gameDisplayLanguage)
        {
            case Language.DisplayLanauge.English:
                controlTypeTitleText.text = controlTypeEN.GetLabelContent("ControlTypeTitleText");
                controlTypeTitleText.font = controlTypeENFont;
                joystickHintText.text = "("+controlTypeEN.GetLabelContent("JoystickHintText")+")";
                joystickHintText.font = controlTypeENFont;
                proceedText.text = controlTypeEN.GetLabelContent("ProceedText");
                proceedText.font = controlTypeENFont;
                titleText.text = controlTypeEN.GetLabelContent("TitleText");
                titleText.font = controlTypeENFont;
                pleaseWaitLabelText.text = controlTypeEN.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = controlTypeENFont;
                btnType1Text.text = controlTypeEN.GetLabelContent("Type1ButtonText");
                btnType1Text.font = controlTypeENFont;
                btnType2Text.text = controlTypeEN.GetLabelContent("Type2ButtonText");
                btnType2Text.font = controlTypeENFont;
                chosenControlTypeText.text = controlTypeEN.GetLabelContent("Type1Text");
                chosenControlTypeText.font = controlTypeENFont;
                break;
            case Language.DisplayLanauge.Japanese:
                controlTypeTitleText.text = controlTypeJP.GetLabelContent("ControlTypeTitleText");
                controlTypeTitleText.font = controlTypeJPFont;
                controlTypeTitleText.fontStyle = FontStyle.Bold;
                joystickHintText.text = "(" + controlTypeJP.GetLabelContent("JoystickHintText") + ")";
                joystickHintText.font = controlTypeJPFont;
                joystickHintText.fontStyle = FontStyle.Bold;
                proceedText.text = controlTypeJP.GetLabelContent("ProceedText");
                proceedText.font = controlTypeJPFont;
                proceedText.fontStyle = FontStyle.Bold;
                titleText.text = controlTypeJP.GetLabelContent("TitleText");
                titleText.font = controlTypeJPFont;
                titleText.fontStyle = FontStyle.Bold;
                pleaseWaitLabelText.text = controlTypeJP.GetLabelContent("PleaseWaitLabelText");
                pleaseWaitLabelText.font = controlTypeJPFont;
                pleaseWaitLabelText.fontStyle = FontStyle.Bold;
                btnType1Text.text = controlTypeJP.GetLabelContent("Type1ButtonText");
                btnType1Text.font = controlTypeJPFont;
                btnType1Text.fontStyle = FontStyle.Bold;
                btnType2Text.text = controlTypeJP.GetLabelContent("Type2ButtonText");
                btnType2Text.font = controlTypeJPFont;
                btnType2Text.fontStyle = FontStyle.Bold;
                chosenControlTypeText.text = controlTypeJP.GetLabelContent("Type1Text");
                chosenControlTypeText.font = controlTypeJPFont;
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
                contronType1Keyboard.SetActive(true);
                controlType1Mouse.SetActive(true);
                controlType2Keyboard.SetActive(false);
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        chosenControlTypeText.text = controlTypeEN.GetLabelContent("Type1Text");
                        chosenControlTypeText.font = controlTypeENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        chosenControlTypeText.text = controlTypeJP.GetLabelContent("Type1Text");
                        chosenControlTypeText.font = controlTypeJPFont;
                        chosenControlTypeText.fontStyle = FontStyle.Bold;
                        break;
                }
                break;
            case "Type2":
                ControlType.chosenControlType = "Type2";
                btnType1.GetComponent<Image>().color = buttonNormalColor;
                btnType2.GetComponent<Image>().color = buttonSelectedColor;
                contronType1Keyboard.SetActive(false);
                controlType1Mouse.SetActive(false);
                controlType2Keyboard.SetActive(true);
                switch (Language.gameDisplayLanguage)
                {
                    case Language.DisplayLanauge.English:
                        chosenControlTypeText.text = controlTypeEN.GetLabelContent("Type2Text");
                        chosenControlTypeText.font = controlTypeENFont;
                        break;
                    case Language.DisplayLanauge.Japanese:
                        chosenControlTypeText.text = controlTypeJP.GetLabelContent("Type2Text");
                        chosenControlTypeText.font = controlTypeJPFont;
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
