using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioSource titleBGMAudioSource, instructionBGMAudioSource, stageBGMAudioSource;
    [SerializeField] float maxBGMVolumeFactor = 0.2f;

    private static bool created = false;
    static int bgmVolume, seVolume;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            bgmVolume = PlayerPrefs.GetInt("BGMVolume");
        }
        else{
            bgmVolume = 100;
        }
        if (PlayerPrefs.HasKey("SEVolume"))
        {
            seVolume = PlayerPrefs.GetInt("SEVolume");
        }
        else
        {
            seVolume = 100;
        }
        instructionBGMAudioSource.volume = maxBGMVolumeFactor * bgmVolume / 100;
        titleBGMAudioSource.volume = maxBGMVolumeFactor * bgmVolume / 100;
        stageBGMAudioSource.volume = maxBGMVolumeFactor * bgmVolume / 100;
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
        SwitchBGM();
        UpdateBGMVolume();
        UpdateSEVolume();
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

    private void UpdateBGMVolume()
    {
        instructionBGMAudioSource.volume = maxBGMVolumeFactor * bgmVolume / 100;
        titleBGMAudioSource.volume = maxBGMVolumeFactor * bgmVolume / 100;
        stageBGMAudioSource.volume = maxBGMVolumeFactor * bgmVolume / 100;
    }

    private void UpdateSEVolume()
    {

    }

    public static void SetBGMVolume(int newBGMVolume)
    {
        bgmVolume = newBGMVolume;
    }

    public static void SetSEVolume(int newSEVolume)
    {
        seVolume = newSEVolume;
    }
}
