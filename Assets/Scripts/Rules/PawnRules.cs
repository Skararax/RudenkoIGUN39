using System.Collections.Generic;
using UnityEngine;

public class PawnRules : IRules
{
    public (List<Vector2Int> moves, List<Vector2Int> attacks) GetPossibleMoves(Unit unit, Dictionary<Vector2Int, Cell> cells)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        List<Vector2Int> attacks = new List<Vector2Int>();

        Vector2Int from = unit.cell.gridPosition;
        int direction = unit.team == Enums.Team.White ? 1 : -1;

        //STANDART STEPS
        Vector2Int forward = new Vector2Int(from.x, from.y + direction);
        if (cells.ContainsKey(forward) && cells[forward].currentUnit == null)
            moves.Add(forward);

        //FIRST STEP
        bool isFirstStep = (unit.team == Enums.Team.White && from.y == 1) ||
                           (unit.team == Enums.Team.Black && from.y == 6);

        if (isFirstStep)
        {
            Vector2Int twoForward = new Vector2Int(from.x, from.y + direction * 2);
            if (IsCellValid(twoForward, cells) && cells[twoForward].currentUnit == null && cells[forward].currentUnit == null)
            {
                moves.Add(twoForward);
            }
        }

        //ATTACK
        Vector2Int attackLeft = new Vector2Int(from.x - 1, from.y + direction);
        if (IsCellValid(attackLeft, cells) && cells[attackLeft].currentUnit != null && cells[attackLeft].currentUnit.team != unit.team)
        {
            attacks.Add(attackLeft);
            Debug.Log($"Attack left possible at {attackLeft}");
        }

        Vector2Int attackRight = new Vector2Int(from.x + 1, from.y + direction);
        if (IsCellValid(attackRight, cells) && cells[attackRight].currentUnit != null && cells[attackRight].currentUnit.team != unit.team)
        {
            attacks.Add(attackRight);
            Debug.Log($"Attack left possible at {attackRight}");
        }

        return (moves, attacks);
    }

    public bool CanPromote(Unit unit) =>
    unit.team == Enums.Team.White && unit.cell.gridPosition.y == 7;
    

    private bool IsCellValid(Vector2Int pos, Dictionary<Vector2Int, Cell> cells)
    {
        return cells.ContainsKey(pos);
    }

}
