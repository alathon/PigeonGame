using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

public class BulletPoolManager : MonoBehaviour
{
    private static BulletPoolManager _instance;

    public static BulletPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BulletPoolManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        foreach (var c in this.transform.GetComponentsInChildren<ObjectPool>())
        {
            _bulletPools.Add(c.pooledObject.name, c);
        }
    }

    private Dictionary<string, ObjectPool> _bulletPools = new Dictionary<string, ObjectPool>();

    public GameObject GetGameObject(string type)
    {
        if (!_bulletPools.ContainsKey(type))
        {
            Debug.Log("Warning: Bullet type not found: " + type);
        }

        return _bulletPools[type].GetPooledObject();
    }
}