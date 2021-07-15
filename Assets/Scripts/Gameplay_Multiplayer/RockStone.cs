using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockStone : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Missile" || other.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
        }
    }
}
