using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    //[SerializeField] private float _rotationXPerSecond = 90;
    //[SerializeField] private float _rotationYPerSecond = 90;
    [SerializeField] private float _rotationZPerSecond = 90;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = transform.localEulerAngles;
        //rotation.x += Time.deltaTime * _rotationXPerSecond;
        //rotation.y += Time.deltaTime * _rotationYPerSecond;
        rotation.z += Time.deltaTime * _rotationZPerSecond;
        transform.localEulerAngles = rotation;
    }
}
