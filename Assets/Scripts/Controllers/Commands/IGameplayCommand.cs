using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayCommand 
{
    public void Execute(Cell selectedCell);
}
