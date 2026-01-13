using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 _rotate;

    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.angularVelocity = _rotate * Time.deltaTime;
    }
}
