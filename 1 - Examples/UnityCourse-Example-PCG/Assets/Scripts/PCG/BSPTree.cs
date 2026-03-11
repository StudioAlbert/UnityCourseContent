using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PCG
{

    public enum CutType
    {
        Horizontal,
        Vertical
    }

    public class BspNode
    {
        public BoundsInt Bounds;
        public CutType CutType;
        public List<BspNode> Children;
        public BspNode Parent = null;

        public Vector3 Center;

        public BspNode(Vector3 center, BoundsInt bounds, CutType cutType, BspNode parent)
        {
            Center = center;
            Bounds = bounds;
            CutType = cutType;
            Parent = parent;

            Children = new List<BspNode>();

        }

        public bool IsLeaf => Children.Count == 0;
    }

    public class BspTree
    {

        public static void Generate(out BspNode root, BoundsInt firstRoom, float minCutRatio, float maxCutRatio,
            float surfaceCriteria, float jitter)
        {
            CutType cutType = Random.value > 0.5f ? CutType.Horizontal : CutType.Vertical;

            Queue<BspNode> cutsQueue = new Queue<BspNode>();
            root = new BspNode(Vector3.zero, firstRoom, cutType, null);
            cutsQueue.Enqueue(root);

            do
            {
                BspNode roomToCut = cutsQueue.Dequeue();

                if ((roomToCut.Bounds.size.x * roomToCut.Bounds.size.y) > surfaceCriteria)
                {

                    float cutRatio = Random.Range(Mathf.Min(minCutRatio, maxCutRatio), Mathf.Max(minCutRatio, maxCutRatio));

                    BoundsInt[] boundsCutResult = new BoundsInt[2];
                    CutType cutTypeResult = CutType.Horizontal;
                    switch (roomToCut.CutType)
                    {
                        case CutType.Horizontal:
                            boundsCutResult = CutVertical(roomToCut.Bounds, cutRatio);
                            cutTypeResult = CutType.Vertical;
                            break;
                        case CutType.Vertical:
                            boundsCutResult = CutHorizontal(roomToCut.Bounds, cutRatio);
                            cutTypeResult = CutType.Horizontal;
                            break;
                    }
                    foreach (BoundsInt boundsInt in boundsCutResult)
                    {
                        BspNode newNode = new BspNode(
                            // boundsInt.center,
                            boundsInt.center + jitter * Random.insideUnitSphere,
                            boundsInt,
                            cutTypeResult,
                            roomToCut);
                        roomToCut.Children.Add(newNode);

                        cutsQueue.Enqueue(newNode);
                    }
                }
                else
                {
                    // nbRooms++;
                }

            } while (cutsQueue.Count > 0);

            Debug.Log($"C'est fini !!!!!!!!!!!");

        }

        private static BoundsInt[] CutVertical(BoundsInt room, float cutRatio)
        {

            BoundsInt roomA = new BoundsInt(
                room.min,
                new Vector3Int(Mathf.RoundToInt(room.size.x * cutRatio), room.size.y, room.size.z)
            );
            BoundsInt roomB = new BoundsInt(
                new Vector3Int(roomA.max.x, roomA.min.y, roomA.min.z),
                new Vector3Int(Mathf.RoundToInt(room.size.x * (1 - cutRatio)), room.size.y, room.size.z)
            );

            return new[] { roomA, roomB };

        }

        private static BoundsInt[] CutHorizontal(BoundsInt room, float cutRatio)
        {

            BoundsInt roomA = new BoundsInt(
                room.min,
                new Vector3Int(room.size.x, Mathf.RoundToInt(room.size.y * cutRatio), room.size.z)
            );
            BoundsInt roomB = new BoundsInt(
                new Vector3Int(roomA.min.x, roomA.max.y, roomA.min.z),
                new Vector3Int(room.size.x, Mathf.RoundToInt(room.size.y * (1 - cutRatio)), room.size.z)
            );

            return new[] { roomA, roomB };

        }

        public static List<BoundsInt> MakeRooms(BspNode root, float shrinkFactor)
        {
            List<BoundsInt> rooms = new List<BoundsInt>();
            Stack<BspNode> pathQueue = new Stack<BspNode>();

            pathQueue.Push(root);

            do
            {

                BspNode current = pathQueue.Pop();

                if (current.IsLeaf)
                {
                    rooms.Add(SampleRoom(current, shrinkFactor));
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        pathQueue.Push(child);
                    }
                }

            } while (pathQueue.Count > 0);

            return rooms;

        }

        private static BoundsInt SampleRoom(BspNode node, float shrinkFactor)
        {
            Vector3Int newSize = Vector3Int.RoundToInt(new Vector3(node.Bounds.size.x * shrinkFactor, node.Bounds.size.y * shrinkFactor, 1));
            Vector3Int newPosition =
                Vector3Int.RoundToInt(
                    node.Center -
                    new Vector3(0.5f * newSize.x, 0.5f * newSize.y, 1)
                );

            return new BoundsInt(newPosition, newSize);

        }

        public static List<Corridor> MakeCorridors(BspNode root)
        {

            List<Corridor> corridors = new List<Corridor>();
            Queue<BspNode> pathQueue = new Queue<BspNode>();

            pathQueue.Enqueue(root);

            do
            {

                BspNode current = pathQueue.Dequeue();

                if (!current.IsLeaf)
                {
                    corridors.Add(new Corridor(current.Children[0], current.Children[1]));
                    foreach (var child in current.Children.FindAll(c => !c.IsLeaf))
                    {
                        pathQueue.Enqueue(child);
                    }
                }

            } while (pathQueue.Count > 0);

            return corridors;


        }
    }
}
