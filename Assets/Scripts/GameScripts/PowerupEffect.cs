using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject targetObject);
}
