using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Collect : IState
{

    private CharacterStateMachine _character;
    private NavMeshAgent _agent;
    private GameObject _targetItem;

    public int collectCounter = 0;

    [Inject]
    public Collect(CharacterStateMachine character, NavMeshAgent agent)
    {
        _character = character;
        _agent = agent;
    }

    public void Enter()
    {
        _targetItem = _character.targetItem;
        if (_targetItem == null) 
        {
            _character.SwitchState(_character.searchState);
            return;
        }

        _agent.isStopped = false;
        _agent.SetDestination(_targetItem.transform.position);
    }

    public void UpdateState()
    {
        if (collectCounter == 5) 
        {
            _character.SwitchState(_character.returnState);
            return;
        }

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            CollectItem();

            _character.SwitchState(_character.searchState);
        }
    }

    public void Exit()
    {
        _agent.isStopped = true;
    }

    private void CollectItem() 
    {
        if (_targetItem != null) 
        {
            GameObject.Destroy(_targetItem);
            collectCounter++;
        }
        Debug.Log("Item collected!");
    }
}
