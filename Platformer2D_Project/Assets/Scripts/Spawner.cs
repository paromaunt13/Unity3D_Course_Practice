using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    private Vector2 _whereToSpawn;
    private float _spawnDelay;
    private float _nextSpawn;

    private void Update()
    {
        if (Time.time > _nextSpawn)
        {
            _spawnDelay = Random.Range(1, 2f);
            _nextSpawn = Time.time + _spawnDelay;
            _whereToSpawn = new Vector2(transform.position.x, transform.position.y);
            GameObject projectile = Instantiate(_projectile, _whereToSpawn, Quaternion.identity);
            Destroy(projectile, 2f);
        }
    }
}
