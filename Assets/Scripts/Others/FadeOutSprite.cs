using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutSprite : MonoBehaviour
{
    [SerializeField] float fadeOutTime = 1.0f;
    [SerializeField] float fadeOutPeriodTime = 2.0f;

    Image targetImage;
    private float timer;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= fadeOutPeriodTime / 0.5)
        {
            GetComponent<Image>().CrossFadeAlpha(1f, fadeOutTime, false);
        }
        if (timer >= fadeOutPeriodTime)
        {
            GetComponent<Image>().CrossFadeAlpha(0f, fadeOutTime, false);
            timer = 0;
        }
    }
}
