using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IngredientTracker : MonoBehaviour
{
    public GameObject foodItem;
    public GameObject ingredientList;
    // Required ingredients for all the rooms
    public RoomRequiredIngredients[] requiredIngredientNamesExt;
    public string initialRoom;

    private Dictionary<string, List<Ingredient>> roomToRequiredIngredients;
    private Dictionary<string, RectTransform> roomToListTransform;

    private struct Ingredient
    {
        public GameObject display;
        public bool status;
        public string name;
    }

    // The only reason for this class is to display the Dictionary easily in the inspector for easy editing.
    [Serializable]
    public struct RoomRequiredIngredients
    {
        public string room;
        public string[] ingredientNames;
    }

    private List<Ingredient> ingredients;
    private string activeRoom;

    private void Awake()
    {
        roomToRequiredIngredients = new Dictionary<string, List<Ingredient>>();
        roomToListTransform = new Dictionary<string, RectTransform>();
        activeRoom = initialRoom;
    }

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<Ingredient>();

        foreach (RoomRequiredIngredients reqIngredient in requiredIngredientNamesExt)
        {
            foreach (string ingredientName in reqIngredient.ingredientNames)
            {
                AddIngredient(reqIngredient.room, ingredientName);
            }
        }

        //Test add
        //AddIngredient("", "Bonito Flakes");
        //AddIngredient("", "Egg");
        //AddIngredient("", "Green Onions");
        //AddIngredient("", "Bamboo Shoots");
        //AddIngredient("", "Pork Belly");
        DisplayRoom(activeRoom); // Test, would usually call this from an outside class obtaining this tracker component

        //Test verify
        VerifyIngredient("Pork Belly");
        VerifyIngredient("Bonito Flakes");

        /*
        //Test isVerified
        Debug.Log(IsVerified("Egg"));
        Debug.Log(IsVerified("Bonito Flakes"));
        */
    }

    // Update is called once per frame
    void Update()
    {}

    public void AddIngredient(string room, string name)
    {
        if (!roomToListTransform.ContainsKey(room))
        {
            GameObject obj = new GameObject();
            GameObject newObj = Instantiate(obj, ingredientList.transform);
            newObj.AddComponent<VerticalLayoutGroup>();
            newObj.AddComponent<RectTransform>();
            newObj.transform.parent = transform;
            newObj.SetActive(false);
            roomToListTransform.Add(room, newObj.GetComponent<RectTransform>());
        }
        Transform parentTransform = roomToListTransform[room];
        //create object
        GameObject gameObject = Instantiate(foodItem, parentTransform);

        //change the name of the created object
        FoodItemManager foodItemManager = gameObject.GetComponent<FoodItemManager>();
        foodItemManager.SetText(name);

        //add to list
        Ingredient ingredient;
        ingredient.display = gameObject;
        ingredient.status = false;
        ingredient.name = name;
        ingredients.Add(ingredient);
        if (!roomToRequiredIngredients.ContainsKey(room))
        {
            roomToRequiredIngredients.Add(room, new List<Ingredient>());
        }
        roomToRequiredIngredients[room].Add(ingredient);
    }

    public void VerifyIngredient(string name)
    {
        for(int i = 0; i < ingredients.Count; i++)
        {
            if(ingredients[i].name == name)
            {
                //change status
                Ingredient temp = ingredients[i];
                temp.status = true;
                ingredients[i] = temp;

                //change image
                FoodItemManager foodItemManager = temp.display.GetComponent<FoodItemManager>();
                foodItemManager.Validate();
                return;
            }
        }
        Debug.Log("Ingredient not found: " + name);
    }

    public bool IsVerified(string name)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].name == name)
            {
                return ingredients[i].status;
            }
        }
        Debug.Log("Ingredient not found: " + name);
        return false;
    }

    public void DisplayRoom(string room)
    {
        if (roomToListTransform.ContainsKey(room))
        {
            Debug.Log(activeRoom);
            roomToListTransform[activeRoom].gameObject.SetActive(false);
            roomToListTransform[room].gameObject.SetActive(true);
            Debug.Log("enabled");
        }
    }

    public float CalculatePercentageDone()
    {
        int amountVerified = 0;
        foreach(Ingredient ingredient in ingredients) {
            if (ingredient.status)
            {
                // Is verified
                amountVerified++;
            }
        }
        return (float)amountVerified / ingredients.Count;
    }
}
