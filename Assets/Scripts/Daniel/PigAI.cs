using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PigAI : MonoBehaviour
{
    #region Public Variables
    public bool rotatePig = true;
    #endregion

    #region Private Variables
    private Transform parentTransform;
    private Transform playerTransform;
    private int faceDirection;
    private bool readyToMove;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private float timer;
    private float moveTime = 0.15f;
    #endregion

    #region Start/Update
    // Start is called before the first frame update
    void Start()
    {
        faceDirection = 0;
        parentTransform = transform.parent.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        readyToMove = true;

        // Snap pig to grid
        Tilemap tilemap = GameObject.FindObjectOfType<Tilemap>();
        parentTransform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position));
    }

    private void FixedUpdate()
    {
        if (readyToMove)
            return;
        else if (parentTransform.position == endPoint)
            MovePig(faceDirection);
        else
        {
            timer += Time.deltaTime / moveTime;
            parentTransform.position = Vector3.Lerp(startPoint, endPoint, timer);
        }
    }
    #endregion

    #region Private Variables
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 direction = (playerTransform.position - parentTransform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(parentTransform.position + direction, direction, 3f, LayerMask.GetMask("Player", "Obstacle"));
            if (hit.collider.transform.CompareTag("Player") && readyToMove)
            {
                readyToMove = false;
                Vector3 danger = new Vector3(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y), 0);
                RunAwayFrom(VectorToDirection(danger));
            }
        }
    }

    private void RunAwayFrom(int dangerDir)
    {
        int dangerDir2 = VectorToDirection(DirectionToVector(dangerDir) * -1);
        int movementDirection = 4;
        float distance = 1;

        for (int i = 0; i < 4; i++)
        {
            if (i == dangerDir)
                continue;
            Vector3 dir = DirectionToVector(i);
            RaycastHit2D hit = Physics2D.Raycast(parentTransform.position + dir, dir, 10f, LayerMask.GetMask("Obstacle", "Player", "Boundary"));
            if (hit.collider != null)
            {
                float tempDistance = (new Vector3(hit.point.x, hit.point.y) - parentTransform.position).sqrMagnitude;
                if (tempDistance > distance || (tempDistance == distance && movementDirection == dangerDir2))
                {
                    distance = tempDistance;
                    movementDirection = i;
                }
            }
            else
            {
                distance = 5;
                movementDirection = i;
            }
        }

        if (movementDirection != 4)
            MovePig(movementDirection);
        else
            readyToMove = true;
    }

    private Vector3 DirectionToVector(int direction)
    {
        switch (direction)
        {
            case 0: return Vector3Int.up;
            case 1: return Vector3Int.right;
            case 2: return Vector3Int.down;
            case 3: return Vector3Int.left;
            default:
                Debug.Log("Passed in illegal direction to DirectionToVector: " + direction);
                throw new System.Exception();
        }
    }

    private int VectorToDirection(Vector3 vector)
    {
        if (vector == Vector3Int.up)
            return 0;
        else if (vector == Vector3Int.right)
            return 1;
        else if (vector == Vector3Int.down)
            return 2;
        else if (vector == Vector3Int.left)
            return 3;

        Debug.Log("Passed illegal vector to VectorToDirection: " + vector);
        throw new System.Exception();
    }

    private void MovePig(int direction)
    {
        faceDirection = direction;
        FaceForward();
        if (CheckInFront())
        {
            startPoint = parentTransform.position;
            endPoint = parentTransform.position + DirectionToVector(direction);
            Debug.Log("start point: " + startPoint + "; endPoint: " + endPoint);
            timer = 0;
        }
        else
            readyToMove = true;
    }

    private bool CheckInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(parentTransform.position + DirectionToVector(faceDirection) * 0.6f,
            DirectionToVector(faceDirection), 0.5f, LayerMask.GetMask("Obstacle", "Item", "Player", "Boundary"));
        if (hit.collider != null && !hit.collider.transform.CompareTag("walkable"))
            return false;
        return true;
    }

    private void FaceForward()
    {
        if (!rotatePig)
            return;
        Vector3 rotationAmount = new Vector3(0, 0, -90 * faceDirection) - transform.eulerAngles;
        parentTransform.Rotate(rotationAmount, Space.Self);
    }

    private void OnDisable()
    {
        if (parentTransform.parent != null)
            parentTransform.parent.transform.position = parentTransform.position;
    }
    #endregion
}
