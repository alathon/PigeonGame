using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
    public GameObject pooledObject;
    public GameObject objectContainer;
    public int poolAmount = 20;
    public bool willGrow = true;
    List<GameObject> objectPool;

    private GameObject InstantiateObject()
    {
        var gObj = (GameObject)Instantiate(pooledObject);
        if (objectContainer != null) gObj.transform.SetParent(objectContainer.transform);
        gObj.SetActive(false);
        objectPool.Add(gObj);
        return gObj;
    }

    void Awake()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            this.InstantiateObject();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        if (willGrow)
        {
            return this.InstantiateObject();
        }

        return null;
    }
}