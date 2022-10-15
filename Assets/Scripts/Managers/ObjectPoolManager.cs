using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    [SerializeField] private Pool[] poolList = null;

    [System.Serializable]
    class Pool
    {
        public Queue<GameObject> poolObjectList;
        public GameObject poolObjectPrefab;
        public int poolListSize;
    }

    private void Awake()
    {
        for (int i = 0; i < poolList.Length; i++)
        {
            poolList[i].poolObjectList = new Queue<GameObject>();
            for (int j = 0; j < poolList[i].poolListSize; j++)
            {
                var gameObject = FactoryManager.Instance.CreateNewObject(poolList[i].poolObjectPrefab.tag, transform);
                gameObject.SetActive(false);
                poolList[i].poolObjectList.Enqueue(gameObject);
            }
        }
    }

    public GameObject GetPoolObject(string objectTag, Vector3 spawnPosition)
    {
        GameObject nextObject = null;
        switch (objectTag)
        {
            case "Shuriken":
                nextObject = FindNextObject(0, spawnPosition);
                break;
            case "BlueEnemy":
                nextObject = FindNextObject(1, spawnPosition);
                break;
        }
        return nextObject;
    }

    private GameObject FindNextObject(int poolListNumber, Vector3 spawnPosition)
    {
        Debug.Log(poolList[poolListNumber].poolObjectList);
        var gameObject = poolList[poolListNumber].poolObjectList.Dequeue();
        gameObject.transform.position = spawnPosition;
        gameObject.SetActive(true);
        poolList[poolListNumber].poolObjectList.Enqueue(gameObject);
        return gameObject;
    }
}
