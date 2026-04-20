using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private CharacterStateMachine _stateMachine;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private int oldValue;

    private void Start()
    {
        oldValue = _stateMachine.collectState.collectCounter;
    }

    private void Update()
    {
        if (oldValue != _stateMachine.collectState.collectCounter) 
        {
            UpdateCounter();
        }
    }

    private void UpdateCounter() 
    {
        if (_stateMachine == null) 
        {
            Debug.Log("Collect is null");
        }

        _textMeshPro.text = $"Collect: {_stateMachine.collectState.collectCounter}";

        oldValue = _stateMachine.collectState.collectCounter;
    }
}
