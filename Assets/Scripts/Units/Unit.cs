using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Enums.Team team;
    public Enums.UnitType type;
    public Cell cell;

    [Header("Render Settings")]
    public Renderer renderer;
    private Color _originalColor;

    private void Awake()
    {
        if (renderer == null)
        {
            Debug.LogWarning("Render is missing");
            return;
        }

        _originalColor = renderer.material.color;
    }

    public void UnitHighlight(bool state) 
    {
        if (state) 
        { 
            renderer.material.color = Color.green;
        }
        if (!state) 
        {
            renderer.material.color = _originalColor;
        }
    }


}
