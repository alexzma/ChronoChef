using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bombPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() 
    {
        GameObject bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        
    }
}
