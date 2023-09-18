using UnityEngine;

public interface IAttackSystem
{
    float BaseDamage { get; }
    GameObject ProjectilePrefab { get; }
    void Attack();
}
