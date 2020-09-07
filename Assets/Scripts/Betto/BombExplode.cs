using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public int timeModifier;
    private void Start()
    {
        Destroy(this, 1); // 1 second till EXPLOSION
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChronoObject chronoObject = collision.GetComponentInParent<ChronoObject>();
        if (chronoObject != null)
        {
            chronoObject.ChangeTimeState(timeModifier);
        }
    }
}
