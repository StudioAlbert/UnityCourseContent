using System;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private SO_Item _itemDatas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_itemDatas && other.TryGetComponent(out PlayerStats stats))
        {
            stats.Heal(_itemDatas.Health);
            stats.MakeMoney(_itemDatas.Gold);
            stats.GainMana(_itemDatas.Mana);

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer && _itemDatas.LootedSprite)
            {
                spriteRenderer.sprite = _itemDatas.LootedSprite;
            }

            Collider2D collider2D = GetComponent<Collider2D>();
            collider2D.enabled = false;
            
        }
    }
}
