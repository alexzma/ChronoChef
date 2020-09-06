using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomManager : MonoBehaviour
{

    public GameObject virtualCam;
    private PolygonCollider2D col;

    void Start()
    {
        //virtualCam.SetActive(false);
        //col = gameObject.GetComponent("PolygonCollider2D");
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
        }
        Debug.Log("Collided with object: %s", other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
        }
        Debug.Log("Collided with object: %s", other);
    }

}
