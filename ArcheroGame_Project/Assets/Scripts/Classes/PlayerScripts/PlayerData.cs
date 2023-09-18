using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private Transform _vfxDisplayPoint;
    [SerializeField] private StatsData _statsData;
    [SerializeField] private ExpData _expData;
    [SerializeField] private SoundData _soundData;
    [SerializeField] private Animator _animator;

    public GameObject ProjectilePrefab => _projectilePrefab;
    public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
    public Transform VFXDisplayPoint => _vfxDisplayPoint;
    public StatsData StatsData => _statsData;
    public ExpData ExpData => _expData;
    public SoundData SoundData => _soundData;
    public Animator Animator => _animator;
}
