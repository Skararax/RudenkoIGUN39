using UnityEngine;
using Zenject;

public class GameLoop : IInitializable, ITickable
{
    private GameState _state = GameState.Waiting;
    private bool _isFirstThrow = true;
    private int _currentFramePins = 0;

    [Inject] private TossBall _tossBall;
    [Inject] private PinManager _pinManager;
    [Inject] private Ball _ball;
    [Inject] private ScoreManager _scoreManager;

    public void Initialize()
    {
        Debug.Log("GameLoop started");
        _state = GameState.Waiting;
    }

    public void Tick()
    {
        switch (_state)
        {
            case GameState.Waiting:
                if (Input.GetMouseButtonDown(0))
                {
                    _tossBall.StartCharging();
                    _state = GameState.Charging;
                    Debug.Log("State: Charging");
                }
                break;

            case GameState.Charging:
                _tossBall.ChargingTick();
                if (Input.GetMouseButtonUp(0))
                {
                    _tossBall.Release();
                    _state = GameState.Throwing;
                }
                break;

            case GameState.Throwing:
                if (_ball.EnteredResetZone || _ball.IsStopped())
                {
                    _state = GameState.Counting;
                    Debug.Log($"State: Counting. Fallen: {_pinManager.FallenCount}");
                }
                break;

            case GameState.Counting:
                int fallenPins = _pinManager.FallenCount;

                bool isStrike = false;
                bool isSpare = false;

                if (_isFirstThrow)
                {
                    if (fallenPins == 10)
                    {
                        isStrike = true;
                    }
                    else
                    {
                        _currentFramePins = fallenPins;
                        _isFirstThrow = false;
                    }
                }
                else
                {
                    int total = _currentFramePins + fallenPins;
                    if (total == 10)
                    {
                        isSpare = true;
                    }
                    _isFirstThrow = true;
                    _currentFramePins = 0;
                }

                _scoreManager.ScoreLogic(fallenPins, _isFirstThrow, isStrike, isSpare);
                _state = GameState.Resetting;
                Debug.Log("State: Resetting");
                break;

            case GameState.Resetting:
                if (_isFirstThrow) 
                {
                    _pinManager.ResetPins();
                }
                _ball.ResetBall();
                _state = GameState.Waiting;
                Debug.Log("State: Waiting (ready for next throw)");
                break;
        }
    }
}