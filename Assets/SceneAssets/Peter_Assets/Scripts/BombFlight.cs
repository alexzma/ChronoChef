using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombFlight : MonoBehaviour
{
    private GameObject player;
    private Vector3 target;
    private Vector3 start_pos;
    public float speed = 1.0f;

    void Start()
    {
        start_pos = transform.position;
        player = GameObject.Find("Player"); // Set target position to the player
        target = player.transform.position;
        int dir = player.GetComponent<Movement>().FaceDirection;
        
        switch(dir) 
        {
            case 0: 
               target += Vector3Int.up;
                break;
            case 1: 
                target += Vector3Int.right;
                break;
            case 2:
                target += Vector3Int.down;
                break;
            case 3: 
                target += Vector3Int.left;
                break;
        }


        //Tilemap tilemap = transform.parent.GetComponent<Tilemap>();
        //Vector3Int cellPosition = tilemap.WorldToCell(target); // Get the updated target position
        //Vector3 target_grid_position = tilemap.GetCellCenterWorld(cellPosition); // target position on the grid
        //target = target_grid_position;


    }

    // Update is called once per frame
    void Update()
    {
        float step  = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        Debug.Log(target);
    }
}
