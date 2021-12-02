using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricBoundary : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Fabrics"))
        {
            Destroy(collider.gameObject);
        }
    }
}
