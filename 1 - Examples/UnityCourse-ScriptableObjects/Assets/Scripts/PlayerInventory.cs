using System;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{

    [SerializeField] private SO_List _inventory;

    private void Awake()
    {
        _inventory.Clear();
    }

    public void PickUp(SO_Weapon collectible)
    {
        _inventory.Add(collectible);
    }
    
}
