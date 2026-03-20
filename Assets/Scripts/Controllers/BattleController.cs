using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class BattleController : ITickable, IInitializable, IDisposable
{
    [SerializeField] private InputActionAsset _inputActionsAsset;

    private Camera _camera;
    private InputAction _selectAction;
    private IGameplayCommand _currentCommand;
    private Battlefield _battlefield;
    private ChessValidator _validator;
    private PawnChanger _pawnChanger;

    public Enums.Team CurrentTurn { get; private set; } = Enums.Team.White;
    public Unit SelectedUnit { get; private set; }
    public IGameplayCommand CurrentCommand => _currentCommand;

  
    public BattleController(InputActionAsset inputActions, Camera camera, Battlefield battlefield, ChessValidator chessValidator, PawnChanger pawnChanger) 
    { 
        _inputActionsAsset = inputActions;
        _camera = camera;
        _battlefield = battlefield;
        _validator = chessValidator;
        _pawnChanger = pawnChanger;
    }

    public void Initialize()
    {
        if (_camera == null) 
        {
            Debug.LogWarning("Camera not injected!");
            return;
        }

        _currentCommand = new SelectUnitCommand(this, _battlefield, _validator);

        SetupInput();
    }

    public void Tick()
    {
    }

    public void Dispose()
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
    }
     
    public void SetCommand(IGameplayCommand newCommand)
    {
        _currentCommand = newCommand;
    }

    public void PromotePawn(Unit unit, Cell cell) 
    {
        Vector2Int pos = cell.gridPosition;
        Enums.Team team = unit.team;

        _pawnChanger.StartPromotion(pos, team, unit);
    }
}
