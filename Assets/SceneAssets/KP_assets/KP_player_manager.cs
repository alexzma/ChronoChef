using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KP_player_manager : MonoBehaviour
{
    public int bombCount = 0;
    public void PickupBomb()
    {
        bombCount++;
    }
}
