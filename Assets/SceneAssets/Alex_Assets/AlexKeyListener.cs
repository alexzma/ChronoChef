using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexKeyListener : MonoBehaviour
{
    public GameObject inventory;
    private bool inventory_active = false;

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory_active);
            inventory_active = !inventory_active;
        }
    }
}
