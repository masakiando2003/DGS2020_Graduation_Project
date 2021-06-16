using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusSolo : MonoBehaviour
{
    [SerializeField] int playerID = 1;
    [SerializeField] int playerMaxLife = 10;
    [SerializeField] float playerMaxBoost = 100f, boostFactor = 10f;
    [SerializeField] Slider boostSlider;
    [SerializeField] Image currentBoostFillArea;
    [SerializeField] Color normalColor;
    [SerializeField] Color explodedColor;
    [SerializeField] Material playerMaterial;
    [SerializeField] ParticleSystem explosionVFX;

    static int currentLife;
    static float playerCurrentBoost;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        playerMaterial.color = Color.white;
        currentLife = playerMaxLife;
        playerCurrentBoost = playerMaxBoost;
        currentBoostFillArea.color = Color.blue;
    }

    public void UpdatePlayerBoostSlider()
    {
        boostSlider.value = GetBoostSliderValue();
    }

    private float GetBoostSliderValue()
    {
        return playerCurrentBoost / playerMaxBoost;
    }

    public void ChangePlayerBoostSliderColor()
    {
        if (GetBoostSliderValue() >= 0.5)
        {
            currentBoostFillArea.color = Color.blue;
        }
        else if (GetBoostSliderValue() >= 0.2)
        {
            currentBoostFillArea.color = Color.yellow;
        }
        else
        {
            currentBoostFillArea.color = Color.red;
        }
    }

    public void ReducePlayerBoost(float speedUpFactor)
    {
        playerCurrentBoost -= Time.deltaTime * boostFactor * speedUpFactor;
    }

    public void RecoverBoost()
    {
        playerCurrentBoost += Time.deltaTime * boostFactor;
        playerCurrentBoost = (playerCurrentBoost > playerMaxBoost) ? playerMaxBoost : playerCurrentBoost;
    }

    public void AddBoost(float amount)
    {
        playerCurrentBoost += amount;
        playerCurrentBoost = (playerCurrentBoost > playerMaxBoost) ? playerMaxBoost : playerCurrentBoost;
    }

    public void ResetBoostToFull()
    {
        playerCurrentBoost = playerMaxBoost;
    }

    public float GetCurrentBoost()
    {
        return playerCurrentBoost;
    }

    public float GetPlayerMaxBoost()
    {
        return playerMaxBoost;
    }

    public void ReducePlayerLife()
    {
        currentLife--;
    }
    public void RecoverPlayerLife(int recoverPlayerLife = 0)
    {
        currentLife += recoverPlayerLife;
        if(currentLife > playerMaxLife)
        {
            currentLife = playerMaxLife;
        }
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public void SetPlayerColorToNormal()
    {
        playerMaterial.color = normalColor;
    }

    public void SetPlayerColorToExploded()
    {
        playerMaterial.color = explodedColor;
    }

    public void PlayExplosionVFX()
    {
        if(explosionVFX == null) { return; }
        explosionVFX.Play();
    }

    public int GetPlayerID()
    {
        return playerID;
    }
}
