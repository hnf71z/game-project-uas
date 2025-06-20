using UnityEngine;

public class PigBomber : Thrower
{
    protected override void FindItemPool()
    {
        Items = FindObjectOfType<Bombs>();
    }
}
