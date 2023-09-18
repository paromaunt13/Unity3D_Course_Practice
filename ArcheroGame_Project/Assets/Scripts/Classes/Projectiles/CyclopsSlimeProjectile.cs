using UnityEngine;

public class CyclopsSlimeProjectile : Projectile
{
    [SerializeField] private GameObject _slowVFX;
    [SerializeField] private float _slowDuration;
    [SerializeField] private float _slowValue;

    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealthSystem>(out var playerHealth))
        {
            playerHealth.TakeDamage(_damage);
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                playerMovement.ApplySlow(_slowValue, _slowDuration);
                var vfxSpawnPoint = playerMovement.GetComponentInParent<PlayerData>().VFXDisplayPoint;
                if (vfxSpawnPoint != null)
                {
                    var slowVFX = Instantiate(_slowVFX, vfxSpawnPoint);
                    Destroy(slowVFX, _slowDuration);
                }
                Destroy(gameObject);
            }
        }      
    }

    public override void SetDamage(float damage)
    {
        _damage = damage;
    }
}
