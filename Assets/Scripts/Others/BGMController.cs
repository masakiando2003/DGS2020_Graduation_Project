using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioSource titleBGMAudioSource, instructionBGMAudioSource, stageBGMAudioSource;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        SwitchBGM();
    }

    private void SwitchBGM()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name.Contains("Stage"))
        {
            if (!stageBGMAudioSource.isPlaying)
            {
                instructionBGMAudioSource.Stop();
                titleBGMAudioSource.Stop();
                stageBGMAudioSource.Play();
            }
        }
        else if (currentScene.name.Contains("Instruction"))
        {
            if (!instructionBGMAudioSource.isPlaying)
            {
                titleBGMAudioSource.Stop();
                stageBGMAudioSource.Stop();
                instructionBGMAudioSource.Play();
            }
        }
        else
        {
            if (!titleBGMAudioSource.isPlaying)
            {
                instructionBGMAudioSource.Stop();
                stageBGMAudioSource.Stop();
                titleBGMAudioSource.Play();
            }
        }
    }
}
