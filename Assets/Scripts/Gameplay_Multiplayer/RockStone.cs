using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockStone : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Missile") || other.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
