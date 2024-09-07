using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using TMPro;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.UI;

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

    
    [SerializeField, Header("Debugging and refrences")] private List<Ingredient> currentDrink = new List<Ingredient>();

    //sweet is x, sour is y, spicy is z
    [SerializeField] private Vector3 currentOrder;
    [SerializeField] private Vector3 currentDrinkStats;

    [SerializeField] private GameObject sweetText;
    [SerializeField] private GameObject sourText;
    [SerializeField] private GameObject saltyText;
    
    // Start is called before the first frame update
    private void Awake()
    {
        currentDrink.Clear();
        UpdateText();
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
            
            currentDrinkStats += new Vector3(possibleIngredients[IngredientEnumNumber].getSweet(),
                possibleIngredients[IngredientEnumNumber].getSour(), 
                possibleIngredients[IngredientEnumNumber].getSpicy());
            
            UpdateText();
        }
    }
    private void UpdateText()
    {
        sweetText.GetComponent<TMP_Text>().text = currentDrinkStats.x.ToString();
        sourText.GetComponent<TMP_Text>().text = currentDrinkStats.y.ToString();
        saltyText.GetComponent<TMP_Text>().text = currentDrinkStats.z.ToString();
    }
    public void CompleteDrink()
    {
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
