using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "Dungeon Datas/Values/List")]
public class SO_List : ScriptableObject
{
    public List<SO_Weapon> _items = new List<SO_Weapon>();

    public Action<Event> OnListChanged;
    
    public void Clear() => _items.Clear();
    
    public void Add(SO_Weapon item){

        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }

    public List<SO_Weapon> Items => _items;

}
