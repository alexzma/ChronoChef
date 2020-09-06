using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KP_pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        KP_player_manager manager = collision.GetComponent<KP_player_manager>();
        if(manager)
        {
            manager.PickupBomb();
        }
    }
}
