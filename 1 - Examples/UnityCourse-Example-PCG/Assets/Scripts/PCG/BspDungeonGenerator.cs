
using System.Collections.Generic;
using System.Linq;
using PCG;
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
	[SerializeField] private float _jitter = 25;

	[Header("Corridors")]
	[SerializeField] private int _corridorSize = 3;
	
	private List<BoundsInt> _rooms = new List<BoundsInt>();
	private List<Corridor>	_corridors = new List<Corridor>();
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

	    // List<Vector2Int> generatedPositions = Generate();
	    // DrawMap(_tilemap, _tile, generatedPositions);

	    Vector2Int corner = _startPos - _size / 2;
	    BoundsInt originalRoom = new BoundsInt(new Vector3Int(corner.x, corner.y, 1), new Vector3Int(_size.x, _size.y, 1));

	    PCG.BspTree.Generate(out BspNode root, originalRoom, _minCutRatio, _maxCutRatio, _maxSurface, _jitter);
	    _rooms = PCG.BspTree.MakeRooms(root, _shrinkFactor);
	    _corridors = PCG.BspTree.MakeCorridors(root);

	    var corridorTiles = new List<Vector2Int>();
	    foreach (Corridor corridor in _corridors)
	    {
		    corridorTiles.AddRange(PCG.Corridor.StraightCorridors(corridor));
	    }

	    _tilemap.ClearAllTiles();
	    foreach (BoundsInt room in _rooms)
	    {
		    Debug.Log($"Room : {room.min} , {room.size}");
		    PCG.Utils.DrawMap(_tilemap, _tile, room);
	    }
	    PCG.Utils.DrawMap(_tilemap, _tile, corridorTiles);
		
    }
    
    private void OnDrawGizmos()
    {
	    foreach (BoundsInt room in _rooms)
	    { 
		    Gizmos.color = Color.red;
			Gizmos.DrawWireCube(room.center, room.size);
	    }

	    foreach (Corridor corridor in _corridors)
	    {
		    Gizmos.color = Color.darkOrange;
		    Gizmos.DrawLine(corridor.A.Bounds.center, corridor.B.Bounds.center);
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

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
