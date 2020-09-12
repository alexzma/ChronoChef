using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb2 : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private GameObject slowbombPrefab;
    [SerializeField]
    private GameObject fastbombPrefab;
    [SerializeField]
    private BombsManager bombsManager;

    private Movement move;
    private PickUpDown pick;
    private bool bombSelector = true; // Default slow bomb
    public bool BombHeld { get { return bombHeld; } }
    private bool bombHeld;
    public bool BombSelector { get { return bombSelector; } }
    private bool startingBomb;
    private GameObject bomb;
    private float bombSpeed;
    private float delay = 0.2f;
    #endregion

    #region Start/Update
    // Start is called before the first frame update
    private void Start()
    {
        move = GetComponent<Movement>();
        pick = GetComponent<PickUpDown>();
        bombHeld = false;
        bombsManager.SetSelected(bombSelector);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (move.RequestFreeze())
            {
                if (!bombHeld && bombsManager.GetSelected() > 0 && pick.SpawnBomb())
                {
                    bombHeld = true;
                    bomb = Instantiate(SelectBomb(bombSelector), move.transform.position, Quaternion.identity);
                    pick.SetPayload(bomb);
                    bombsManager.SubtractSelected(1);
                    startingBomb = bombSelector;
                }
                else if (!bombHeld && bombsManager.GetSelected() < 1 && bombsManager.GetNonSelected() > 0 && pick.SpawnBomb())
                {
                    bombSelector = !bombSelector;
                    bombsManager.ToggleSelected();

                    bombHeld = true;
                    bomb = Instantiate(SelectBomb(bombSelector), move.transform.position, Quaternion.identity);
                    pick.SetPayload(bomb);
                    bombsManager.SubtractSelected(1);
                    startingBomb = bombSelector;
                }
                else if (bombHeld)
                {
                    bombsManager.AddSelected(1);
                    Destroy(bomb);

                    if (bombSelector != startingBomb || bombsManager.GetNonSelected() < 1)
                    {
                        pick.TossBomb();
                        bombHeld = false;
                        bomb = null;
                    }
                    else
                    {
                        bombSelector = !bombSelector;
                        bombsManager.ToggleSelected();
                        bombsManager.SubtractSelected(1);
                        bomb = Instantiate(SelectBomb(bombSelector), move.transform.position, Quaternion.identity);
                        pick.SetPayload(bomb);
                    }
                }
                Invoke("CallReleaseFreeze", delay);
            }
        }
        else if (Input.GetKeyDown("space"))
        {
            if (bombHeld && move.RequestFreeze())
            {
                pick.TossBomb();
                bombHeld = false;
                bomb.GetComponent<BombFlight2>().target = move.transform.position + move.DirectionToVector(move.FaceDirection);
                bombSpeed = bomb.GetComponent<BombFlight2>().Speed;
                bomb.GetComponent<BombFlight2>().Fly();
                bomb = null;
                Invoke("CallReleaseFreeze", delay);
            }
        }
    }
    #endregion

    #region Private Functions
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

    public void RefillBombs(int refill)
    {
        bombsManager.AddFuture(refill);
        bombsManager.AddPast(refill);
    }
    #endregion
}
