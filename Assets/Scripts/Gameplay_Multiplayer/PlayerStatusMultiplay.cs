using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusMultiplay : MonoBehaviour
{
    [SerializeField] int playerID = 1;
    [SerializeField] float playerMaxBoost = 100f, boostFactor = 10f;
    [SerializeField] float spriteBlinkingMiniDuration = 1f;
    [SerializeField] Slider boostSlider;
    [SerializeField] Image currentBoostFillArea;
    [SerializeField] Image cautionLeftImage, cautionRightImage;
    [SerializeField] Color normalColor;
    [SerializeField] Color explodedColor;
    [SerializeField] Material playerMaterial;
    [SerializeField] ParticleSystem explosionVFX;

    bool isInvicible;
    bool cautionLeftFlag, cautionRightFlag;
    float playerCurrentBoost;
    float spriteBlinkingTimer;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        isInvicible = false;
        playerMaterial.color = Color.white;
        playerCurrentBoost = playerMaxBoost;
        currentBoostFillArea.color = Color.blue;
        cautionLeftImage.enabled = false;
        cautionRightImage.enabled = false;
        cautionLeftFlag = false;
        cautionRightFlag = false;
    }

    private void Update()
    {
        if (cautionLeftFlag)
        {
            EnableCautionImage(cautionLeftImage);
            DisableCautionImage(cautionRightImage);
        }
        else if (cautionRightFlag)
        {
            EnableCautionImage(cautionRightImage);
            DisableCautionImage(cautionLeftImage);
        }
        else
        {
            DisableCautionImage(cautionLeftImage);
            DisableCautionImage(cautionRightImage);
        }
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

    public void RecoverHalfOfBoost()
    {
        playerCurrentBoost += playerMaxBoost / 2f;
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
        if (explosionVFX == null) { return; }
        explosionVFX.Play();
    }

    public int GetPlayerID()
    {
        return playerID;
    }

    public bool IsInvicible()
    {
        return isInvicible;
    }

    public void AcitivateInvicibleMode()
    {
        isInvicible = true;
    }

    public void DeactivateInvicible()
    {
        isInvicible = false;
    }

    public void SetCautionState(string direction = "Left", bool state = true)
    {
        if (direction == "Left")
        {
            cautionLeftFlag = state;
        }
        else if (direction == "Right")
        {
            cautionRightFlag = state;
        }
        else
        {
            cautionLeftFlag = state;
            cautionRightFlag = state;
        }
        spriteBlinkingTimer = 0f;
    }

    private void EnableCautionImage(Image cautionImage)
    {
        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (cautionImage.enabled == true)
            {
                cautionImage.enabled = false;
            }
            else
            {
                cautionImage.enabled = true;
            }
        }
    }

    private void DisableCautionImage(Image cautionImage)
    {
        cautionImage.enabled = false;
    }
}
