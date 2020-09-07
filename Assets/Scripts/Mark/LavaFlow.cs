using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;

public class LavaFlow : MonoBehaviour
{
    public float tickRate = 1;
    public Tilemap tilemap;
    public RuleTile lavaTile;
    public BoundsInt bounds;

    private Vector3Int pos;
    private float clockTick;

    private void Start()
    {
        clockTick = Time.time + tickRate;      
    }

    void Update()
    {
        
        if (Time.time > clockTick)
        {
            clockTick += tickRate;
            TileBase[] tileArray = tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = tileArray[x + y * bounds.size.x];
                    pos.x = bounds.position.x + x;
                    pos.y = bounds.position.y + y;
                    pos.z = 0;
                    // Loop over all lava tiles
                    if (tile == lavaTile)
                    {
                        // Nothing below, then flow downward
                        if (!tilemap.HasTile(pos + Vector3Int.down))
                        {
                            tilemap.SetTile(pos + Vector3Int.down, lavaTile);
                        }
                        // Something is below and isn't lava
                        else if (tilemap.GetTile(pos + Vector3Int.down) != lavaTile)
                        {
                            if (!tilemap.HasTile(pos + Vector3Int.left))
                            {
                                tilemap.SetTile(pos + Vector3Int.left, lavaTile);
                            }
                            if (!tilemap.HasTile(pos + Vector3Int.right))
                            {
                                tilemap.SetTile(pos + Vector3Int.right, lavaTile);
                            }
                        }
                    }
                }
            }





        }
    }

}
