using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomManager : MonoBehaviour
{

    public GameObject virtualCam;
    public string roomName;
    public IngredientTracker ingredientTracker;
    private PolygonCollider2D col;

    void Start()
    {
        virtualCam.SetActive(false);
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
        }
        //Debug.Log("Collided with object: %s", other);
        ingredientTracker.DisplayRoom(roomName);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
        }
        //Debug.Log("Collided with object: %s", other);
    }

}
