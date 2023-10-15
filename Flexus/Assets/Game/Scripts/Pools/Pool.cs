using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;

    private readonly List<T> _pool = new List<T>(16);

    public T GetNew()
    {
        var reusable = _pool.FirstOrDefault(x => !x.gameObject.activeSelf);
        if (reusable)
        {
            reusable.gameObject.SetActive(true);
            return reusable;
        }
        else
        {
            var newOne = Instantiate(_prefab);
            _pool.Add(newOne);
            return newOne;
        }

    }
}