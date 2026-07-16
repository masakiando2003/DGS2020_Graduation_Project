using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricBoundary : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Fabrics"))
        {
            Destroy(collider.gameObject);
        }
    }
}
