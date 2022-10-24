using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawnerManager : MonoBehaviour
{
    private Transform _player;

    private int _spawnRandomEnemyCount;
    private int _spawnRandomEnemyIndex;

    private float _angleStep;

    private string _selectedEnemyTag;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        SpawnRandomEnemy();
        InvokeRepeating("SpawnRandomEnemy", 5f, 5f);
    }

    public void SpawnRandomEnemy()
    {
        _spawnRandomEnemyCount = Random.Range(5, 15);
        _angleStep = 360 / _spawnRandomEnemyCount;
        for (int i = 0; i < _spawnRandomEnemyCount; i++)
        {
            _spawnRandomEnemyIndex = Random.Range(0, 2);
            switch (_spawnRandomEnemyIndex)
            {
                case 0:
                    _selectedEnemyTag = "BlueEnemy";
                    break;
                case 1:
                    _selectedEnemyTag = "BrownEnemy";
                    break;
            }
            GameObject instantiateObject = ObjectPoolManager.Instance.GetPoolObject(_selectedEnemyTag, _player.position + new Vector3(Random.Range(8, 20), 0f, 0f));
            instantiateObject.transform.RotateAround(_player.position, new Vector3(0, 0, 1), _angleStep * i);
            instantiateObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
