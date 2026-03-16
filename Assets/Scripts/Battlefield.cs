using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [Header("White Settings")]
    [SerializeField] private Unit _whitePawnPrefab;
    [SerializeField] private Unit _whiteRookPrefab;
    [SerializeField] private Unit _whiteKnightPrefab;
    [SerializeField] private Unit _whiteBishopPrefab;
    [SerializeField] private Unit _whiteQueenPrefab;
    [SerializeField] private Unit _whiteKingPrefab;

    [Header("Black Settings")]
    [SerializeField] private Unit _blackPawnPrefab;
    [SerializeField] private Unit _blackRookPrefab;
    [SerializeField] private Unit _blackKnightPrefab;
    [SerializeField] private Unit _blackBishopPrefab;
    [SerializeField] private Unit _blackQueenPrefab;
    [SerializeField] private Unit _blackKingPrefab;

    [Header("Board")]
    [SerializeField] private Object _chessBoardPrefab;

    [Header("Board Settings")]
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private int _boardSize;
    [SerializeField] private float _distanceXPlus = 0.45f;
    [SerializeField] private float _distanceZPlus = 3f;
    [SerializeField] private float _cellDistanceX = 0;
    [SerializeField] private float _cellDistanceZ = 0;

    private Dictionary<Vector2Int, Cell> _cells = new Dictionary<Vector2Int, Cell>();
    private List<Cell> _highlightedCells = new List<Cell>();

    private void Start()
    {
        GenerateGrid();

        SetupInitialPosition();
    }

    public void GenerateGrid()
    {
        if (_cellPrefab == null)
        {
            Debug.Log("Cell prefab is null!");
        }

        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                Cell newCell = Instantiate(_cellPrefab, new Vector3(_cellDistanceX, 0, _cellDistanceZ), Quaternion.identity);
                newCell.SetGridPosition(new Vector2Int(i,j));
                newCell.transform.SetParent(transform);

                _cellDistanceX += _distanceXPlus;

                _cells[new Vector2Int(i,j)] = newCell;
            }
            _cellDistanceX = 0;
            _cellDistanceZ += _distanceZPlus;
        }
    }

    private void SpawnUnit(Unit prefab,Vector2Int pos, Enums.Team team, Enums.UnitType type, string namePrefix) 
    {

        if (_cells.TryGetValue(pos, out Cell cell)) 
        {
            Unit newUnit = Instantiate(prefab, cell.transform.position + Vector3.up * 0.2f, Quaternion.identity);

            newUnit.team = team;
            newUnit.type = type;
            newUnit.cell = cell;
            newUnit.name = $"{namePrefix} {pos.x}";

            cell.SetUnit(newUnit);
        }
    }

    private void SetupInitialPosition() 
    {
        //Pawns
        for (int x = 0; x < 8; x++) 
        {
            SpawnUnit(_whitePawnPrefab, new Vector2Int(x, 1), Enums.Team.White, Enums.UnitType.Pawn, _whitePawnPrefab.name);
            SpawnUnit(_blackPawnPrefab, new Vector2Int(x, 6), Enums.Team.Black, Enums.UnitType.Pawn, _blackPawnPrefab.name);
        }

        //Rooks
        for (int x = 0; x < 8; x += 7) 
        {
            SpawnUnit(_whiteRookPrefab, new Vector2Int(x, 0), Enums.Team.White, Enums.UnitType.Rook, _whiteRookPrefab.name);
            SpawnUnit(_blackRookPrefab, new Vector2Int(x, 7), Enums.Team.Black, Enums.UnitType.Rook, _blackRookPrefab.name);
        }

        //Knights
        for (int x = 1; x < 8; x += 5) 
        {
            SpawnUnit(_whiteKnightPrefab, new Vector2Int(x, 0), Enums.Team.White, Enums.UnitType.Knight, _whiteKnightPrefab.name);
            SpawnUnit(_blackKnightPrefab, new Vector2Int(x, 7), Enums.Team.Black, Enums.UnitType.Knight, _blackKnightPrefab.name);
        }

        //Bishops
        for (int x = 2; x < 6; x += 3) 
        {
            SpawnUnit(_whiteBishopPrefab, new Vector2Int(x, 0), Enums.Team.White, Enums.UnitType.Bishop, _whiteBishopPrefab.name);
            SpawnUnit(_blackBishopPrefab, new Vector2Int(x, 7), Enums.Team.Black, Enums.UnitType.Bishop, _blackBishopPrefab.name);
        }

        //Queen
        SpawnUnit(_whiteQueenPrefab, new Vector2Int(3, 0), Enums.Team.White, Enums.UnitType.Queen, _whiteQueenPrefab.name);
        SpawnUnit(_blackQueenPrefab, new Vector2Int(3, 7), Enums.Team.Black, Enums.UnitType.Queen, _blackQueenPrefab.name);

        //Kings
        SpawnUnit(_whiteKingPrefab, new Vector2Int(4, 0), Enums.Team.White, Enums.UnitType.King, _whiteKingPrefab.name);
        SpawnUnit(_blackKingPrefab, new Vector2Int(4, 7), Enums.Team.Black, Enums.UnitType.King, _blackKingPrefab.name);
    }

    public void DestroyUnit(Unit unit) 
    {
        if (unit != null)
        {
            if (unit.cell != null)
                unit.cell.SetUnit(null);

            Destroy(unit.gameObject);
        }
    }

    public void HighlightCells(List<Vector2Int> movePositions, List<Vector2Int> attackPositions) 
    {
        ClearHighlights();

        foreach (var pos in movePositions) 
        {
            if (_cells.TryGetValue(pos, out Cell cell)) 
            { 
                cell.Highlight(UnityEngine.Color.yellow);
                _highlightedCells.Add(cell);
            }
        }

        foreach (var pos in attackPositions) 
        {
            if (_cells.TryGetValue(pos, out Cell cell))
            {
                cell.Highlight(UnityEngine.Color.red);
                if (!_highlightedCells.Contains(cell))
                    _highlightedCells.Add(cell);
            }
        }
    }

    public void ClearHighlights() 
    {
        foreach (var cell in _highlightedCells) 
        { 
            cell.ResetHighlight();
        }
        _highlightedCells.Clear();
    }

    public Dictionary<Vector2Int, Cell> GetAllCells() 
    { 
        return _cells;
    }

    public bool IsCellHighlighted(Cell cell) 
    {
        return _highlightedCells.Contains(cell);
    }

    public Unit GetUnitAt(Vector2Int position) 
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(position.x, 0.5f, position.y), 0.3f);

        foreach (var collider in colliders) 
        {
            Unit unit = collider.GetComponent<Unit>();
            if(unit != null) return unit;
        }
        return null;
    }

    public Cell GetCellAt(Vector2Int cell) 
    {
        return _cells[cell];
    }
}
