using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChronoTileManager : MonoBehaviour
{
    // Could extend this in the future to contain other tilemaps
    
    //public Tilemap tilemap;
    public LavaFlow lavaflow;
    
    public void activateChrono(int dir, Vector3 pos)
    {
        lavaflow.ChronoUpdate(dir, pos);
    }

    void Start()
    {
        //lavaflow = tilemap.GetComponent<LavaFlow>();
    }
}
