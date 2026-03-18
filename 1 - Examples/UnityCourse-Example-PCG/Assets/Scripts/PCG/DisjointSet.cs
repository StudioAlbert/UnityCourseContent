using System.Collections.Generic;

namespace PCG
{
    public class BspCorridorsUnionFind
    {
        private readonly BspNode[] _parent;
        private readonly BspNode[] _rank;

        public BspCorridorsUnionFind(List<BspCorridor> corridorsGraph)
        {
            _parent = new BspNode[corridorsGraph.Count];
            _rank = new BspNode[corridorsGraph.Count];
            for (int i = 0; i < corridorsGraph.Count; i++)
            {
                _parent[i] = corridorsGraph[i].i;
            }
        }
        
        public BspNode Find(BspNode x)
        {
            if (_parent[x] != x)
                _parent[x] = Find(_parent[x]); // compression de chemin
            return _parent[x];
        }
        
        public bool Union(BspNode a, BspNode b)
        {
            int ra = Find(a), rb = Find(b);
            if (ra == rb) return false; // même composante → cycle

            if (_rank[ra] < _rank[rb]) (ra, rb) = (rb, ra);
            _parent[rb] = ra;
            if (_rank[ra] == _rank[rb]) _rank[ra]++;
            return true;
        }
    }
}
