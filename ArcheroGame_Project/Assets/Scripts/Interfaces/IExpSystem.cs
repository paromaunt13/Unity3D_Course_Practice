using System;

public interface IExpSystem
{
    int LevelExp { get; }
    int MaxLevel { get; }
    void LevelUp();
    public static Action OnLevelUp;
}
