using System;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _startPos;
    private Quaternion _startRot;
    private bool _isFallen = false;  

    public event Action<Pin> OnPinFallen;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startPos = transform.position;
        _startRot = transform.rotation;
    }

    private void Update()  
    {
        if (!_isFallen)
        {
            CheckFallen();
        }
    }

    public void ResetPin()
    {
        _isFallen = false;  
        _rb.isKinematic = true;
        transform.position = _startPos;
        transform.rotation = _startRot;
        _rb.isKinematic = false;
    }

    private void CheckFallen()
    {
        if (Vector3.Dot(transform.up, Vector3.up) < 0.7f)
        {
            _isFallen = true;
            OnPinFallen?.Invoke(this);
        }
    }
}