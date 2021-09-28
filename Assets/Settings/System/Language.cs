using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public enum DisplayLanauge
    {
        None = 0,
        English,
        Japanese
    }

    public static DisplayLanauge gameDisplayLanguage = DisplayLanauge.None;
}
