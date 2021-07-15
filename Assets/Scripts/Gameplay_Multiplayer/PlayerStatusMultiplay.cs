using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusMultiplay : MonoBehaviour
{
    [SerializeField] int playerID = 1;
    [SerializeField] float playerMaxBoost = 100f, boostFactor = 10f;
    [SerializeField] float cautionBlinkingMiniDuration = 1f, cannotTakeItemBlinkingMiniDuration = 1f;
    [SerializeField] Slider boostSlider;
    [SerializeField] Image currentBoostFillArea;
    [SerializeField] Image cautionLeftImage, cautionRightImage;
    [SerializeField] RawImage stopImage, itemBoxImage;
    [SerializeField] Color normalColor;
    [SerializeField] Color explodedColor;
    [SerializeField] Material playerMaterial;
    [SerializeField] ParticleSystem explosionVFX;

    bool isInvicible;
    bool cautionLeftFlag, cautionRightFlag, cannotTakeItemFlag;
    float playerCurrentBoost;
    float cautionBlinkingTimer, cannotTakeItemBlinkingTimer;

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
        stopImage.enabled = false;
        itemBoxImage.enabled = false;
        cautionLeftFlag = false;
        cautionRightFlag = false;
        cannotTakeItemFlag = false;
        cautionBlinkingTimer = 1f;
        cannotTakeItemBlinkingTimer = 1f;
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

        if (cannotTakeItemFlag)
        {
            EnableCannotTakeItemFlagImage(stopImage, itemBoxImage);
        }
        else
        {
            DisableCannotTakeItemFlagImage(stopImage, itemBoxImage);
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
        cautionBlinkingTimer = 0f;
    }

    public void SetCanntTakeItemState(bool state = true)
    {
        cannotTakeItemFlag = state;
    }

    private void EnableCautionImage(Image cautionImage)
    {
        cautionBlinkingTimer += Time.deltaTime;
        if (cautionBlinkingTimer >= cautionBlinkingMiniDuration)
        {
            cautionBlinkingTimer = 0.0f;
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

    private void EnableCannotTakeItemFlagImage(RawImage stopImage, RawImage itemBoxImage)
    {
        cannotTakeItemBlinkingTimer += Time.deltaTime;
        if (cannotTakeItemBlinkingTimer >= cannotTakeItemBlinkingMiniDuration)
        {
            cannotTakeItemBlinkingTimer = 0.0f;
            if (stopImage.enabled == true)
            {
                stopImage.enabled = false;
            }
            else
            {
                stopImage.enabled = true;
            }
            if (itemBoxImage.enabled == true)
            {
                itemBoxImage.enabled = false;
            }
            else
            {
                itemBoxImage.enabled = true;
            }
        }
    }

    private void DisableCannotTakeItemFlagImage(RawImage stopImage, RawImage itemBoxImage)
    {
        stopImage.enabled = false;
        itemBoxImage.enabled = false;
    }
}
