﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEWetPaddy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit shit");
        Debug.Log(collision.gameObject.tag + "hit");
        if (collision.gameObject.tag == "Player") return;
        if (collision.gameObject.tag == "water")
        {
            Debug.Log("Hit scan");
            Destroy(collision.gameObject);
        }
    }
}
