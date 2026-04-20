using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Search : IState
{
    public float area = 10f;
    public float findRadius = 6f;

    private CharacterStateMachine _character;
    private NavMeshAgent _agent;

    [Inject]
    public Search(CharacterStateMachine character, NavMeshAgent agent) 
    {
        _character = character;
        _agent = agent;
    }

    public void Enter()
    {
        Debug.Log("Enetr Search is worck!");

        _agent.isStopped = false;
        SetRandomDestination();
    }

    public void UpdateState()
    {
        Debug.Log("SearchState");

        if (DetectCollectbls(out GameObject item)) 
        {
            _character.targetItem = item;
            _character.SwitchState(_character.collectState);
            return;
        }

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance) 
        { 
            SetRandomDestination();
        }
    }

    private void SetRandomDestination() 
    {
        Vector3 randomPoint = Random.insideUnitSphere * area;
        _agent.SetDestination(randomPoint);
    }

    public void Exit()
    {
        Debug.Log("Exit Search is worck!");
        _agent.isStopped = true;
    }

    private bool DetectCollectbls(out GameObject foundItem) 
    {
        Collider[] hits = Physics.OverlapSphere(_agent.transform.position, findRadius);

        foreach (Collider hit in hits) 
        {
            if (hit.TryGetComponent(out ICollectable collectable)) 
            {
                Debug.Log("Find item!");

                foundItem = hit.gameObject;
                return true;
            }
        }
        foundItem = null;
        return false;
    }
}
