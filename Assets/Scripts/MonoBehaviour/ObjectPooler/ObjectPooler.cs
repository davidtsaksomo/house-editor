using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
    }

    // Singleton
    public static ObjectPooler instance;
    Dictionary<string, Queue<GameObject>> objectPools;

    [SerializeField]
    List<Pool> pools = new List<Pool>();
    // Map pool with its name in dictionary for easy access
    Dictionary<string, Pool> poolsDictionary;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        objectPools = new Dictionary<string, Queue<GameObject>>();
        poolsDictionary = new Dictionary<string, Pool>();
        foreach (Pool pool in pools)
        {
            objectPools.Add(pool.name, new Queue<GameObject>());
            poolsDictionary.Add(pool.name, pool);
        }
    }

    public GameObject SpawnFromPool(string objectName, Vector3 position, Quaternion rotation)
    {
        return SpawnFromPool(objectName, position, rotation, null);
    }

    public GameObject SpawnFromPool(string objectName, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!objectPools.ContainsKey(objectName))
        {
            Debug.LogWarning("No pool with name " + objectName);
            return null;
        }

        if (objectPools[objectName].Count == 0)
        {
            GameObject instantiantedObject = Instantiate(poolsDictionary[objectName].prefab, position, rotation);
            if (parent)
            {
                instantiantedObject.transform.transform.SetParent(parent, true);
            }
            return instantiantedObject;
        }

        GameObject objectToSpawn = objectPools[objectName].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        if (parent)
        {
            Vector3 scale = objectToSpawn.transform.localScale;
            objectToSpawn.transform.SetParent(parent, true);
            objectToSpawn.transform.localScale = scale;
        }

        return objectToSpawn;
    }

    public bool DespawnToPool(string objectName, GameObject objectToDespawn)
    {
        if (!objectPools.ContainsKey(objectName))
        {
            Debug.LogWarning("No pool with name " + objectName);
            return false;
        }

        objectPools[objectName].Enqueue(objectToDespawn);

        objectToDespawn.SetActive(false);

        return true;
    }
}
