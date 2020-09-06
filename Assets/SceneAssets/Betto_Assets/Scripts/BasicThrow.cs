using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicThrow : MonoBehaviour
{
    public Transform bomb;
    private void Update()
    {
        Instantiate(bomb);
    }
}
