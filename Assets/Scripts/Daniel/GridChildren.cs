using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        foreach (Transform tr in transform.GetComponentInChildren<Transform>())
            tr.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(tr.position));
    }

}
