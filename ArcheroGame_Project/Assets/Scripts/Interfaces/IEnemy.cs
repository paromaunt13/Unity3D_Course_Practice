using UnityEngine;

public interface IEnemy : IHealthSystem, IAttackSystem, IDamageSystem, ITargetSystem
{
    StatsData StatsData { get; }
}
