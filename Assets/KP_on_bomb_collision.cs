using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KP_on_collision : MonoBehaviour
{
    void Start()
    {
        
    }

    //attached to 
    void OnCollisionEnter(Collision other)
    {
        if(gameObject.tag == "redBomb" && 
            other.gameObject.tag == "item" )
        {
            Destroy(gameObject);
            Destroy(other);
            //spawn future instance of gameObject
        }

        if (gameObject.tag == "blueBomb" &&
             other.gameObject.tag == "item")
        {
            Destroy(gameObject);
            Destroy(other);
            //spawn past instance of gameObject
        }
    }
}
