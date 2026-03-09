using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class Items : ScriptableObject
{
    private int _item;
    public string ItemToString;
    
    public int Item
    {
        set
        {
            _item = value;
            ItemToString = _item.ToString("00");
        }
    }


}































[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float MaxHp;
    public float CurrentHp;
    

    public void Reset()
    {
        CurrentHp = MaxHp;
    }
}

public static class HealthConverters
{
    public const string GroupName = "HealthBar";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Register()
    {
        var group = new ConverterGroup(GroupName);

        // Pourcentage (0-1) vers largeur en %
        group.AddConverter((ref float percent) => 
            new StyleLength(new Length(percent * 100f, LengthUnit.Percent)));
        
        // Pourcentage (0-1) vers couleur
        group.AddConverter((ref float percent) =>
        {
            Color color;
            if (percent > 0.5f)
                color = Color.Lerp(Color.yellow, Color.green, (percent - 0.5f) * 2f);
            else
                color = Color.Lerp(Color.red, Color.yellow, percent * 2f);
            return new StyleColor(color);
        });
    }
}

