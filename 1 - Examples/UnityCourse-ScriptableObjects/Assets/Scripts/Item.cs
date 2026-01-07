using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{

    [SerializeField] private ItemData _itemData;
    
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            Debug.Log("Player met !");
            Debug.Log("More money : " + _itemData.MoneyGain());
            
            player.Heal(_itemData.HealthGain);
            Destroy(gameObject, 0.5f);
            
        }
    }
}
