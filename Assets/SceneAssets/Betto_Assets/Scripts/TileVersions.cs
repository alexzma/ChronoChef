using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileVersions : ScriptableObject
{
    // Tiles in the order of past to future.
    // That is, tiles[0] is the oldest tile (in terms of timeline) while tiles[tiles.size] is the most future tile0
    public TileVersion[] tiles;
}
