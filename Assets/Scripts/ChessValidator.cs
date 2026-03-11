using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class ChessValidator : MonoBehaviour
{
     private Battlefield _battlefield;

    [Inject]
    private void Construct(Battlefield battlefield)
    {
        _battlefield = battlefield;
    }

    public bool isCheck(Enums.Team team)
    {
        Vector2Int? kingPos = FindKingPosition(team);
        if (kingPos == null) 
        {
            Debug.Log($"King team {team} not found");
            return false;
        }

        return IsCellAttacked(kingPos.Value, team);
    }

    private Vector2Int? FindKingPosition(Enums.Team team)
    {
        Debug.Log($"Всего клеток: {_battlefield.GetAllCells().Count}");

        foreach (var cell in _battlefield.GetAllCells().Values)
        {
            Unit unit = cell.currentUnit;
            if (unit != null && unit.team == team && unit.type == Enums.UnitType.King)
            {
                return cell.gridPosition;
            }

        }
        Debug.Log("King not found");
        return null;
    }

    private bool IsCellAttacked(Vector2Int cellPos, Enums.Team defendingTeam)
    {
        Enums.Team attackingTeam = defendingTeam == Enums.Team.White 
            ? Enums.Team.Black
            : Enums.Team.White;

        foreach (var cell in _battlefield.GetAllCells().Values) 
        {
            Unit enemy = cell.currentUnit;

            if (enemy == null || enemy.team != attackingTeam) continue;

            if (enemy.type == Enums.UnitType.Pawn) 
            { 
                PawnRules rules = new PawnRules();
                var (_, attacks) = rules.GetPossibleMoves(enemy, _battlefield.GetAllCells());
                if (attacks.Contains(cellPos)) return true; 
            }
            if (enemy.type == Enums.UnitType.Bishop)
            {
                BishopRules rules = new BishopRules();
                var (_, attacks) = rules.GetPossibleMoves(enemy, _battlefield.GetAllCells());
                if (attacks.Contains(cellPos)) return true;
            }
            if (enemy.type == Enums.UnitType.Rook)
            {
                RookRules rules = new RookRules();
                var (_, attacks) = rules.GetPossibleMoves(enemy, _battlefield.GetAllCells());
                if (attacks.Contains(cellPos)) return true;
            }
            if (enemy.type == Enums.UnitType.Knight)
            {
                KnightRules rules = new KnightRules();
                var (_, attacks) = rules.GetPossibleMoves(enemy, _battlefield.GetAllCells());
                if (attacks.Contains(cellPos)) return true;
            }
            if (enemy.type == Enums.UnitType.Queen)
            {
                QueenRules rules = new QueenRules();
                var (_, attacks) = rules.GetPossibleMoves(enemy, _battlefield.GetAllCells());
                if (attacks.Contains(cellPos)) return true;
            }
        }

        return false;
    }

    private bool IsMoveLegal(Unit unit, Cell targetCell) 
    { 
        Cell fromCell = unit.cell;
        Unit targetUnit = targetCell.currentUnit;

        fromCell.SetUnit(null);
        targetCell.SetUnit(unit);
        unit.cell = targetCell;

        bool stillInCheck = isCheck(unit.team);

        fromCell.SetUnit(unit);
        targetCell.SetUnit(targetUnit);
        unit.cell = fromCell;

        return !stillInCheck;

    }
}
