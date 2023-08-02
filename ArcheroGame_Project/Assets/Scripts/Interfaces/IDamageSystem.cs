public interface IDamageSystem 
{
    int Damage { get; }
    void TakeDamage(int damage);
    void Die();
}
