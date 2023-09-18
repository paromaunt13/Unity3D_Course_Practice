using UnityEngine;

[CreateAssetMenu(fileName = "Add Max Health PowerUp", menuName = "PowerUps/Add Max Health Power Up")]
public class AddMaxHealthPowerUp : PowerUp
{
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _value;

    public override string Description { get => _description; }
    public override Sprite Icon { get => _icon; }
    public override float Value { get => _value; }

    private void AddMaxHealth()
    {
        var targetToAddHealth = FindObjectOfType<PlayerHealthSystem>();
        if (targetToAddHealth != null)
        {
            targetToAddHealth.AddMaxHealth(Value);
            Debug.Log("Health increased");
        }
    }

    public override void ApplyPowerUp()
    {
        AddMaxHealth();
    }   
}
