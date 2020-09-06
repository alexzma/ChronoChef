using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private Transform transform;
    // Start is called before the first frame update
    void Awake()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            // Since we are either facing the x or y direction, the forward vector only has one component with a non-zero value
            Vector3 forward = GetComponent<Transform>().right;
            // Debug.Log("Direction: " + Vector3.Dot(tilemap.cellSize, forward));
            // Take into the account cell size
            float cellDistance = Mathf.Abs(Vector3.Dot(tilemap.cellSize, forward));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, forward, cellDistance, LayerMask.GetMask("items"));
            if (hit.collider != null)
            {
                Debug.Log("pick");
            } else
            {
                Debug.Log("no pick, sadge");
            }
        }
    }
}
