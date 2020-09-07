using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombFlight : MonoBehaviour
{
    private GameObject player;
    private Vector3 target;
    public float speed = 1.0f;
    public int timeModifier = 0;

    void Start()
    {
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

    }

    // Update is called once per frame
    void Update()
    {
        float step  = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        // Debug.Log(target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        Debug.Log(collision.tag);
        if (collision.CompareTag("chrono"))
        {
            ChronoObject chrono = collision.GetComponentInParent<ChronoObject>();
            chrono.ChangeTimeState(timeModifier);
            Destroy(gameObject);
        }
    }
}
