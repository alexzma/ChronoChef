using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombTracker : MonoBehaviour
{
    private int num_bombs = 5;
    public Text m_text;
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        Set_num(num_bombs);
    }

    // Update is called once per frame
    void Update()
    {}

    public int Get_num()
    {
        return num_bombs;
    }

    public void Set_num(int num)
    {
        num_bombs = num;
        if (num_bombs < 0)
        {
            num_bombs = 0;
        }
        m_text.text = num_bombs.ToString() + " " + type;
    }

    public void Add_num(int num)
    {
        num_bombs += num;
        m_text.text = num_bombs.ToString() + " " + type;
    }

    public void Subtract_num(int num)
    {
        num_bombs -= num;
        if(num_bombs < 0)
        {
            num_bombs = 0;
        }
        m_text.text = num_bombs.ToString() + " " + type;
    }
}
