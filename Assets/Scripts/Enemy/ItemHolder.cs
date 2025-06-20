using UnityEngine;

public abstract class ItemHolder : ObjectPool
{
    protected abstract GameObject Template { get; }

    private void Awake()
    {
        Initialize(Template);
    }

    public GameObject GetItem()
    {
        return TryGetObject(out GameObject item) ? item : null;
    }

    private void Update()
    {
        DisableObjectsAbroadScreen();
    }
}
