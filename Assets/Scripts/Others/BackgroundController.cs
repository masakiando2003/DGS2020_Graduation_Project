using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] Material[] skyboxes;

    private static bool created = false;
    static int skyboxIndex;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (PlayerPrefs.HasKey("BackgroundIndex"))
        {
            skyboxIndex = PlayerPrefs.GetInt("BackgroundIndex");
        }
        else
        {
            skyboxIndex = 0;
        }
        if (skyboxIndex < 0 || skyboxIndex > skyboxes.Length - 1)
        {
            skyboxIndex = 0;
        }
        if (skyboxIndex >= 0 && skyboxIndex <= skyboxes.Length - 1)
        {
            RenderSettings.skybox = skyboxes[skyboxIndex];
        }
        if (!created)
        {
            // this is the first instance -make it persist
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // this must be aduplicate from a scene reload  - DESTROY!
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        SetSkybox();
    }

    private void SetSkybox()
    {
        if (PlayerPrefs.HasKey("BackgroundIndex"))
        {
            skyboxIndex = PlayerPrefs.GetInt("BackgroundIndex");
        }
        else
        {
            skyboxIndex = 0;
        }
        if (skyboxIndex < 0 || skyboxIndex > skyboxes.Length - 1)
        {
            skyboxIndex = 0;
        }
        if (skyboxIndex >= 0 && skyboxIndex <= skyboxes.Length - 1)
        {
            RenderSettings.skybox = skyboxes[skyboxIndex];
        }
    }
}
