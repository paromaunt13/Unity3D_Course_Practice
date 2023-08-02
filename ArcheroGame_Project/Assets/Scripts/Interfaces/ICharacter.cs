using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter : IHealthSystem, IDamageSystem, IAttackSystem, IExpSystem
{
    StatsData StatsData { get; }
    ExpData ExpData { get; }
    //List<Ability> Abilities { get; }
}
