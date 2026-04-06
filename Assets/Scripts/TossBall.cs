using UnityEngine;
using Zenject;

public class TossBall : MonoBehaviour
{
    [Inject] private Ball _ball;

    [SerializeField] private float _minForce = 5f;
    [SerializeField] private float _maxForce = 100f;
    [SerializeField] private float _chargeSpeed = 30f;

    private float _currentForce;
    private bool _isCharging = false;

    [SerializeField] private float _tossCD = 3f;

    private bool _canToss = true;


    public void StartCharging()
    {
        _isCharging = true;
        _currentForce = _minForce;
    }

    public void ChargingTick()
    {
        if (!_isCharging) return;

        _currentForce += _chargeSpeed * Time.deltaTime;
        _currentForce = Mathf.Clamp(_currentForce, _minForce, _maxForce);
    }

    public void Release()
    {
        if (!_isCharging || !_canToss) return;

        Throw();
        _isCharging = false;
    }

    private void Throw()
    {
        _ball.Toss(_currentForce);
        _canToss = false;

        Invoke(nameof(EnableThrow), _tossCD);
    }

    private void EnableThrow()
    {
        _canToss = true;
    }
}
