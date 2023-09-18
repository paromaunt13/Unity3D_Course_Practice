using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealthSystem>(out var player))
        {
            player.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Wall>())
        {
            Destroy(gameObject);
        }
    }

    public override void SetDamage(float damage)
    {
        _damage = damage;
    }
}
