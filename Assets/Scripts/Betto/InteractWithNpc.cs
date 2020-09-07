using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNpc : MonoBehaviour
{
    private Transform transform;
    private Movement move;
    public bool isTalkingToNpc;
    // Start is called before the first frame update
    void Awake()
    {
        transform = GetComponent<Transform>();
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check in front, if NPC,
        if (Input.GetKey(KeyCode.E) && !isTalkingToNpc)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, move.DirectionToVector(move.FaceDirection), 1f, LayerMask.GetMask("Obstacle", "Item"));
            NpcBasic npc = hit.collider.GetComponent<NpcBasic>();
            if (hit.collider != null && npc != null)
            {
                isTalkingToNpc = true;
                npc.TalkToNpc();
            }
        }
    }

    public void ResumeActions()
    {
        isTalkingToNpc = false;
    }
}
