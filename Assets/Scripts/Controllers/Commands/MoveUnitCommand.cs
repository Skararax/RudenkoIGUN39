using Unity.VisualScripting;
using UnityEngine;

public class MoveUnitCommand : IGameplayCommand 
{
    private BattleController _battleController;
    private Battlefield _battlefield;
    private ChessValidator _chessValidator;

    public MoveUnitCommand(BattleController battleController, Battlefield battlefield, ChessValidator chessValidator)
    {
        _battleController = battleController;
        _battlefield = battlefield;
        _chessValidator = chessValidator;
    }

    public void Execute(Cell selectedCell)
    {
        if (selectedCell == null)
        {
            Debug.LogWarning("Cell not found");
            _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield, _chessValidator));
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
            _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield, _chessValidator));
            _battlefield.ClearHighlights();
            _battleController.SelectedUnit.UnitHighlight(false);
            return;
        }

        Unit movingUnit = _battleController.SelectedUnit;
        Cell fromCell = movingUnit.cell;
        Cell toCell = selectedCell;
        Unit targetUnit = toCell.currentUnit;

        fromCell.SetUnit(null);

        toCell.SetUnit(movingUnit);
        movingUnit.cell = toCell;

        bool stillInCheck = _chessValidator.isCheck(movingUnit.team);

        fromCell.SetUnit(movingUnit);
        toCell.SetUnit(targetUnit);
        movingUnit.cell = fromCell;

        if (stillInCheck)
        {
            Debug.Log($"Step not possible! King {movingUnit.team} stell check!");
            _battlefield.ClearHighlights();
            _battleController.SelectedUnit.UnitHighlight(false);
            _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield, _chessValidator));
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

        _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield, _chessValidator));
    }
}
