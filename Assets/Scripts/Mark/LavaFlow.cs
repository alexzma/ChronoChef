using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;

// Could extend this to a base class ChronoTilemap later with ChronoUpdate as a member function
public class LavaFlow : MonoBehaviour
{
    public float tickRate = 1;
    public Tilemap tilemap;
    public Tilemap rockTilemap;
    public RuleTile lavaTile;
    public Tile rockTile;
    public Tile nullTile;
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

    public void ChronoUpdate(int dir, Vector3 pos)
    {
        
        // Get the value of map pos
        Vector3Int tilePos = tilemap.WorldToCell(pos);
        Debug.Log("Activating ChronoUpdate at " + tilePos);
        if (rockTilemap.GetTile(tilePos) == rockTile && dir == -1)
        {
            tilemap.SetTile(tilePos, lavaTile);
            rockTilemap.SetTile(tilePos, null);
        }
        else if (tilemap.GetTile(tilePos) == lavaTile && dir == 1)
        {
            tilemap.SetTile(tilePos, null);
            rockTilemap.SetTile(tilePos, rockTile);
        }
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
                    if (!CustomHasTile(pos + Vector3Int.down))
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
                    if (tilemap.GetTile(pos + Vector3Int.down) != lavaTile && CustomHasTile(pos + Vector3Int.down))
                    {
                        if (!CustomHasTile(pos + Vector3Int.left))
                        {
                            addLava.Add(pos + Vector3Int.left);
                            //tilemap.SetTile(pos + Vector3Int.left, lavaTile);
                        }
                        if (!CustomHasTile(pos + Vector3Int.right))
                        {
                            addLava.Add(pos + Vector3Int.right);
                            //tilemap.SetTile(pos + Vector3Int.right, lavaTile);
                        }
                    }
                }
            }
            foreach (Vector3Int item in addLava)
            { 
                if (bounds.Contains(item))
                    tilemap.SetTile(item, lavaTile);
            }
            foreach (Vector3Int item in remLava)
            {
                if (bounds.Contains(item))
                    tilemap.SetTile(item, nullTile);
            }
        }
    }

    public bool CustomHasTile(Vector3Int pos)
    {
        if (tilemap.GetTile(pos) == lavaTile)
            return true;
        else if (rockTilemap.HasTile(pos))
            return true;
        else
            return false;
    }
}
