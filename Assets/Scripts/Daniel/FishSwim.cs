using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FishSwim : MonoBehaviour
{
    private Vector3 target;
    private bool readyToMove;

    [SerializeField]
    private Tilemap tilemap;
    public Sprite water;
    private float speed = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        readyToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == target)
            readyToMove = true;
        else if (readyToMove)
            return;
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sprite temp = tilemap.GetSprite(tilemap.WorldToCell(collision.transform.position));
        if (collision.name.ToLower() == "seaweed" && temp != null)
        {
            target = collision.transform.position;
            readyToMove = false;
            Debug.Log("target set to " + target);
        }
    }
}
