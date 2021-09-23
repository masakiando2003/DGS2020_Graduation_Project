using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public void ActivateFuelObject()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateFuelObject()
    {
        gameObject.SetActive(false);
    }
}
