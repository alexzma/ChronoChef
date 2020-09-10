using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject rowOne;
    public GameObject rowTwo;

    private struct InventoryItem
    {
        public GameObject item;
        public bool empty;
        public string name;
    }
    private List<InventoryItem> inventoryList1;
    private List<InventoryItem> inventoryList2;

    // Start is called before the first frame update
    void Start()
    {
        //add items of list1
        inventoryList1 = new List<InventoryItem>();
        foreach(Transform child in rowOne.transform)
        {
            InventoryItem item;
            item.item = child.gameObject;
            item.empty = true;
            item.name = "Empty";
            inventoryList1.Add(item);
        }

        //add items of list2
        inventoryList2 = new List<InventoryItem>();
        foreach (Transform child in rowTwo.transform)
        {
            InventoryItem item;
            item.item = child.gameObject;
            item.empty = true;
            item.name = "Empty";
            inventoryList2.Add(item);
        }

        //AddItem("Bonito Flakes");
        //AddItem("Pork Belly");
        //AddItem("Bonito Flakes");
        //AddItem("Bonito Flakes");
        //AddItem("Bonito Flakes");
        //AddItem("Bonito Flakes");
    }

    // Update is called once per frame
    void Update()
    {}

    public void AddItem(string name)
    {
        //add item to inventory in empty spot
        for(int i = 0; i < inventoryList1.Count; i++)
        {
            if (inventoryList1[i].empty)
            {
                InventoryItem temp;
                temp.item = inventoryList1[i].item;
                temp.empty = false;
                temp.name = name;
                inventoryList1[i] = temp;
                InventoryItemManager itemManager = temp.item.GetComponent<InventoryItemManager>();
                itemManager.SetText(name);
                //TODO: change image displayed
                return;
            }
        }
        for (int i = 0; i < inventoryList2.Count; i++)
        {
            if (inventoryList2[i].empty)
            {
                InventoryItem temp;
                temp.item = inventoryList2[i].item;
                temp.empty = false;
                temp.name = name;
                inventoryList2[i] = temp;
                InventoryItemManager itemManager = temp.item.GetComponent<InventoryItemManager>();
                itemManager.SetText(name);
                //TODO: change image displayed
                return;
            }
        }
        Debug.Log("Inventory full");
    }

    public void RemoveItem(string name)
    {
        //remove item from inventory, making that spot empty
        for (int i = 0; i < inventoryList1.Count; i++)
        {
            if (inventoryList1[i].name == name)
            {
                InventoryItem temp;
                temp.item = inventoryList1[i].item;
                temp.empty = true;
                temp.name = "Empty";
                inventoryList1[i] = temp;
                InventoryItemManager itemManager = temp.item.GetComponent<InventoryItemManager>();
                itemManager.SetText("Empty");
                //TODO: change image displayed
                return;
            }
        }
        for (int i = 0; i < inventoryList2.Count; i++)
        {
            if (inventoryList2[i].name == name)
            {
                InventoryItem temp;
                temp.item = inventoryList2[i].item;
                temp.empty = true;
                temp.name = "Empty";
                inventoryList2[i] = temp;
                InventoryItemManager itemManager = temp.item.GetComponent<InventoryItemManager>();
                itemManager.SetText("Empty");
                //TODO: change image displayed
                return;
            }
        }
        Debug.Log("Item not found");
    }
}
