using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DrinkMenuManager : MonoBehaviour
{
    [HideInInspector, System.Serializable] public enum IngredientTypes
    {
        Ectoplasm,
        SpoiledCheese,
        EyeOfNewt,
        OrangeZest,
        CatPiss,
        Vodka
    }
    
    [SerializeField, Header("Enter all the possible ingredients here"), 
        Tooltip("Enum is ingredient name, then add the ingredient's stats into the three digits")] 
    private List<Ingredient> possibleIngredients = new List<Ingredient>();
    
    [SerializeField] private List<Ingredient> currentDrink = new List<Ingredient>();

    //sweet is x, sour is y, spicy is z
    [SerializeField] private Vector3 currentOrder;
    [SerializeField] private Vector3 currentDrinkStats;

    // Start is called before the first frame update
    private void Awake()
    {
        currentDrink.Clear();
    }

    public void AddIngredient (int IngredientEnumNumber)
    {
        if (currentDrink.Count < 5)
        {
            int count = 0;
            while (count < currentDrink.Count)
            {
                if (IngredientEnumNumber >= (int)currentDrink[count].getIngType())
                {
                    break;
                }
                count++;
            }
            currentDrink.Insert(count, possibleIngredients[IngredientEnumNumber]);
            UpdateText();
        }

    }

    private void UpdateText()
    {

    }
    public void CompleteDrink()
    {
        foreach (Ingredient ing in currentDrink) {
            currentDrinkStats += new Vector3(ing.getSweet(), ing.getSour(), ing.getSpicy());
        }
        if (currentDrinkStats.x < 0)
        {
            currentDrinkStats.x = 0;
        }
        if (currentDrinkStats.y < 0)
        {
            currentDrinkStats.y = 0;
        }
        if (currentDrinkStats.z < 0)
        {
            currentDrinkStats.z = 0;
        }

        if (currentDrinkStats == currentOrder)
        {
            Debug.Log("order complete");
        }
        else
        {
            Debug.Log("fuck you");
        }

    }
    public void CloseMenu()
    {

    }
}
