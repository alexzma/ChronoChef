using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexKeyListener : MonoBehaviour
{
    public GameObject menu;
    public GameObject inventory;
    private bool inventory_active = false;
    private bool menu_active = false;

    struct InventoryAction
    {
        public string type;
        public string name;
    }
    List<InventoryAction> inventoryQueue;

    // Start is called before the first frame update
    void Start()
    {
        inventoryQueue = new List<InventoryAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.activeSelf)
        {
            for(int i = 0; i < inventoryQueue.Count; i++)
            {
                if(inventoryQueue[i].type == "Add")
                {
                    InventoryManager invManager = inventory.GetComponent<InventoryManager>();
                    invManager.AddItem(inventoryQueue[i].name);
                } else if(inventoryQueue[i].type == "Remove")
                {
                    InventoryManager invManager = inventory.GetComponent<InventoryManager>();
                    invManager.RemoveItem(inventoryQueue[i].name);
                }
            }
            inventoryQueue = new List<InventoryAction>();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu_active);
            menu_active = !menu_active;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory_active);
            inventory_active = !inventory_active;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(inventory.activeSelf);
            if (inventory.activeSelf)
            {
                InventoryManager invManager = inventory.GetComponent<InventoryManager>();
                invManager.AddItem("Pizza");
                invManager.RemoveItem("Bonito Flakes");
            }
            else
            {
                InventoryAction temp;
                temp.type = "Add";
                temp.name = "Pizza";
                inventoryQueue.Add(temp);
                InventoryAction temp2;
                temp2.type = "Remove";
                temp2.name = "Bonito Flakes";
                inventoryQueue.Add(temp2);
            }
        }
    }
}
