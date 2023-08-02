using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackSystem
{
    GameObject ProjectilePrefab { get; }
    void Attack();
}
