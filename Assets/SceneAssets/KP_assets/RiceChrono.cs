using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RiceChrono : MonoBehaviour
{
    [SerializeField] private GameObject[] riceStates =  new GameObject[2];
    public int timeState;
    private int numStates;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private Transform transform;
    void Awake()
    {

    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "RedBomb" && timeState == 0)
        {
            Destroy(gameObject);
            Instantiate(riceStates[1], transform.position, transform.rotation);
        }
    }
}
