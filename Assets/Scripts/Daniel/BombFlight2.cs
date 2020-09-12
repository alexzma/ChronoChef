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
    void Update()
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
        //Debug.Log("Explode at " + target + ", dir " + timeModifier);
        if (ctm != null)
            ctm.activateChrono(timeModifier, target);
        Destroy(gameObject);
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("room") || !flying)
            return;
        //ChronoObject chronoObject;
        if (!collision.transform.parent.TryGetComponent(out ChronoObject chronoObject))
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            chronoObject.ChangeTimeState(timeModifier);
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            return;
        bool temp = collision.transform.parent.TryGetComponent(out ChronoObject chronoObject);
        //Debug.Log("Bomb hit: " + flying + "; " + chronoObject.name);
        if (flying && temp)
        {
            //Debug.Log("Hit");
            chronoObject.ChangeTimeState(timeModifier);
            Explode();
        }
    }
}
