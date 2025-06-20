using UnityEngine;
using UnityEngine.Events;

public class EnemiesHolder : MonoBehaviour
{
    public event UnityAction<bool> RoomChangeStatus;

    public void ChangeEnemiesState(bool status)
    {
        RoomChangeStatus?.Invoke(status);
    }
}
