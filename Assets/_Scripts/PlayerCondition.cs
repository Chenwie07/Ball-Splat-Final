using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition 
{
    public bool IsMoving { get; set; }

    internal void ResetConditions()
    {
        IsMoving = false; 
    }
}
