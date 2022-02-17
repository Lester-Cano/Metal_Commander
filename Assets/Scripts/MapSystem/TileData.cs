using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapSystem
{
    [CreateAssetMenu]
    public class TileData : ScriptableObject
    {
        public TileBase[] tiles;
    
        public bool isWalkable, isCol, isOccupied, isStation, isAirport;
        public Vector3 position;
    }
}
