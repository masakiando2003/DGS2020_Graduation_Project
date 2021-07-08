using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemBox : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                var player = other.gameObject.GetComponent<PlayerItem>();
                if (!player.CheckPlayerItemExists()
                    && player.CanTakeItem())
                {
                    player.RandomizeItem();
                }
                break;
        }
    }
}
