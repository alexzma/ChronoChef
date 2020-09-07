using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public int timeModifier;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        Debug.Log(collision.gameObject.tag);
        if(collision.CompareTag("Chrono"))
        {
            ChronoObject chrono = collision.GetComponentInParent<ChronoObject>();
            chrono.ChangeTimeState(timeModifier);
            Destroy(gameObject);
        }
    }
}
