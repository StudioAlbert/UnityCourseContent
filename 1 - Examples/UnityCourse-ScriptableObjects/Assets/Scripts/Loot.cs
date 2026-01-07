using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private LootData _data;
    public LootData Data { get; }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            if(player.Pickup(_data))
                Destroy(gameObject);
        }
    }

}
