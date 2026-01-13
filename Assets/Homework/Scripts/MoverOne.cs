using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MoverOne : MonoBehaviour
{
    [SerializeField] Vector3 _start;
    [SerializeField] Vector3 _end;
    [SerializeField] float _speed;
    [SerializeField] float _delay;

    Rigidbody _rb;

    private void Start()
    {
        _start = transform.position;

        if (_rb == null) 
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }

        _rb = GetComponent<Rigidbody>();

        StartCoroutine(MoverLoop());
    }

    IEnumerator MoverLoop()
    {
        bool switchTarget = true;

        while (true)
        {

            Vector3 target = switchTarget ? _end : _start;

            while (Vector3.Distance(_rb.position, target) > 0.5f)
            {
                Vector3 direction = (target - _rb.position).normalized;
                _rb.velocity = direction * _speed;
                yield return new WaitForFixedUpdate();
            }

            Debug.Log("SWITCH");
            _rb.velocity = Vector3.zero;

            yield return new WaitForSeconds(_delay);
            switchTarget = !switchTarget;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_start, Vector3.one);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_end, Vector3.one);
    }
}
