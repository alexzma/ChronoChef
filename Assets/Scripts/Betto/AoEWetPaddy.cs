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
        for (int i = 0; i < 5; i++)
        {
            if (i == 4)
            {
                hit = Physics2D.Raycast(transform.position, Vector3.up, 0.1f, LayerMask.GetMask("Puddle"));
                if (hit.collider != null && hit.collider.CompareTag("water"))
                    Destroy(hit.collider.gameObject);
                continue;
            }
            hit = Physics2D.Raycast(transform.position + move.DirectionToVector(i), move.DirectionToVector(i), 0.1f, LayerMask.GetMask("Puddle"));
            if (hit.collider != null && hit.collider.CompareTag("water"))
                Destroy(hit.collider.gameObject);
        }
    }
}
