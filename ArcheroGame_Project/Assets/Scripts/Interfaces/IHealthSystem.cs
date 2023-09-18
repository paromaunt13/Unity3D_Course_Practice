using System;

public interface IHealthSystem
{   
    float BaseMaxHealth { get; }
    float CurrentHealth { get; }
    void TakeDamage(float damage);
    void Die();
    public event Action<float, float, bool> OnHealthChanged;
}
