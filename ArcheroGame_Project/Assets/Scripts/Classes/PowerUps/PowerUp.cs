using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : ScriptableObject
{
    public abstract string Description { get; }
    public abstract Sprite Icon { get; }
    public abstract float Value { get; }
    public abstract void ApplyPowerUp();
}
