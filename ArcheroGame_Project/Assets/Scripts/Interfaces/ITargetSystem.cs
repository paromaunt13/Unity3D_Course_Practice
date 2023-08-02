using UnityEngine;

public interface ITargetSystem
{
    float DetectionRange { get; }
    Transform NearestTarget { get; }
}
