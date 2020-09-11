using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDown : MonoBehaviour
{
    #region Private Variables
    private Movement move;
    private bool carrying;
    public bool Carrying { get { return carrying;  } }
    private GameObject payload;
    private ThrowBomb2 throwbomb2;
    private IngredientTracker ingredientTracker;
    private ThrowBomb2 throwBomb;
    private List<string> ingredients;
    #endregion

    #region Start/Update
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        carrying = false;
        payload = null;
        throwbomb2 = GetComponent<ThrowBomb2>();
        ingredientTracker = GameObject.FindObjectOfType<IngredientTracker>();
        ingredients = new List<string>{ "bambooshoot", "porkbelly", "soysauce", "bonitoflakes", "ricenoodles", "egg", "nori", "fishcake" };
        throwBomb = GetComponent<ThrowBomb2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(move.transform.position, move.DirectionToVector(move.FaceDirection), 1f, LayerMask.GetMask("NPC"));
            if (hit.collider != null && hit.collider.GetComponent<NpcBasic>() != null)
            {
                move.RequestFreeze();
                Debug.Log("Hit a NPC");
                hit.collider.GetComponent<NpcBasic>().TalkToNpc();
                //move.ReleaseFreeze();
                return;
            }
            else if (!carrying)
            {
                if (move.RequestFreeze())
                    Pickup();
            }
            else
            {
                if (!throwbomb2.BombHeld && move.RequestFreeze())
                    PutDown();
                return;
            }
        }

        if (carrying)
            payload.transform.position = move.transform.position;
        // Rotate the payload to match the player
        //if (carrying)
        //    payload.transform.Rotate(rotationAmount, Space.Self);
    }
    #endregion

    #region Public Functions
    public bool SpawnBomb()
    {
        if (carrying)
            return false;
        else
        {
            carrying = true;
            return true;
        }
    }

    public void TossBomb()
    {
        carrying = false;
        payload = null;
    }

    public void SetPayload(GameObject thing)
    {
        payload = thing;
    }

    public void ResumeActionsAfterTalkingFromNpc()
    {
        Debug.Log("Release freeze on NPC interaction");
        move.ReleaseFreeze();
    }
    #endregion

    #region Private Functions
    private bool Pickup()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, move.DirectionToVector(move.FaceDirection), 1f, LayerMask.GetMask("Item"));
        if (hit.collider == null)
        {
            move.ReleaseFreeze();
            return false;
        }
        StartCoroutine(PickupHelper(hit));

        return true;
    }

    private IEnumerator PickupHelper(RaycastHit2D hit)
    {
        carrying = true;
        payload = hit.collider.gameObject;
        payload.tag = "static";

        Vector3 startPos = hit.collider.transform.position;
        hit.collider.enabled = false;
        float t = 0;

        while (t < 1)
        {
            yield return new WaitForFixedUpdate();
            t += Time.deltaTime / 0.5f;
            hit.collider.transform.position = Vector3.Lerp(startPos, transform.position, t);
        }

        if (CheckIngredient())
        {
            carrying = false;
            Destroy(payload);
            payload = null;
        }

        move.ReleaseFreeze();
    }

    private bool PutDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, move.DirectionToVector(move.FaceDirection), 1f, LayerMask.GetMask("Obstacle", "Item"));
        if (hit.collider != null)
        {
            move.ReleaseFreeze();
            return false;
        }
        StartCoroutine(PutDownHelper());

        return true;
    }

    private IEnumerator PutDownHelper()
    {
        Vector3 startPos = payload.transform.position;
        float t = 0;

        while (t < 1)
        {
            yield return new WaitForFixedUpdate();
            t += Time.deltaTime / 0.5f;
            Vector3 destination = Vector3.Lerp(startPos, startPos + move.DirectionToVector(move.FaceDirection), t);
            if (payload.GetComponentInParent<ChronoObject>())
            {
                // Since the ChronoObject spawns new entities relative to parent position, the parent transform should be changed as well
                payload.transform.parent.position = destination;
            }
            payload.transform.position = destination;
        }

        payload.transform.Rotate(-payload.transform.rotation.eulerAngles);
        payload.GetComponent<BoxCollider2D>().enabled = true;
        carrying = false;
        payload = null;

        move.ReleaseFreeze();
    }

    private bool CheckIngredient()
    {
        string editedName = payload.name.Split(' ')[0].ToLower();
        if (ingredients.Contains(editedName))
        {
            if (!ingredientTracker.IsVerified(payload.name))
            {
                ingredientTracker.VerifyIngredient(payload.name);
                return true;
            }
        }
        else if (editedName == "timeanomaly")
        {
            throwBomb.RefillBombs(5);
            return true;
        }
        
        return false;
    }
    #endregion
}
