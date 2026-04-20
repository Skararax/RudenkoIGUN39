using UnityEngine;
using Zenject;

public class CharacterStateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }
    public Vector3 startPosition;

    [Inject] public Idle idleState;
    [Inject] public Search searchState;
    [Inject] public Collect collectState;
    [Inject] public Return returnState;

    public GameObject targetItem;

    private void Awake()
    {
        startPosition = transform.position;
        SwitchState(idleState);
    }

    private void Update()
    {
        CurrentState.UpdateState();
    }

    public void SwitchState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}
