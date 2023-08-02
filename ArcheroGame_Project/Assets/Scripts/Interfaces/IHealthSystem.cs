using System;

public interface IHealthSystem
{   
    float MaxHealth { get; }
    float CurrentHealth { get; }

    public event Action<float, float> OnHealthChanged;
}
