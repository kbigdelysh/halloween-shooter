using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Ref: https://learn.unity.com/tutorial/introduction-to-object-pooling#5ff8d015edbc2a002063971d
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        StartCoroutine(CreatePool());
    }
    /// <summary>
    /// Creates pool of objects (one in a frame to prevent 
    /// freezing/slowing at the start of the scene).
    /// </summary>
    IEnumerator CreatePool()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            yield return null;
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
