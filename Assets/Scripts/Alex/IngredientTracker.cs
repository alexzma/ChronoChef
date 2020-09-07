using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTracker : MonoBehaviour
{
    public GameObject foodItem;
    public GameObject ingredientList;

    private struct Ingredient
    {
        public GameObject display;
        public bool status;
        public string name;
    }

    private List<Ingredient> ingredients;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<Ingredient>();

        //Test add
        AddIngredient("bambooshoot");
        AddIngredient("porkbelly");
        AddIngredient("soysauce");
        AddIngredient("bonitoflakes");
        AddIngredient("ricenoodles");
        AddIngredient("egg");
        AddIngredient("nori");
        AddIngredient("fishcake");

        //Test verify
        //VerifyIngredient("Pork Belly");
        //VerifyIngredient("Bonito Flakes");

        /*
        //Test isVerified
        Debug.Log(IsVerified("Egg"));
        Debug.Log(IsVerified("Bonito Flakes"));
        */
    }

    // Update is called once per frame
    void Update()
    {}

    public void AddIngredient(string name)
    {
        //create object
        GameObject gameObject = Instantiate(foodItem, ingredientList.transform);

        //change the name of the created object
        FoodItemManager foodItemManager = gameObject.GetComponent<FoodItemManager>();
        foodItemManager.SetText(name);

        //add to list
        Ingredient ingredient;
        ingredient.display = gameObject;
        ingredient.status = false;
        ingredient.name = name;
        ingredients.Add(ingredient);
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
}
