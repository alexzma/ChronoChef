using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileClick : MonoBehaviour
{
    
    public Tilemap tilemap;
    
    public TileBase swaptile;
    public RuleTile ruleTile;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Vector3Int tilePos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(pos));
            tilemap.SetTile(tilePos, ruleTile);
            //tilemap.RefreshAllTiles();
        }

    }

}
