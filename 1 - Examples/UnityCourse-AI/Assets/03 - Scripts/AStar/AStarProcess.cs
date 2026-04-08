using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class AStarPoint
{
    public Vector3Int Pos;

    public float G; // Dijkstra 
    public float H; // Heuristic
    public AStarPoint Parent = null;

    public AStarPoint(Vector3Int pos, float g, float h, AStarPoint parent)
    {
        Pos = pos;
        G = g;
        H = h;
        Parent = parent;
    }

    public float F => G + H; // Global score

}

public static class AStarProcess
{
    public static Vector3Int[] Process(List<Vector3Int> aiWalkables, Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        
        if (aiWalkables.Contains(start) && aiWalkables.Contains(end))
        {
            // Basics stuff --------
            // path.Add(start);
            // path.Add(end);

            List<AStarPoint> openPoints = new List<AStarPoint>();
            openPoints.Add(new AStarPoint(start, 0, Vector3Int.Distance(start, end), null));

            List<Vector3Int> closedPoints = new List<Vector3Int>();

            do
            {

                // BFS pathing 
                AStarPoint currentPoint = openPoints.OrderBy(p => p.F).First();
                openPoints.Remove(currentPoint);

                // We found the end
                if (currentPoint.Pos == end)
                {
                    Debug.Log("J'ai trouvé la sortie, elle est dans cette pièce.");
                    GetPath(currentPoint, path);
                    return path.ToArray();
                }


                // Check neighbours
                foreach (var neighbour in Utils.MooreDirections.OrderBy(_ => Random.value))
                {
                    Vector3Int pos = currentPoint.Pos + neighbour;
                    // Add a point if
                    // - the point is in the map
                    // - the point is not already checked
                    // if (walkables.Contains(pos) && !closedPoints.Contains(pos))
                    if (aiWalkables.Contains(pos))
                    {
                        float newG = currentPoint.G + Vector3Int.Distance(pos, currentPoint.Pos);
                        float newH = Vector3Int.Distance(pos, end);

                        AStarPoint existingPoint = openPoints.FirstOrDefault(p => p.Pos == pos);
                        if (existingPoint == null)
                        {
                            openPoints.Add(new AStarPoint(pos, newG, newH, currentPoint));
                        }
                        else if (existingPoint.F > newH + newG)
                        {
                            existingPoint.G = newG;
                            existingPoint.H = newH;
                            existingPoint.Parent = currentPoint;
                        }

                        // Check for points already checked
                        // closedPoints.Add(pos);
                    }
                }


            } while (openPoints.Count > 0);

        }

        return path.ToArray();
    }

    private static void GetPath(AStarPoint pathPoint, List<Vector3Int> path)
    {
        path.Add(pathPoint.Pos);
        if (pathPoint.Parent != null)
        {
            GetPath(pathPoint.Parent, path);
        }
    }

    public static bool HasWalkableNeighbours(List<Vector3Int> walkables, Vector3Int position)
    {
        int neighbourCount = 0;
        var neighbourhood = Utils.MooreDirections;
        
        // Check neighbours
        foreach (Vector3Int neighbour in neighbourhood)
        {
            if (walkables.Contains(position + neighbour)) neighbourCount++;
        }
        return neighbourCount >= neighbourhood.Length;
    }
}
