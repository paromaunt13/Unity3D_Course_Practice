using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _dieVFX;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private Transform _vfxDisplayPoint;
    [SerializeField] private StatsData _statsData;
    [SerializeField] private SoundData _soundData;
    [SerializeField] private Animator _animator;
    

    public StatsData StatsData => _statsData;
    public SoundData SoundData => _soundData;
    public GameObject ProjectilePrefab => _projectilePrefab;
    public GameObject DieVFX => _dieVFX;
    public float BaseDamage => StatsData.Damage;
    public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
    public Transform VFXDisplayPoint => _vfxDisplayPoint;
    public Animator Animator => _animator;
}
