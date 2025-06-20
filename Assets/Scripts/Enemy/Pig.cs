using UnityEngine;

public abstract class Pig : MonoBehaviour
{
    private EnemiesHolder _enemiesHolder;

    protected bool IsActive;

    private void Awake()
    {
        _enemiesHolder = transform.parent.GetComponent<EnemiesHolder>();
    }

    private void OnEnable()
    {
        _enemiesHolder.RoomChangeStatus += OnRoomChangeStatus; 
    }

    private void OnDisable()
    {
        _enemiesHolder.RoomChangeStatus -= OnRoomChangeStatus;
    }

    private void OnRoomChangeStatus(bool roomStatus)
    {
        IsActive = roomStatus;
    }
}
