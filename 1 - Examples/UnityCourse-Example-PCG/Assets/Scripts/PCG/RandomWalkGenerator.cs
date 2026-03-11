using System.Collections.Generic;
using PCG;
using UnityEngine;

public static class RandomWalkGenerator
{
    public static void Generate(
        out HashSet<Vector2Int> positions,
        out List<Vector2Int> points,
        Vector2Int startPos,
        int positionsMax,
        int pointsMax,
        int lMin,
        int lMax
    )
    {
        positions = new HashSet<Vector2Int>();
        points = new List<Vector2Int>();
        
        List<Vector2Int> directions = new List<Vector2Int>(Utils.VonNeumannDirections);
        Vector2Int position = startPos;
        points.Add(startPos);
        
        while (positions.Count < positionsMax && points.Count < pointsMax)
        {
            Vector2Int direction = directions[Random.Range(0, directions.Count)];
            int currentPathLength = Random.Range(lMin, lMax);

            Vector2Int futurePosition = position + currentPathLength * new Vector2Int(direction.x, direction.y);

            for (int i = 0; i < currentPathLength; i++)
            {
                position += new Vector2Int(direction.x, direction.y);
                positions.Add(position);
            }
            points.Add(futurePosition);

            Debug.Log($"Tiles Count {positions.Count} : Iterations Count {points.Count}");

        }

    }
}
