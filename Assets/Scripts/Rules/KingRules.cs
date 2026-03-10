using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRules : MonoBehaviour
{
    public (List<Vector2Int> moves, List<Vector2Int> attacks) GetPossibleMoves(Unit unit, Dictionary<Vector2Int, Cell> cells) 
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        List<Vector2Int> attacks = new List<Vector2Int>();

        Vector2Int from = unit.cell.gridPosition;

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1)
        };

        foreach (var dir in directions) 
        {
            Vector2Int next = new Vector2Int(from.x + dir.x, from.y + dir.y);

            if (!cells.ContainsKey(next))
                continue;

            Cell targetCell = cells[next];

            if (cells.ContainsKey(next) && cells[next].currentUnit == null)
                moves.Add(next);
            else 
            {
                if (targetCell.currentUnit.team == unit.team) continue;
                attacks.Add(next);
            }
        }

        return (moves, attacks);
    }
}
