using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (!other.gameObject.GetComponent<PlayerItem>().CheckPlayerItemExists())
                {
                    other.gameObject.GetComponent<PlayerItem>().RandomizeItem();
                }
                break;
        }
    }
}
