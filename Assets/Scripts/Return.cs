using UnityEngine.AI;
using Zenject;


public class Return : IState
{
    private CharacterStateMachine _character;
    private NavMeshAgent _agent;

    [Inject]
    public Return(CharacterStateMachine character, NavMeshAgent agent)
    {
        _character = character;
        _agent = agent;
    }

    public void Enter()
    {
        _agent.isStopped = false;

        _agent.SetDestination(_character.startPosition);
    }

    public void UpdateState()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _character.SwitchState(_character.idleState);
        }
    }

    public void Exit()
    {
        _agent.isStopped = true;
    }
}
