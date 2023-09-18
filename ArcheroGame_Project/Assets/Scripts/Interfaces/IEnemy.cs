using UnityEngine;

public interface IEnemy : IHealthSystem, IAttackSystem, ITargetSystem
{
    StatsData StatsData { get; }
}
