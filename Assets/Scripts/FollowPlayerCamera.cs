using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraDistance = 10.0f;

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - cameraDistance);
    }
}
