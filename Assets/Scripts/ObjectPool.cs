using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected int Capacity;
    [SerializeField] private Transform _container;

    protected List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject _template)
    {
        for (int i = 0; i < Capacity; i++)
        {
            GameObject spawned = Instantiate(_template, _container);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected void Initialize(GameObject[] _templates)
    {
        for (int i = 0; i < Capacity; i++)
        {
            int _templateIndex = Random.Range(0, _templates.Length);
            GameObject spawned = Instantiate(_templates[_templateIndex], _container);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }

    protected bool TryGetLastActive(out GameObject result)
    {
        result = _pool.LastOrDefault(p => p.activeSelf == true);
        return result != null;
    }

    protected void DisableObjectsAbroadScreen()
    {
        Camera camera = Camera.main;

        foreach (GameObject obj in _pool)
        {
            if (obj.activeSelf)
            {
                Vector3 point = camera.WorldToViewportPoint(obj.transform.position);

                if (point.y < -10)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
