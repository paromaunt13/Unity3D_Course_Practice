using System;

public interface IExpSystem
{
    int CurrentExp { get; set; }
    int LevelExp { get; }
    int CurrentLevel { get; set; }
    int MaxLevel { get; }
    void LevelUp();
    public static Action OnLevelUp;
}
