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
    private List<Vector3Int> addLava, remLava;

    private void Start()
    {
        addLava = new List<Vector3Int>();
        remLava = new List<Vector3Int>();
        clockTick = Time.time + tickRate;
    }

    void Update()
    {
        if (Time.time > clockTick)
        {
            clockTick += tickRate;
            addLava.Clear();
            remLava.Clear();

            for (int x = 0; x < bounds.size.x; x++)
            {
                Vector3Int p = Vector3Int.zero;
                p.x = x + bounds.position.x;
                p.y = bounds.position.y + bounds.size.y - 1;
                tilemap.SetTile(p, lavaTile);
            }


            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                { 
                    pos.x = bounds.position.x + x;
                    pos.y = bounds.position.y + y;
                    pos.z = 0;

                    if (tilemap.GetTile(pos) != lavaTile)
                        continue;

                    // Nothing below, then flow downward
                    if (!tilemap.HasTile(pos + Vector3Int.down))
                    {
                        addLava.Add(pos + Vector3Int.down);
                        //tilemap.SetTile(pos + Vector3Int.down, lavaTile);
                    }

                    // Check left/up for any flow
                    bool hasFlow = false;
                    Vector3Int flowPos = pos;
                    for (int scanx = x; scanx >= 0; scanx--, flowPos += Vector3Int.left)
                    {
                        if (tilemap.GetTile(flowPos) != lavaTile)
                            break;
                        if (tilemap.GetTile(flowPos + Vector3Int.up) == lavaTile)
                        {
                            hasFlow = true;
                            break;
                        }
                    }
                    // Check right/up for any flow
                    flowPos = pos;
                    for (int scanx = x; scanx < bounds.size.x; scanx++, flowPos += Vector3Int.right)
                    {
                        if (tilemap.GetTile(flowPos) != lavaTile)
                            break;
                        if (tilemap.GetTile(flowPos + Vector3Int.up) == lavaTile)
                        {
                            hasFlow = true;
                            break;
                        }
                    }
                    // Remove anything that has no flow above it
                    if (!hasFlow)
                    {
                        remLava.Add(pos);
                        //tilemap.SetTile(pos, null);
                        continue;
                    }

                    // Something is below and isn't lava
                    if (tilemap.GetTile(pos + Vector3Int.down) != lavaTile && tilemap.HasTile(pos + Vector3Int.down))
                    {
                        if (!tilemap.HasTile(pos + Vector3Int.left))
                        {
                            addLava.Add(pos + Vector3Int.left);
                            //tilemap.SetTile(pos + Vector3Int.left, lavaTile);
                        }
                        if (!tilemap.HasTile(pos + Vector3Int.right))
                        {
                            addLava.Add(pos + Vector3Int.right);
                            //tilemap.SetTile(pos + Vector3Int.right, lavaTile);
                        }
                    }

                }
            }


            foreach (Vector3Int item in addLava)
            {
                tilemap.SetTile(item, lavaTile);
            }
            foreach (Vector3Int item in remLava)
            {
                tilemap.SetTile(item, null);
            }
            
            
            //// Update lava tiles
            //TileBase[] tileArray = tilemap.GetTilesBlock(bounds);
            //Debug.Log(tileArray);
            //        int index = x + y * bounds.size.x;
            //        TileBase tile = tileArray[index];
            //        TileBase tile_u = tileArray[Math.Min(bounds.size.x * bounds.size.y, x + y * (bounds.size.x + 1))];
            //        TileBase tile_d = tileArray[Math.Max(0, x + y * (bounds.size.x - 1))];
            //        TileBase tile_l = tileArray[Math.Max(0, x + y * (bounds.size.x) - 1)];
            //        TileBase tile_r = tileArray[Math.Min(bounds.size.x * bounds.size.y, x + y * (bounds.size.x) + 1)];
            //        pos.x = bounds.position.x + x;
            //        pos.y = bounds.position.y + y;
            //        pos.z = 0;
            //        // Loop over all lava tiles
            //        if (tile == lavaTile)
            //        {
            //            // Nothing below, then flow downward
            //            if (tile_d == null)
            //            {
            //                tilemap.SetTile(pos + Vector3Int.down, lavaTile);
            //            }
            //            // Something is below and isn't lava
            //            else if (tile_d != lavaTile)
            //            {
            //                if (tile_l == null)
            //                {
            //                    tilemap.SetTile(pos + Vector3Int.left, lavaTile);
            //                }
            //                if (tile_r == null)
            //                {
            //                    tilemap.SetTile(pos + Vector3Int.right, lavaTile);
            //                }
            //            }
            //            // Check left/up for any flow
            //            bool hasFlow = false;
            //            int scanindex = index;
            //            for (int scanx = x; scanx >= 0; scanx--)
            //            {
            //                if (tileArray[scanindex] != lavaTile)
            //                    break;
            //                if (tileArray[Math.Min(bounds.size.x * bounds.size.y, scanindex + bounds.size.x)] == lavaTile)
            //                {
            //                    hasFlow = true;
            //                    break;
            //                }
            //                scanindex--;
            //            }
            //            // Check right/up for any flow
            //            scanindex = index;
            //            for (int scanx = x; scanx < bounds.size.x; scanx++)
            //            {
            //                if (tileArray[scanindex] != lavaTile)
            //                    break;
            //                if (tileArray[Math.Min(bounds.size.x * bounds.size.y, scanindex + bounds.size.x)] == lavaTile)
            //                {
            //                    hasFlow = true;
            //                    break;
            //                }
            //                scanindex++;
            //            }
            //        }

        }
    }
}
