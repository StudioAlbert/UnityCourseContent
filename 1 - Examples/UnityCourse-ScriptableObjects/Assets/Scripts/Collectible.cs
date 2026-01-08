using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private SO_Weapon _datas;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerInventory playerInventory))
            {
                playerInventory.PickUp(_datas);
                gameObject.SetActive(false);
            }
        }

    }
}
