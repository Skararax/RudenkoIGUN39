using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;
using Zenject;
using static Enums;

public class BattleController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActionsAsset;

    private Camera _camera;
    private InputAction _selectAction;
    private IGameplayCommand _currentCommand;

    public Enums.Team CurrentTurn { get; private set; } = Enums.Team.White;
    public Unit SelectedUnit { get; private set; }
    public IGameplayCommand CurrentCommand => _currentCommand;

    [Inject] private Battlefield _battlefield;
    [Inject] private ChessValidator _validator;

    [Inject]
    public void Construct(Camera camera)
    {
        _camera = camera;
    }

    private void Start()
    {
        if (_camera == null) 
        {
            Debug.LogWarning("Camera not injected!");
            return;
        }

        _currentCommand = new SelectUnitCommand(this, _battlefield);

        SetupInput();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool check = _validator.isCheck(Enums.Team.White);
            Debug.Log($"White king check: {check}");
        }
    }

    private void OnDestroy()
    {
        if (_selectAction != null)
        {
            _selectAction.performed -= OnSelectPerformed;
        }
    }

    public void SelectUnit(Unit unit) 
    { 
        if (unit == null) return;
        SelectedUnit = unit;
        Debug.Log($"{unit.name} selected!");
    }

    private void SetupInput()
    {
        if (_inputActionsAsset == null)
        {
            Debug.LogWarning("Input Action asset not assigned");
            return;
        }

        _inputActionsAsset.FindActionMap("Players").Enable();
        _selectAction = _inputActionsAsset.FindAction("Select");

        _selectAction.performed += OnSelectPerformed;
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleSelection();
        }
    }

    private void HandleSelection()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = _camera.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Cell clockedCell = hit.collider.GetComponent<Cell>();
            if (clockedCell != null) 
            {
                if (_currentCommand != null)
                {
                    _currentCommand.Execute(clockedCell);
                }
            }
        }
    }

    public void SwitchTurn()
    {
        CurrentTurn = CurrentTurn == Enums.Team.White
            ? Enums.Team.Black
            : Enums.Team.White;

        Debug.Log($"Step: {CurrentTurn}");
    }

    public void SetCommand(IGameplayCommand newCommand)
    {
        _currentCommand = newCommand;
        Debug.Log($"Command: {newCommand.GetType().Name}");
    }

}
