using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SelectUnitCommand : IGameplayCommand
{
    private BattleController _battleController;
    private Battlefield _battlefield;

    [Inject]
    public SelectUnitCommand(BattleController battleController, Battlefield battlefield)
    {
        _battleController = battleController;
        _battlefield = battlefield;
    }

    public void Execute(Cell selectedCell)
    {
        Debug.Log($"SelectUnitCommand: clicked cell at {selectedCell.gridPosition}");

        Unit unit = selectedCell.currentUnit;

        if (unit != null)
        {
            if (unit.team != _battleController.CurrentTurn)
            {
                Debug.Log("Сейчас не твой ход!");
                return;
            }

            if (unit.team == _battleController.CurrentTurn)
            {
                Debug.Log($"Find unit: {unit.name}!");

                if (_battleController.SelectedUnit != null)
                {
                    _battleController.SelectedUnit.UnitHighlight(false);
                }

                unit.UnitHighlight(true);
                _battleController.SelectUnit(unit);

                if (unit.type == Enums.UnitType.Pawn)
                {
                    PawnRules pawnRules = new PawnRules(); 
                    var (moves, attacks) = pawnRules.GetPossibleMoves(unit, _battlefield.GetAllCells());
                    _battlefield.HighlightCells(moves, attacks);
                }
                if (unit.type == Enums.UnitType.Rook)
                {
                    RookRules rookRules = new RookRules();
                    var (moves, attacks) = rookRules.GetPossibleMoves(unit, _battlefield.GetAllCells());
                    _battlefield.HighlightCells(moves, attacks);
                }
                if (unit.type == Enums.UnitType.Bishop)
                {
                    BishopRules bishopRules = new BishopRules();
                    var (moves, attacks) = bishopRules.GetPossibleMoves(unit, _battlefield.GetAllCells());
                    _battlefield.HighlightCells(moves, attacks);
                }
                if (unit.type == Enums.UnitType.Knight)
                {
                    KnightRules knightRules = new KnightRules();
                    var (moves, attacks) = knightRules.GetPossibleMoves(unit, _battlefield.GetAllCells());
                    _battlefield.HighlightCells(moves, attacks);
                }
                if (unit.type == Enums.UnitType.Queen)
                {
                    QueenRules queenRules = new QueenRules();
                    var (moves, attacks) = queenRules.GetPossibleMoves(unit, _battlefield.GetAllCells());
                    _battlefield.HighlightCells(moves, attacks);
                }
                if (unit.type == Enums.UnitType.King)
                {
                    KingRules kingRules = new KingRules();
                    var (moves, attacks) = kingRules.GetPossibleMoves(unit, _battlefield.GetAllCells());
                    _battlefield.HighlightCells(moves, attacks);
                }

                _battleController.SetCommand(new MoveUnitCommand(_battleController, _battlefield));
            }
            else
            {
                if (_battleController.SelectedUnit != null)
                {
                    _battleController.SelectedUnit.UnitHighlight(false);
                    _battleController.SelectUnit(null);
                    _battlefield.ClearHighlights();
                }
            }

        }
        else
        {
            Debug.Log("Unit null");

            if (_battleController.SelectedUnit != null)
            {
                _battleController.SelectedUnit.UnitHighlight(false);
                _battleController.SelectUnit(null);
            }
        }
    }
}