using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PawnChanger : MonoBehaviour
{
    [Header("Units prefabs")]
    [SerializeField] private Unit _whiteRookPrefab;
    [SerializeField] private Unit _whiteKnightPrefab;
    [SerializeField] private Unit _whiteBishopPrefab;
    [SerializeField] private Unit _whiteQueenPrefab;

    [SerializeField] private Unit _blackRookPrefab;
    [SerializeField] private Unit _blackKnightPrefab;
    [SerializeField] private Unit _blackBishopPrefab;
    [SerializeField] private Unit _blackQueenPrefab;

    [Header("Buttons and Canvas")]
    [SerializeField] private Canvas _canvas;

    [SerializeField] private Button _rookButton;
    [SerializeField] private Button _knightButton;
    [SerializeField] private Button _bishopButton;
    [SerializeField] private Button _queenButton;

    private Battlefield _battlefield;

    private Vector2Int _place;
    private Enums.Team _team;
    private Unit _currentUnit;
    private Unit _newUnit;

    [Inject]
    public void Construct(Battlefield battlefield)
    {
        _battlefield = battlefield;
    }

    private void Start()
    {
        _rookButton.onClick.AddListener(() => SelectUnit(Enums.UnitType.Rook));
        _knightButton.onClick.AddListener(() => SelectUnit(Enums.UnitType.Knight));
        _bishopButton.onClick.AddListener(() => SelectUnit(Enums.UnitType.Bishop));
        _queenButton.onClick.AddListener(() => SelectUnit(Enums.UnitType.Queen));
    }

    public void StartPromotion(Vector2Int place, Enums.Team team, Unit unit)
    {
        _place = place;
        _team = team;
        _currentUnit = unit;
        _canvas.gameObject.SetActive(true);
    }

    private void SelectUnit(Enums.UnitType type) 
    {
        Unit prefab = GetPrefabByType(type, _team);

        _newUnit = Instantiate(prefab, _currentUnit.transform.position, UnityEngine.Quaternion.identity);

        Cell cell = _battlefield.GetCellAt(_place);

        cell.SetUnit(_newUnit);
        _newUnit.cell = cell;

        Destroy(_currentUnit.gameObject);

        _canvas.gameObject.SetActive(false);
    }

    private Unit GetPrefabByType(Enums.UnitType type, Enums.Team team)
    {
        if (team == Enums.Team.White)
        {
            return type switch
            {
                Enums.UnitType.Rook => _whiteRookPrefab,
                Enums.UnitType.Knight => _whiteKnightPrefab,
                Enums.UnitType.Bishop => _whiteBishopPrefab,
                Enums.UnitType.Queen => _whiteQueenPrefab,
                _ => null
            };
        }
        else
        {
            return type switch
            {
                Enums.UnitType.Rook => _blackRookPrefab,
                Enums.UnitType.Knight => _blackKnightPrefab,
                Enums.UnitType.Bishop => _blackBishopPrefab,
                Enums.UnitType.Queen => _blackQueenPrefab,
                _ => null
            };
        }

    }

}
