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
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] GameObject playerItem;
    [SerializeField] Image playerItemImage;
    [SerializeField] Text playerItemText;

    PlayerStatusMultiplay playerStatusMultiplay;

    private void Start()
    {
        playerStatusMultiplay = GetComponent<PlayerStatusMultiplay>();
    }

    private void Update()
    {
        if (CheckPlayerItemExists())
        {
            ResponseToUseItems();
        }
    }

    private bool CheckPlayerItemExists()
    {
        if (playerItem == null)
        {
            playerItemText.enabled = false;
            playerItemImage.color = new Color(playerItemImage.color.r, playerItemImage.color.g, playerItemImage.color.b, 0f);
            return false;
        }
        else
        {
            playerItemText.enabled = true;
            playerItemImage.color = new Color(playerItemImage.color.r, playerItemImage.color.g, playerItemImage.color.b, 1f);
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
        /*
        int randomizedItemCategoryIndex = Random.Range(0, Enum.GetNames(typeof(MultiplayerItems.ItemCategories)).Length);
        if(randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.AttackItem)
        {

        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.DefenseItem)
        {

        }
        else if (randomizedItemCategoryIndex == (int)MultiplayerItems.ItemCategories.AbnormalConditionItem)
        {

        }
        */
    }

    public void UseItem()
    {
        switch (playerItem.tag)
        {
            case "Shield":
                GameObject playerShield = Instantiate(playerItem);
                playerShield.transform.SetParent(gameObject.transform);
                playerShield.transform.localPosition = new Vector3(0f, 0f, 0f);
                playerShield.transform.localScale = new Vector3(9f, 9f, 9f);
                playerStatusMultiplay.AcitivateInvicibleMode();
                break;
        }
        playerItem = null;
    }
}
