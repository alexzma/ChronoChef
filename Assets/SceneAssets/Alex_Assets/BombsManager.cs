using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombsManager : MonoBehaviour
{
    public Image pastImage;
    public Image futureImage;

    public Material unselectedMaterial;
    public Material selectedMaterial;

    private bool pastSelected = true;

    public BombTracker pastTracker;
    public BombTracker futureTracker;

    // Start is called before the first frame update
    void Start()
    {
        if (pastSelected)
        {
            pastImage.material = selectedMaterial;
            futureImage.material = unselectedMaterial;
        }
        else
        {
            pastImage.material = unselectedMaterial;
            futureImage.material = selectedMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {}

    public void SetSelected(bool isPast)
    {
        pastSelected = isPast;
        if (pastSelected)
        {
            pastImage.material = selectedMaterial;
            futureImage.material = unselectedMaterial;
        }
        else
        {
            pastImage.material = unselectedMaterial;
            futureImage.material = selectedMaterial;
        }
    }

    public void ToggleSelected()
    {
        pastSelected = !pastSelected;
        if (pastSelected)
        {
            pastImage.material = selectedMaterial;
            futureImage.material = unselectedMaterial;
        } else
        {
            pastImage.material = unselectedMaterial;
            futureImage.material = selectedMaterial;
        }
    }

    public int GetSelected()
    {
        if (pastSelected)
        {
            return pastTracker.Get_num();
        } else
        {
            return futureTracker.Get_num();
        }
    }

    public void AddSelected(int num)
    {
        if (pastSelected)
        {
            pastTracker.Add_num(num);
        } else
        {
            futureTracker.Add_num(num);
        }
    }

    public void SetSelected(int num)
    {
        if (pastSelected)
        {
            pastTracker.Set_num(num);
        } else
        {
            futureTracker.Set_num(num);
        }
    }

    public void SubtractSelected(int num)
    {
        if (pastSelected)
        {
            pastTracker.Subtract_num(num);
        } else
        {
            futureTracker.Subtract_num(num);
        }
    }

    public int GetPast()
    {
        return pastTracker.Get_num();
    }

    public void AddPast(int num)
    {
        pastTracker.Add_num(num);
    }

    public void SetPast(int num)
    {
        pastTracker.Set_num(num);
    }

    public void SubtractPast(int num)
    {
        pastTracker.Subtract_num(num);
    }

    public int GetFuture()
    {
        return futureTracker.Get_num();
    }

    public void AddFuture(int num)
    {
        futureTracker.Add_num(num);
    }

    public void SetFuture(int num)
    {
        futureTracker.Set_num(num);
    }

    public void SubtractFuture(int num)
    {
        futureTracker.Subtract_num(num);
    }
}
