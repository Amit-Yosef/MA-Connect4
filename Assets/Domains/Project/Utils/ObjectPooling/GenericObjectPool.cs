using System.Collections.Generic;
using UnityEngine;

namespace Project.Utils.ObjectPooling
{
    public class GenericObjectPool<T> where T : Component
    {
        private readonly List<T> _pool = new();
        private readonly T _prefab;
        private readonly Transform _parent;
        private const int InitialPoolSize = 5;

        public GenericObjectPool(T prefab, string poolName, bool dontDestroyOnLoad = false)
        {
            _prefab = prefab;
            _parent = new GameObject(poolName).transform;
            if (dontDestroyOnLoad) Object.DontDestroyOnLoad(_parent);
            
            for (int i = 0; i < InitialPoolSize; i++)
            {
                AddObjectToPool();
            }
        }

        public T GetObject()
        {
            foreach (var obj in _pool)
            {
                if (!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
            var newObject = AddObjectToPool();
            newObject.gameObject.SetActive(true);
            return newObject;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private T AddObjectToPool()
        {
            var newObj = Object.Instantiate(_prefab, _parent);
            newObj.gameObject.SetActive(false);
            _pool.Add(newObj);
            return newObj;
        }
    }
}