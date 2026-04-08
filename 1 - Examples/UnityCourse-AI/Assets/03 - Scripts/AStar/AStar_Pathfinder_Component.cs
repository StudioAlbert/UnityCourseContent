using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar_Pathfinder_Component : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private Tilemap _map;
    [SerializeField] private BoundsInt _boundsInt;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    [Header("Settings")]
    [SerializeField] private float _pathRefreshRate = 1;
    
    [Header("Debug")]
    [SerializeField] private Tilemap _pathMap;
    [SerializeField] private TileBase _pathTile;

    private List<Vector3Int> _walkables = new List<Vector3Int>();
    private Vector3Int[] _realPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        StartCoroutine(Pathfinding());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
    void Start()
    {
        _map.CompressBounds();
        var mapBounds = _map.cellBounds;

        foreach (var pos in _map.cellBounds.allPositionsWithin)
        {
            if (_map.HasTile(pos)) _walkables.Add(pos);
        }

        Debug.Log("fini");

    }

    IEnumerator Pathfinding()
    {
        do
        {
            // Filter walkables
            List<Vector3Int> aiWalkables = _walkables.Where(w => AStarProcess.HasWalkableNeighbours(_walkables, w)).ToList();

            // Calculate path
            var path = AStarProcess.Process(aiWalkables, Vector3Int.FloorToInt(_start.position), Vector3Int.FloorToInt(_end.position));

            // Draw the path
            _pathMap.ClearAllTiles();
            if (path.Length > 0)
            {
                Utils.DrawMap(_pathMap, _pathTile, path);
            }

            // End of process, update "real" path
            _realPath = path;

            yield return new WaitForSeconds(_pathRefreshRate);
            
        } while (true);
    }
    
    
}
