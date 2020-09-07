using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFlight2 : MonoBehaviour
{
    [HideInInspector]
    public Vector3 target;

    private float speed = 10f;
    public float Speed { get { return speed; } }
    private bool flying;

    void Start()
    {
        flying = false;
        if (target == null)
            Debug.Log("BombFlight2: Target is null");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flying)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (transform.position == target)
            {
                flying = false;
                Explode();
            }
        }
    }

    public void Fly()
    {
        flying = true;
    }

    private void Explode()
    {
        Debug.Log("Explode!!");
        return;
    }
}
