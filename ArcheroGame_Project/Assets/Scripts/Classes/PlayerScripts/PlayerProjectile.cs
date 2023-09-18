using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    private float _damage;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<EnemyHealthSystem>(out var enemy))
    //    {
    //        enemy.TakeDamage(_damage);
    //    }
    //    Destroy(gameObject);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealthSystem>(out var enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }       
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
