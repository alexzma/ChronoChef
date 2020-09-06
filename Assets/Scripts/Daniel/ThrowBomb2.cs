using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb2 : MonoBehaviour
{
    [SerializeField]
    private GameObject slowbombPrefab;
    [SerializeField]
    private GameObject fastbombPrefab;
    [SerializeField]
    private BombsManager bombsManager;

    private Movement move;
    private PickUpDown pick;
    private bool bombSelector = true; // Default slow bomb
    private bool bombHeld;
    public bool BombHeld { get { return bombHeld; } }
    public bool BombSelector { get { return bombSelector; } }
    private int swap;
    private GameObject bomb;
    private float bombSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        move = GetComponent<Movement>();
        pick = GetComponent<PickUpDown>();
        bombHeld = false;
        swap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (move.RequestFreeze())
            {
                if (!bombHeld && pick.SpawnBomb())
                {
                    //bombsManager.SetSelected(bombSelector);
                    bombHeld = true;
                    bomb = Instantiate(SelectBomb(bombSelector), move.transform.position, Quaternion.identity);
                    pick.SetPayload(bomb);
                    swap++;
                }
                else if (bombHeld)
                {
                    Destroy(bomb);
                    bombSelector = !bombSelector;
                    if (swap > 1)
                    {
                        pick.TossBomb();
                        bombHeld = false;
                        bomb = null;
                        swap = 0;
                    }
                    else
                    {
                        bomb = Instantiate(SelectBomb(bombSelector), move.transform.position, Quaternion.identity);
                        pick.SetPayload(bomb);
                        swap++;
                    }
                }
                move.ReleaseFreeze();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (bombsManager.GetSelected() > 0)
            //{
            //    Shoot();
            //    bombsManager.SubtractSelected(1);
            //}

            if (bombHeld && move.RequestFreeze())
            {
                pick.TossBomb();
                bombHeld = false;
                bomb.GetComponent<BombFlight2>().target = move.transform.position + move.DirectionToVector(move.FaceDirection);
                bombSpeed = bomb.GetComponent<BombFlight2>().Speed;
                bomb.GetComponent<BombFlight2>().Fly();
                bomb = null;
                swap = 0;
                Invoke("CallReleaseFreeze", 1f / bombSpeed);
            }
        }
    }

    private GameObject SelectBomb(bool select)
    {
        if (select)
            return slowbombPrefab;
        else
            return fastbombPrefab;
    }

    private void CallReleaseFreeze()
    {
        move.ReleaseFreeze();
    }
}
