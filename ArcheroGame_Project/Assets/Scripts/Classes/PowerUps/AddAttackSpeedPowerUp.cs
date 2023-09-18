using UnityEngine;

[CreateAssetMenu(fileName = "Add Attack Speed PowerUp", menuName = "PowerUps/Add Attack Speed PowerUp")]
public class AddAttackSpeedPowerUp : PowerUp
{
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _value;   

    public override string Description { get => _description; }
    public override Sprite Icon { get => _icon; }
    public override float Value { get => _value; }

    private void IncreaseAttackSpeed()
    {
        FindObjectOfType<PlayerAttackSystem>().TimeToAttack -= Value;
        Debug.Log("Attack speed increased");
    }

    public override void ApplyPowerUp()
    {
        IncreaseAttackSpeed();
    }  
}
