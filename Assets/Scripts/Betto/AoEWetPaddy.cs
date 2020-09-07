using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEWetPaddy : MonoBehaviour
{
    Movement move;
    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<Movement>();
        RaycastHit2D hit;
        for (int i = 0; i < 4; i++)
        {
            hit = Physics2D.Raycast(transform.position + move.DirectionToVector(i), move.DirectionToVector(i), 0.1f, ~LayerMask.GetMask("Player"));
            if (hit.collider != null && hit.collider.CompareTag("water"))
                Destroy(hit.collider.gameObject);
        }
    }
}
