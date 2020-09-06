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
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
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
                Debug.Log(hit.collider.name);
                // This is for testing a chronobomb interaction example
                if (hit.collider.CompareTag("chrono"))
                {
                    Debug.Log("Hit a chrono object");
                    // This is the example of forwarding time by 1 state (Must be parent!)
                    hit.collider.GetComponentInParent<ChronoObject>().ChangeTimeState(1);
                }
            } else
            {
                Debug.Log("no pick, sadge");
            }
        }
    }
}
