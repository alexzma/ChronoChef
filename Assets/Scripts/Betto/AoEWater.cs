using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEWater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag == "water")
        {
           
            Destroy(collision.gameObject);
        }
    }
}
