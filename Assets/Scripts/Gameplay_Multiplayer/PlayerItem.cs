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
    [SerializeField] float playerAttackItemOffsetY = 5f;
    [SerializeField] GameObject targetPlayer;
    [SerializeField] GameObject playerItem, attackItem, defenceItem, abnormalItem;
    [SerializeField] RawImage attackItemImage, defenceItemImage;
    [SerializeField] Image abnormalItemImage;
    [SerializeField] Text playerItemText;

    PlayerStatusMultiplay playerStatusMultiplay;
    bool canUseItem;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        playerItem = null;
        playerStatusMultiplay = GetComponent<PlayerStatusMultiplay>();
        canUseItem = true;
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
            abnormalItemImage.enabled = false;
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
                    break;
                case "Shield":
                    defenceItemImage.enabled = true;
                    attackItemImage.enabled = false;
                    abnormalItemImage.enabled = false;
                    break;
                case "Timer":
                    abnormalItemImage.enabled = true;
                    attackItemImage.enabled = false;
                    defenceItemImage.enabled = false;
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
        Debug.Log("Enum.GetNames(typeof(MultiplayerItems.ItemCategories)).Length: " + Enum.GetNames(typeof(MultiplayerItems.ItemCategories)).Length);
        int randomizedItemCategoryIndex = Random.Range(0, Enum.GetNames(typeof(MultiplayerItems.ItemCategories)).Length);
        Debug.Log("randomizedItemCategoryIndex: "+ randomizedItemCategoryIndex);
        if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.AttackItem)
        {
            playerItem = attackItem;
        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.DefenseItem)
        {
            playerItem = defenceItem;
        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.AbnormalConditionItem)
        {
            playerItem = abnormalItem;
        }
    }

    public void UseItem()
    {
        switch (playerItem.tag)
        {
            case "Missile":
                GameObject playerAttackItem = Instantiate(attackItem);
                playerAttackItem.transform.position = gameObject.transform.position + new Vector3(0f, playerAttackItemOffsetY, 0f);
                playerAttackItem.GetComponent<Missile>().SetTargetPlayer(targetPlayer);
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
        }
        playerItem = null;
    }

    public void EnableUseItem()
    {
        canUseItem = true;
    }
    public void DisableUseItem()
    {
        canUseItem = false;
    }
}
