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
    private GameObject bomb;

    // Start is called before the first frame update
    private void Start()
    {
        move = GetComponent<Movement>();
        pick = GetComponent<PickUpDown>();
        bombHeld = false;
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
                    bomb = Instantiate(SelectBomb(bombSelector), move.transform.position, Quaternion.identity);
                    bombHeld = true;
                    pick.SetPayload(bomb);
                }
                else if (bombHeld)
                {
                    bombSelector = !bombSelector;
                    //bombsManager.SetSelected(bombSelector);
                    Destroy(bomb);
                    bomb = Instantiate(SelectBomb(bombSelector), move.transform.position, Quaternion.identity);
                    pick.SetPayload(bomb);
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

            if (move.RequestFreeze())
            {
                pick.TossBomb();
                bombHeld = false;
                bomb.GetComponent<BombFlight2>().Fly();
                bomb = null;
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
}
