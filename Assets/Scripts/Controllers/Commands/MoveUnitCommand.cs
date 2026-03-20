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
            _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield, _chessValidator));
            return;
        }

        if (_battleController.SelectedUnit == null)
        {
            return;
        }

        if (!_battlefield.IsCellHighlighted(selectedCell)) 
        {
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
            _battlefield.ClearHighlights();
            _battleController.SelectedUnit.UnitHighlight(false);
            _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield, _chessValidator));
            return;
        }
        if (selectedCell.currentUnit != null) 
        {
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

        if (movingUnit.type == Enums.UnitType.Pawn)
        {
            PawnRules pawnRules = new PawnRules();
            if (pawnRules.CanPromote(movingUnit)) 
            {
                _battleController.PromotePawn(movingUnit, movingUnit.cell);
            }
        }

        if (_chessValidator.IsCheckmate(_battleController.CurrentTurn)) 
        {
            Debug.Log($"Checkmate! {_battleController.CurrentTurn} defeat!");
            //add Logic
        }

        _battleController.SetCommand(new SelectUnitCommand(_battleController, _battlefield, _chessValidator));
    }
}
