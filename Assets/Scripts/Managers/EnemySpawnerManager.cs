using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawnerManager : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", 5f,5f);
    }

    public void SpawnRandomEnemy()
    {
        int spawnRandomEnemyCount = Random.Range(5, 15);
        float angleStep = 360 / spawnRandomEnemyCount;
        for (int i = 0; i < spawnRandomEnemyCount; i++)
        {
            GameObject instantiateObject = ObjectPoolManager.Instance.GetPoolObject("BlueEnemy", _player.position + new Vector3(Random.Range(8, 20), 0f, 0f));
            instantiateObject.transform.RotateAround(_player.position, new Vector3(0, 0, 1), angleStep * i);
            instantiateObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
