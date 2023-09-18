using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBossProjectile : Projectile
{
    [SerializeField] private GameObject _poisonVFX;
    [SerializeField] private float _poisonDuration = 3f;
    [SerializeField] private int _tickCount = 3;

    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealthSystem>(out var player))
        {
            player.TakeOverTimeDamage(_damage, _poisonDuration, _tickCount);
            var vfxSpawnPoint = player.GetComponentInParent<PlayerData>().VFXDisplayPoint;
            if (vfxSpawnPoint != null) 
            {
                var poisonVFX = Instantiate(_poisonVFX, vfxSpawnPoint);
                Destroy(poisonVFX, _poisonDuration);
            }
            Destroy(gameObject);
        }       
    }

    public override void SetDamage(float damage)
    {
        _damage = damage;
    }
}
