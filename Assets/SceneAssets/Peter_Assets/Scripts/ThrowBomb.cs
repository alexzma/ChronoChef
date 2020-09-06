using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject slowbombPrefab;
    public GameObject fastbombPrefab;
    private int slowBomb = 1; // Default slow bomb
    public int BombToggle {get { return slowBomb; }}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            slowBomb = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            slowBomb = 0;
        }

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() 
    {
        if (slowBomb == 1)
        {
            GameObject bomb = Instantiate(slowbombPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            GameObject bomb = Instantiate(fastbombPrefab, firePoint.position, firePoint.rotation);
        }
        
    }
}
