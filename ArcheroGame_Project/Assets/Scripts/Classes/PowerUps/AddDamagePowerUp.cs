using UnityEngine;

[CreateAssetMenu(fileName = "Add Damage Power Up", menuName = "PowerUps/Add Damage Power Up")]
public class AddDamagePowerUp : PowerUp
{
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _value;

    public override string Description { get => _description; }
    public override Sprite Icon { get => _icon; }
    public override float Value { get => _value; }

    private void IncreaseDamage()
    {
        FindObjectOfType<PlayerAttackSystem>().CurrentDamage += Value;
        Debug.Log("Damage increased");
    }

    public override void ApplyPowerUp()
    {
        IncreaseDamage();
    }   
}
