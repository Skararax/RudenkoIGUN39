using System.Collections.Generic;
using UnityEngine;

public class BishopRules
{
    public (List<Vector2Int> moves, List<Vector2Int> attacks) GetPossibleMoves(Unit unit, Dictionary<Vector2Int, Cell> cells)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        List<Vector2Int> attacks = new List<Vector2Int>();

        Vector2Int from = unit.cell.gridPosition;

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1)
        };

        //STANDART STEPS
        foreach (var dir in directions)
        {
            for (int step = 1; step < 8; step++)
            {
                Vector2Int next = new Vector2Int(from.x + dir.x * step, from.y + dir.y * step);

                if (!cells.ContainsKey(next))
                    break;

                Cell targetCell = cells[next];

                if (targetCell.currentUnit == null)
                {
                    moves.Add(next);
                }
                else
                {
                    if (targetCell.currentUnit.team != unit.team)
                        attacks.Add(next);

                    break;
                }
            }
        }

        return (moves, attacks);
    }
}
