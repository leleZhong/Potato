using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target;

    public float _distance = 10.0f;
    public float _height = 5.0f;
    public float _xAxis;
    public float _yAxis;

    float _rotSensitive = 3f;
    float _rotationMin = -80f;
    float _rotationMax = 80f;

    void LateUpdate()
    {
        if (_target == null)
            return;
            
        transform.position = _target.position;
        transform.position -= transform.forward * _distance;
        transform.position = new Vector3(transform.position.x, _height, transform.position.z);

        _xAxis += Input.GetAxis("Mouse X") * _rotSensitive;
        _yAxis -= Input.GetAxis("Mouse Y") * _rotSensitive;

        _xAxis = Mathf.Clamp(_xAxis, _rotationMin, _rotationMax);

        transform.localRotation = Quaternion.Euler(_yAxis, _xAxis, 0);
    }
}
