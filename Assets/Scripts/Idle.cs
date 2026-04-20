using UnityEngine;
using Zenject;

public class Idle : IState
{
    private CharacterStateMachine _character;
    private float _timer = 0f;

    [Inject]
    public Idle(CharacterStateMachine character) 
    { 
        _character = character;
    }

    public void Enter() 
    {
        _timer = 0f;
    }

    public void UpdateState() 
    {
        _timer += Time.deltaTime;

        if (_timer > 5f) 
        {
            _character.SwitchState(_character.searchState);
        }
    }

    public void Exit() 
    {
        Debug.Log("Exit Idle is worck!");
    }
}
