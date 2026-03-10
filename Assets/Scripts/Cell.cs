using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Unit currentUnit;

    private Renderer _cellRenderer;
    private UnityEngine.Color _originalColor;

    private void Start()
    {
        _cellRenderer = GetComponentInChildren<Renderer>();
        if (_cellRenderer != null)
            _originalColor = _cellRenderer.material.color;
    }

    public void SetUnit(Unit unit)
    {
        currentUnit = unit;
    }

    public void SetGridPosition(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        gameObject.name = $"Cell_{gridPosition.x}_{gridPosition.y}";
    }

    public void Highlight(UnityEngine.Color color)
    {
        if (_cellRenderer != null) 
        { 
            _cellRenderer.material.color = color;
        }
    }

    public void ResetHighlight() 
    {
        if (_cellRenderer != null)
        {
            _cellRenderer.material.color = _originalColor;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position, Vector3.one * 0.4f);
    //}
}
