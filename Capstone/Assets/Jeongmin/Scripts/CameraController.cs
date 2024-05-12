using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target;

    public float _xAxis;
    public float _yAxis;

    float _rotSensitive = 3f;
    float _rotationMin = -80f;
    float _rotationMax = 80f;
    float _time = 0.12f;

    Vector3 _offset;
    Vector3 _currentVel;

    void Start()
    {
        _offset = _target.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _target.position - _offset;

        _xAxis += Input.GetAxis("Mouse X") * _rotSensitive;
        _yAxis -= Input.GetAxis("Mouse Y") * _rotSensitive;

        _xAxis = Mathf.Clamp(_xAxis, _rotationMin, _rotationMax);

        transform.localRotation = Quaternion.Euler(_yAxis, _xAxis, 0);
    }
}
