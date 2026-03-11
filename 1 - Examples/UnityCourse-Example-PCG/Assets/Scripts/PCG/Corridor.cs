using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    public struct Corridor
    {
        public BspNode A;
        public BspNode B;
        public Corridor(BspNode a, BspNode b)
        {
            A = a;
            B = b;
        }
        
        public static List<Vector2Int> StraightCorridors(Corridor corridor)
        {
            Vector3Int start = Vector3Int.RoundToInt(corridor.A.Center);
            Vector3Int end = Vector3Int.RoundToInt(corridor.B.Center);
            List<Vector2Int> tiles = new List<Vector2Int>();
            
            var direction = end - start;
            direction.Clamp(new Vector3Int(-1,-1,-1), new Vector3Int(1,1,1));
        
            Vector3Int current = start;
            do
            {
                current += direction;
                Debug.Log($"Current corridor: {current}");
                tiles.Add(new Vector2Int(current.x, current.y));
                
            }while(Vector3.Distance(current, end) >= Mathf.Epsilon);
            
            return tiles;
        
        }
    }
    
   
    
    
}
