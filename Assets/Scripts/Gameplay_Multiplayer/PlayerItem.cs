using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] int playerID = 1;
    [SerializeField] float playerInvisibleTime = 3f;
    [SerializeField] float playerAttackItemOffsetX = 25f;
    [SerializeField] float playerDropBackItemOffsetX = -5f;
    [SerializeField] float waitToTakeItemSeconds = 3f;
    [SerializeField] GameObject targetPlayer;
    [SerializeField] GameObject playerItem, attackItem, defenceItem, abnormalItem, boostCanItem, dropBackItem;
    [SerializeField] RawImage attackItemImage, defenceItemImage, boostCanItemImage, dropBackItemImage;
    [SerializeField] Image abnormalItemImage;
    [SerializeField] Text playerItemText;
    [SerializeField] Text playerTargetLabel, playerTargetText;

    int targetPlayerID;
    PlayerStatusMultiplay playerStatusMultiplay;
    bool canUseItem;
    bool canTakeItem;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        playerItem = null;
        playerStatusMultiplay = GetComponent<PlayerStatusMultiplay>();
        canUseItem = true;
        canTakeItem = true;
        playerTargetLabel.enabled = false;
        playerTargetText.enabled = false;
        targetPlayerID = 0;
    }

    private void Update()
    {
        if (CheckPlayerItemExists() && canUseItem)
        {
            ResponseToUseItems();
        }
    }

    public bool CheckPlayerItemExists()
    {
        if (playerItem == null)
        {
            playerItemText.enabled = false;
            attackItemImage.enabled = false;
            defenceItemImage.enabled = false;
            boostCanItemImage.enabled = false;
            abnormalItemImage.enabled = false;
            dropBackItemImage.enabled = false;
            return false;
        }
        else
        {
            playerItemText.enabled = true;
            switch (playerItem.tag)
            {
                case "Missile":
                    attackItemImage.enabled = true;
                    defenceItemImage.enabled = false;
                    abnormalItemImage.enabled = false;
                    boostCanItemImage.enabled = false;
                    dropBackItemImage.enabled = false;
                    break;
                case "Shield":
                    defenceItemImage.enabled = true;
                    attackItemImage.enabled = false;
                    abnormalItemImage.enabled = false;
                    boostCanItemImage.enabled = false;
                    dropBackItemImage.enabled = false;
                    break;
                case "Timer":
                    abnormalItemImage.enabled = true;
                    attackItemImage.enabled = false;
                    defenceItemImage.enabled = false;
                    boostCanItemImage.enabled = false;
                    dropBackItemImage.enabled = false;
                    break;
                case "Fuel":
                    boostCanItemImage.enabled = true;
                    attackItemImage.enabled = false;
                    defenceItemImage.enabled = false;
                    abnormalItemImage.enabled = false;
                    dropBackItemImage.enabled = false;
                    break;
                case "Stone":
                    dropBackItemImage.enabled = true;
                    attackItemImage.enabled = false;
                    defenceItemImage.enabled = false;
                    abnormalItemImage.enabled = false;
                    boostCanItemImage.enabled = false;
                    break;
            }
            return true;
        }
    }
    private void ResponseToUseItems()
    {
        if(playerItem == null) { return; }
        if (Input.GetButtonDown(playerID+"PUseItem"))
        {
            UseItem();
        }
    }

    public void RandomizeItem()
    {
        RandomItemSettings playerRandomItemSetting = FindObjectOfType<GameManagerMultiplay>().GetPlayerRandomItemSettings(playerID);
        int randomizedItemCategoryIndex = 0;
        int randomAttackItemFactor = playerRandomItemSetting.GetAttackItemRandomRate();
        int randomDefenceItemFactor = playerRandomItemSetting.GetAttackItemRandomRate() + 
            playerRandomItemSetting.GetDefenceItemRandomRate();
        int randomAbnormalItemFactor = playerRandomItemSetting.GetAttackItemRandomRate() + 
            playerRandomItemSetting.GetAttackItemRandomRate() + 
            playerRandomItemSetting.GetAbnormalItemRandomRate();
        int randomBoostCanItemFactor = playerRandomItemSetting.GetAttackItemRandomRate() + 
            playerRandomItemSetting.GetDefenceItemRandomRate() +
            playerRandomItemSetting.GetAbnormalItemRandomRate() + 
            playerRandomItemSetting.GetBoostCanItemRandomRate();
        int randomDropBackItemFactor = playerRandomItemSetting.GetAttackItemRandomRate() +
            playerRandomItemSetting.GetDefenceItemRandomRate() +
            playerRandomItemSetting.GetAbnormalItemRandomRate() +
            playerRandomItemSetting.GetBoostCanItemRandomRate() +
            playerRandomItemSetting.GetDropBackItemRandomRate();
        int randomFactor = Random.Range(0, playerRandomItemSetting.GetTotalRate());
        if(randomFactor >= 0 && randomFactor <= randomAttackItemFactor)
        {
            randomizedItemCategoryIndex = (int)MultiplayerItems.ItemCategories.AttackItem;
        }
        else if (randomFactor > randomAttackItemFactor && randomFactor <= randomDefenceItemFactor)
        {
            randomizedItemCategoryIndex = (int)MultiplayerItems.ItemCategories.DefenseItem;
        }
        else if(randomFactor > randomDefenceItemFactor && randomFactor <= randomAbnormalItemFactor)
        {
            randomizedItemCategoryIndex = (int)MultiplayerItems.ItemCategories.AbnormalConditionItem;
        }
        else if (randomFactor > randomAbnormalItemFactor && randomFactor <= randomBoostCanItemFactor)
        {
            randomizedItemCategoryIndex = (int)MultiplayerItems.ItemCategories.BoostCanItem;
        }
        else if (randomFactor > randomBoostCanItemFactor && randomFactor <= randomDropBackItemFactor)
        {
            randomizedItemCategoryIndex = (int)MultiplayerItems.ItemCategories.DropbackItem;
        }

        if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.AttackItem)
        {
            string targetPlayerName = "";
            string teamBelongsTo = "";
            playerItem = attackItem;
            if(MultiplayPlayerMode.gameMode == "TeamPlay")
            {
                Debug.Log("Team Play CASE");
                if (Array.IndexOf(MultiplayPlayerMode.TeamAPlayerIDs, playerID) >= 0)
                {
                    teamBelongsTo = "Team A";
                }
                else
                {
                    teamBelongsTo = "Team B";
                }
                targetPlayer = FindObjectOfType<GameManagerMultiplay>().GetClosetPlayerRocketTeamPlay(playerID, teamBelongsTo);
                targetPlayerName = FindObjectOfType<GameManagerMultiplay>().GetClosetPlayerNameTeamPlay(playerID, teamBelongsTo);
                targetPlayerID = FindObjectOfType<GameManagerMultiplay>().GetClosetPlayerIDTeamPlay(playerID, teamBelongsTo);
            }
            else
            {
                Debug.Log("Battle Royale CASE");
                targetPlayer = FindObjectOfType<GameManagerMultiplay>().GetClosetPlayerRocketBattleRoyale(playerID);
                targetPlayerName = FindObjectOfType<GameManagerMultiplay>().GetClosetPlayerNameBattleRoyale(playerID);
                targetPlayerID = FindObjectOfType<GameManagerMultiplay>().GetClosetPlayerIDBattleRoyale(playerID);
            }
            if (targetPlayer != null && targetPlayerName != "")
            {
                playerTargetText.text = targetPlayerName;
                playerTargetLabel.enabled = true;
                playerTargetText.enabled = true;
            }

        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.DefenseItem)
        {
            playerItem = defenceItem;
        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.AbnormalConditionItem)
        {
            playerItem = abnormalItem;
        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.BoostCanItem)
        {
            playerItem = boostCanItem;
        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.DropbackItem)
        {
            playerItem = dropBackItem;
        }
    }

    public void UseItem()
    {
        switch (playerItem.tag)
        {
            case "Missile":
                GameObject playerAttackItem = Instantiate(playerItem);
                playerAttackItem.GetComponent<Missile>().SetTargetPlayer(targetPlayer);
                Debug.Log("Target Player ID: "+targetPlayerID);
                Camera targetPlayerCamera = GameObject.Find("FollowPlayer" + targetPlayerID + "Camera").GetComponent<Camera>();
                playerAttackItem.GetComponent<Missile>().SetTargetPlayerCamera(targetPlayerCamera);
                if (playerAttackItem.transform.position.x >= targetPlayer.transform.position.x)
                {
                    playerAttackItem.transform.rotation = Quaternion.Euler(0f, 0f, -180f);
                    targetPlayer.GetComponent<PlayerStatusMultiplay>().SetCautionState("Right", true);
                    playerAttackItem.transform.position = targetPlayer.transform.position + new Vector3(playerAttackItemOffsetX, 0f, 0f);
                    playerAttackItem.GetComponent<Missile>().SetPositionOffSetX(playerAttackItemOffsetX);
                }
                else
                {
                    targetPlayer.GetComponent<PlayerStatusMultiplay>().SetCautionState("Left", true);
                    playerAttackItem.transform.position = targetPlayer.transform.position + new Vector3(-playerAttackItemOffsetX, 0f, 0f);
                    playerAttackItem.GetComponent<Missile>().SetPositionOffSetX(-playerAttackItemOffsetX);
                }
                targetPlayer.GetComponent<PlayerStatusMultiplay>().ProcessCautionCountDown(playerAttackItem.GetComponent<Missile>().GetMissileWaitToStartMovingTime());
                targetPlayer = null;
                break;
            case "Shield":
                GameObject playerDefenItem = Instantiate(playerItem);
                playerDefenItem.transform.SetParent(gameObject.transform);
                playerDefenItem.transform.localPosition = new Vector3(0f, 0f, 0f);
                playerDefenItem.transform.localScale = new Vector3(9f, 9f, 9f);
                playerStatusMultiplay.AcitivateInvicibleMode();
                break;
            case "Timer":
                FindObjectOfType<GameManagerMultiplay>().StopOtherPlayersMovement(playerID);
                FindObjectOfType<GameManagerMultiplay>().StopAllMissilesMovement();
                break;
            case "Fuel":
                playerStatusMultiplay.RecoverHalfOfBoost();
                break;
            case "Stone":
                GameObject playerDropBackItem = Instantiate(playerItem);
                playerDropBackItem.transform.position = gameObject.transform.position + new Vector3(playerDropBackItemOffsetX, 0f, 0f);
                break;
        }
        playerTargetLabel.enabled = false;
        playerTargetText.enabled = false;
        playerItem = null;
        StartCoroutine(PlayerTakeItemDelay());
    }

    private IEnumerator PlayerTakeItemDelay()
    {
        DisableTakeItem();
        playerStatusMultiplay.SetCanntTakeItemState(true);
        yield return new WaitForSeconds(waitToTakeItemSeconds);
        EnableTakeItem();
        playerStatusMultiplay.SetCanntTakeItemState(false);
    }

    public void EnableUseItem()
    {
        canUseItem = true;
    }
    public void DisableUseItem()
    {
        canUseItem = false;
    }

    private void EnableTakeItem()
    {
        canTakeItem = true;
    }

    private void DisableTakeItem()
    {
        canTakeItem = false;
    }

    public bool CanTakeItem()
    {
        return canTakeItem;
    }
}
