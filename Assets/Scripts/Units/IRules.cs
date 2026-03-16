using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRules 
{
    (List<Vector2Int> moves, List<Vector2Int> attacks) 
    GetPossibleMoves(Unit unit, Dictionary<Vector2Int, Cell> cells);
}
