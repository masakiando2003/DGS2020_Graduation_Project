using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    CreateAssetMenu(
        fileName = "Localization",
        menuName = "Localization"
    )
]
public class Localization : ScriptableObject
{
    public List<string> labelName, labelText;

    public string GetLabelContent(string specifiedLabel)
    {
        int labelIndex = labelName.FindIndex(n => n == specifiedLabel);
        return labelText[labelIndex];
    }
}
