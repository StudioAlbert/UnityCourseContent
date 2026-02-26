using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RoomType
{
    Fight,
    Shop,
    Boss,
    End,
    Pool,
    Secret
}

[System.Serializable]
public struct Link
{
    public float Weight;
    public RoomStereotype Room;
}

[CreateAssetMenu(fileName = "RoomStereotype", menuName = "Scriptable Objects/RoomStereotype")]
public class RoomStereotype : ScriptableObject
{
    public Vector2 Size;
    public RoomType Type;

    public List<Link> Links =  new List<Link>();

    public RoomStereotype NextRoom()
    {
        if (Links.Count > 0)
        {
            float rng = Random.value * Links.Sum(l => l.Weight);
            float rngSum = 0;
            foreach (Link link in Links)
            {
                if (rng < rngSum + link.Weight)
                {
                    return link.Room;
                }
                rngSum += link.Weight;
            }
        }
        
        return null;
    }

}
