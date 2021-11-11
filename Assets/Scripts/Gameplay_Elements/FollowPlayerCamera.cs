using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraDistanceY = 10.0f;
    [SerializeField] float cameraDistanceZ = 20.0f;

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraDistanceY, playerTransform.position.z - cameraDistanceZ);
    }
}
