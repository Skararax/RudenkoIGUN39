using UnityEngine;
using System.Collections;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [Inject] private Transform _tossPoint;
    private Rigidbody _rigidbody;
    private Coroutine _resetCoroutine;

    private bool _enteredResetZone = false;
    public bool EnteredResetZone => _enteredResetZone;

    public event System.Action OnBallTossed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true; // стартовая позиция
        transform.position = _tossPoint.position;
    }

    public void ResetBall()
    {
        if (_tossPoint == null)
        {
            Debug.LogError("Toss point not assigned!");
            return;
        }

        _rigidbody.isKinematic = false;

        transform.position = _tossPoint.position;
        transform.rotation = Quaternion.identity;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _rigidbody.isKinematic = true;
        _enteredResetZone = false;
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(10f);

        ResetBall();
        _resetCoroutine = null;
    }

    public void Toss(float force)
    {
        if (_resetCoroutine != null)
            StopCoroutine(_resetCoroutine);

        _rigidbody.isKinematic = false;         
        Vector3 direction = Vector3.left;
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);

        OnBallTossed?.Invoke();
        _resetCoroutine = StartCoroutine(WaitAndReset());
    }

    public bool IsStopped()
    {
        if (_resetCoroutine != null) return false;

        return _rigidbody.velocity.magnitude < 0.3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reset"))
        {
            Debug.Log("Ball entered reset zone");
            _enteredResetZone = true;
        }
    }
}