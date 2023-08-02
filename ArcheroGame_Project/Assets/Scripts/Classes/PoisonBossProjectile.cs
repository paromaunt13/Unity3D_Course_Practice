using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBossProjectile : MonoBehaviour
{
    private int _damage;
    private float _duration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            player.TakeOverTimeDamage(_damage, _duration);
        }
        Destroy(gameObject);
    }
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
