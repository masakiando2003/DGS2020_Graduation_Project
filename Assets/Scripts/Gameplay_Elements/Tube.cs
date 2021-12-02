using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] Transform[] tubes;
    [SerializeField] float freeFallDelayTime = 0.5f;

    public void EnableFreeFall()
    {
        StartCoroutine(StartFreeFall());
    }

    private IEnumerator StartFreeFall()
    {
        foreach(Transform tube in tubes)
        {
            Debug.Log("tube: " + tube.gameObject.name);
            yield return new WaitForSeconds(freeFallDelayTime);
            foreach (Rigidbody tubeDetail in tube.GetComponentsInChildren<Rigidbody>())
            {
                Debug.Log("tubeDetail: " + tubeDetail.gameObject.name);
                tubeDetail.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                                        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}
