using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string Name;
    public float Cooldown;
    public Sprite Icon;
    public SoundData SoundData;
    public GameObject BuffVFX;
    public abstract void OnAbilityActivated();
}
