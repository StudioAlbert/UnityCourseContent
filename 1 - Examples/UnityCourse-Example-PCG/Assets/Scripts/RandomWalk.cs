using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomWalk : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] Tilemap _tilemap;
    [SerializeField] TileBase _tile;
    
    [Header("Random Walk Generator")]
    [SerializeField] private Vector2Int _startPos;
    [SerializeField] private int _nbTilesMax;
    [SerializeField] private int _iterMax;
    [SerializeField] private int _lMin;
    [SerializeField] private int _lMax;
    
    [Header("Rooms generation")]
    [SerializeField] private Vector2Int _roomSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomWalkGenerator.Generate(out HashSet<Vector2Int> positions, out List<Vector2Int> points, _startPos, _nbTilesMax, _iterMax, _lMin, _lMax);

        foreach (Vector2Int pt in points)
        {
            for (int x = pt.x - _roomSize.x / 2; x <= pt.x + _roomSize.x / 2; x++ )
            {
                for (int y = pt.y - _roomSize.y / 2; y <= pt.y + _roomSize.y / 2; y++)
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }
        }
        
        PCG.Utils.DrawMap(_tilemap, _tile, positions.ToList());
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
