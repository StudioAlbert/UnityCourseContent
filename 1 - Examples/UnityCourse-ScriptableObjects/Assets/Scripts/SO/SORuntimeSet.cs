using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableValues/runtimeSet", fileName = "runtimeSet")]
public class SORuntimeSet : ScriptableObject
{

    [SerializeField] private int _limit;
    
    private readonly List<LootData> _list = new List<LootData>();
    public List<LootData> List => _list;

    public void Clear()
    {
        _list.Clear();
    }
    
    public bool Add(LootData myObject)
    {
        if(_list.Count < _limit)
        {
            _list.Add(myObject);
            return true;
        }
        return false;
    }

    public void Remove(LootData objToRemove)
    {
        _list.Remove(objToRemove);
    }

}
