using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{

	[SerializeField] private Vector2Int _size;
	[SerializeField] private Vector2Int _startPos;
	
	[SerializeField] private float _threshold = 0.5f;
	
	[SerializeField] private Tilemap _tilemap;
	[SerializeField] private TileBase _tile;

	[Header("BSP")]
	[SerializeField][Range(0.1f, 1f)] private float _minCutRatio;
	[SerializeField][Range(0.1f, 1f)] private float _maxCutRatio;
	[SerializeField] private int _maxSurface = 1250;
	[SerializeField] private float _shrinkFactor = 0.8f;
	
	private List<BoundsInt> _rooms = new List<BoundsInt>();
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

	    // List<Vector2Int> generatedPositions = Generate();
	    // DrawMap(_tilemap, _tile, generatedPositions);

	    Vector2Int corner = _startPos - _size / 2;
	    BoundsInt originalRoom = new BoundsInt(new Vector3Int(corner.x, corner.y, 1), new Vector3Int(_size.x, _size.y, 1));

	    _rooms = PCG.BspTree.Generate(originalRoom, _minCutRatio, _maxCutRatio, _maxSurface);
	    _rooms = ShrinkRooms(_rooms);
	    
	    MakePaths(_rooms);

	    _tilemap.ClearAllTiles();
	    foreach (BoundsInt room in _rooms)
	    {
		    Debug.Log($"Room : {room.min} , {room.size}");
		    DrawMap(_tilemap, _tile, room);
	    }
		
    }
    
    
    private void MakePaths(List<BoundsInt> rooms)
    {
	    BoundsInt firstRoom = _rooms.OrderBy(r => Vector3.Distance(r.center, new Vector3(_startPos.x, _startPos.y, 0))).First();
    }

    private void OnDrawGizmos()
    {
	    foreach (BoundsInt room in _rooms)
	    { 
		    Gizmos.color = Color.red;
			Gizmos.DrawWireCube(room.center, room.size);
	    }
    }

    private List<Vector2Int> Generate()
    {
	    List<Vector2Int> positions = new List<Vector2Int>();
	    
	    for(int x = 0; x < _size.x; x++)
	    {
		    for(int y =  0; y < _size.y; y++)
		    {
			    float perlinValue = Mathf.PerlinNoise(x / (float)_size.x, y / (float)_size.y);
			    if(perlinValue > _threshold)
			    {
				    positions.Add(new Vector2Int(_startPos.x + x, _startPos.y + y));
			    }
		    }
	    }
	    
	    return positions;
    }
    
    private void DrawMap(Tilemap map, TileBase tile, List<Vector2Int> generatedPositions)
    {
	    _tilemap.ClearAllTiles();
	    
	    foreach (Vector2Int position in generatedPositions)
	    {
		    _tilemap.SetTile(new Vector3Int(position.x, position.y, 0), _tile);    
	    }
    }
    private void DrawMap(Tilemap map, TileBase tile, BoundsInt positions)
    {
	    foreach (var position in positions.allPositionsWithin)
	    {
		    _tilemap.SetTile(position, _tile);    
	    }
    }

    private List<BoundsInt> ShrinkRooms(List<BoundsInt> rooms)
    {
	    List<BoundsInt> newRooms = new List<BoundsInt>();
	    
	    foreach (BoundsInt room in rooms)
	    {
		    Vector3Int newSize = Vector3Int.RoundToInt(new Vector3(room.size.x * _shrinkFactor, room.size.y * _shrinkFactor, 1));
		    Vector3Int newPosition = Vector3Int.RoundToInt(room.center - new Vector3(0.5f * newSize.x, 0.5f * newSize.y, 1));
		    
		    newRooms.Add(new BoundsInt(newPosition, newSize));
		    
	    }

	    return newRooms;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
