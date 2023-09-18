using UnityEngine;

[CreateAssetMenu(fileName = "Heal Ability", menuName = "Abilities/Heal Ability")]
public class HealAbility : Ability
{
    [SerializeField] private int _value;

    private void Heal()
    {
        var targetToHeal = FindObjectOfType<PlayerData>();
        if (targetToHeal != null)
        {
            targetToHeal.GetComponent<PlayerHealthSystem>().Heal(_value);
            var healVFX = Instantiate(BuffVFX, targetToHeal.VFXDisplayPoint);
            Destroy(healVFX, 2.5f);
            AudioManager.Instance.PlaySound(SoundData.HealSound);
        }
    }

    public override void OnAbilityActivated()
    {
        Heal();
    } 
}
