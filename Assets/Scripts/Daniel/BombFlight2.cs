using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFlight2 : MonoBehaviour
{
    [HideInInspector]
    public Vector3 target;
    public int timeModifier;

    private float speed = 10f;
    public float Speed { get { return speed; } }
    private bool flying;
    private ChronoTileManager ctm;

    void Start()
    {
        ctm = GameObject.Find("ChronoTileManager").GetComponent<ChronoTileManager>();
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
        Debug.Log("Explode at " + target + ", dir " + timeModifier);
        ctm.activateChrono(timeModifier, target);
        Destroy(gameObject);
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChronoObject chronoObject;
        if (!collision.transform.parent.TryGetComponent(out chronoObject))
        {
            Explode();
            return;
        }
        else
        {
            Debug.Log("Bomb hit: " + flying + "; " + chronoObject.name);
            if (flying && chronoObject != null)
            {
                Debug.Log("Hit");
                chronoObject.ChangeTimeState(timeModifier);
                Explode();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            return;
        ChronoObject chronoObject = collision.gameObject.GetComponentInParent<ChronoObject>();
        Debug.Log("Bomb hit: " + flying + "; " + chronoObject.name);
        if (flying && chronoObject != null)
        {
            Debug.Log("Hit");
            chronoObject.ChangeTimeState(timeModifier);
            Explode();
        }
    }
}
