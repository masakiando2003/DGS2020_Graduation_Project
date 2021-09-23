using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    [SerializeField] private float _anglesPerSecond = 90;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = transform.localEulerAngles;
        rotation.z += Time.deltaTime * _anglesPerSecond;
        transform.localEulerAngles = rotation;
    }
}
