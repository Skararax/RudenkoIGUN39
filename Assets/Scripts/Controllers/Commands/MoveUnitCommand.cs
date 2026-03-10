using Unity.VisualScripting;
using UnityEngine;

public class MoveUnitCommand : IGameplayCommand 
{
    private BattleController _battleController;
    private Battlefield _battlefield;

    public MoveUnitCommand(BattleController battleController, Battlefield battlefield)
    {
        _battleController = battleController;
        _battlefield = battlefield;
    }

    public void Execute(Cell selectedCell)
    {
        if (selectedCell == null)
        {
            Debug.LogWarning("Cell not found");
            _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield));
            return;
        }

        if (_battleController.SelectedUnit == null)
        {
            Debug.Log("No unit selected!");
            return;
        }

        if (!_battlefield.IsCellHighlighted(selectedCell)) 
        {
            Debug.Log("Cannot move there!");
            _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield));
            _battlefield.ClearHighlights();
            _battleController.SelectedUnit.UnitHighlight(false);
            return;
        }

        if (selectedCell.currentUnit != null) 
        {
            Debug.Log($"Attacking enemy unit at {selectedCell.gridPosition}");

            _battlefield.DestroyUnit(selectedCell.currentUnit);
        }

        Cell oldCell = _battleController.SelectedUnit.cell;

        Vector3 targetPos = selectedCell.transform.position + Vector3.up * 0.2f;
        _battleController.SelectedUnit.transform.position = targetPos;
        
        _battleController.SelectedUnit.cell = selectedCell;
        selectedCell.currentUnit = _battleController.SelectedUnit;

        if (oldCell != null) oldCell.SetUnit(null);

        _battleController.SelectedUnit.UnitHighlight(false);
        _battleController.SwitchTurn();
        _battlefield.ClearHighlights();

        _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield));
    }
}
