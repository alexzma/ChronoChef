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
            
            for (int x = 0; x < bounds.size.x; x++)
            {
                Vector3Int p = Vector3Int.zero;
                p.x = x + bounds.position.x;
                p.y = bounds.position.y + bounds.size.y;
                tilemap.SetTile(p, lavaTile);
            }
            
            // Update lava tiles
            TileBase[] tileArray = tilemap.GetTilesBlock(bounds);
            Debug.Log(tileArray);
            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y - 1; y++)
                {
                    int index = x + y * bounds.size.x;
                    TileBase tile = tileArray[index];
                    TileBase tile_u = tileArray[Math.Min(bounds.size.x * bounds.size.y, x + y * (bounds.size.x + 1))];
                    TileBase tile_d = tileArray[Math.Max(0, x + y * (bounds.size.x - 1))];
                    TileBase tile_l = tileArray[Math.Max(0, x + y * (bounds.size.x) - 1)];
                    TileBase tile_r = tileArray[Math.Min(bounds.size.x * bounds.size.y, x + y * (bounds.size.x) + 1)];
                    pos.x = bounds.position.x + x;
                    pos.y = bounds.position.y + y;
                    pos.z = 0;
                    // Loop over all lava tiles
                    if (tile == lavaTile)
                    {
                        // Nothing below, then flow downward
                        if (tile_d == null)
                        {
                            tilemap.SetTile(pos + Vector3Int.down, lavaTile);
                        }
                        // Something is below and isn't lava
                        else if (tile_d != lavaTile)
                        {
                            if (tile_l == null)
                            {
                                tilemap.SetTile(pos + Vector3Int.left, lavaTile);
                            }
                            if (tile_r == null)
                            {
                                tilemap.SetTile(pos + Vector3Int.right, lavaTile);
                            }
                        }
                        // Check left/up for any flow
                        bool hasFlow = false;
                        int scanindex = index;
                        for (int scanx = x; scanx >= 0; scanx--)
                        {
                            if (tileArray[scanindex] != lavaTile)
                                break;
                            if (tileArray[Math.Min(bounds.size.x * bounds.size.y, scanindex + bounds.size.x)] == lavaTile)
                            {
                                hasFlow = true;
                                break;
                            }
                            scanindex--;
                        }
                        // Check right/up for any flow
                        scanindex = index;
                        for (int scanx = x; scanx < bounds.size.x; scanx++)
                        {
                            if (tileArray[scanindex] != lavaTile)
                                break;
                            if (tileArray[Math.Min(bounds.size.x * bounds.size.y, scanindex + bounds.size.x)] == lavaTile)
                            {
                                hasFlow = true;
                                break;
                            }
                            scanindex++;
                        }
                    }
                    //if (tile == lavaTile)
                    //{ 
                    //    // Nothing below, then flow downward
                    //    if (!tilemap.HasTile(pos + Vector3Int.down))
                    //    {
                    //        tilemap.SetTile(pos + Vector3Int.down, lavaTile);
                    //    }
                    //    // Something is below and isn't lava
                    //    else if (tilemap.GetTile(pos + Vector3Int.down) != lavaTile)
                    //    {
                    //        if (!tilemap.HasTile(pos + Vector3Int.left))
                    //        {
                    //            tilemap.SetTile(pos + Vector3Int.left, lavaTile);
                    //        }
                    //        if (!tilemap.HasTile(pos + Vector3Int.right))
                    //        {
                    //            tilemap.SetTile(pos + Vector3Int.right, lavaTile);
                    //        }
                    //    }
                    //    // Check left/up for any flow
                    //    bool hasFlow = false;
                    //    Vector3Int flowPos = pos;
                    //    for (int scanx = x; scanx >= 0; scanx--, flowPos += Vector3Int.left)
                    //    {
                    //        if (tilemap.GetTile(flowPos) != lavaTile)
                    //            break;
                    //        if (tilemap.GetTile(flowPos + Vector3Int.up) == lavaTile)
                    //        {
                    //            hasFlow = true;
                    //            break;
                    //        }
                    //    }
                    //    // Check right/up for any flow
                    //    flowPos = pos;
                    //    for (int scanx = x; scanx < bounds.size.x; scanx++, flowPos += Vector3Int.right)
                    //    {
                    //        if (tilemap.GetTile(pos) != lavaTile)
                    //            break;
                    //        if (tilemap.GetTile(pos + Vector3Int.up) == lavaTile)
                    //        {
                    //            hasFlow = true;
                    //            break;
                    //        }
                    //    }


                    //    // Remove anything that has no flow above it
                    //    if (!hasFlow)
                    //    {
                    //        tilemap.SetTile(pos, null);
                    //        break;
                    //    }
                    //}
                }
            }
        }
    }
}
