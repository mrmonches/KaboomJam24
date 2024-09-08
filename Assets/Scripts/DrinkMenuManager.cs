using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class DrinkMenuManager : MonoBehaviour
{
    [HideInInspector, System.Serializable] public enum IngredientTypes
    {
        Draculum,
        Orange20,
        Vodka,
        Ectoplasm,
        SpiritJam,
        Ashes

    }
    
    [SerializeField, Header("Enter all the possible ingredients here"), 
        Tooltip("Enum is ingredient name, then add the ingredient's stats into the three digits")] 
    private List<Ingredient> possibleIngredients = new List<Ingredient>();
    [SerializeField, Header("Enter how much money each ingredient is worth here")] private int IngredientProfit;

    
    [SerializeField, Header("Debugging and refrences")] private List<Ingredient> currentDrink = new List<Ingredient>();

    //sweet is x, sour is y, spicy is z
    [SerializeField] private Vector3 currentOrder;
    [SerializeField] private Vector3 currentDrinkStats;

    [SerializeField] private GameObject sweetText;
    [SerializeField] private GameObject sourText;
    [SerializeField] private GameObject saltyText;
    [SerializeField] private List<GameObject> DrinkLayers;

    [SerializeField] private Image customerPortrait;
    [SerializeField] private GameObject orderSweetText;
    [SerializeField] private GameObject orderSourText;
    [SerializeField] private GameObject orderSaltyText;


    private int[] inventory = new int[6];
    [SerializeField] private TMP_Text[] inventoryTexts; 

    [SerializeField] private CustomerController currentCustomer;
    private bool orderComplete = false;

    [SerializeField] private List<Button> buttons;

    [SerializeField] private GameObject drinkCompleteMenu;
    [SerializeField] private TMP_Text flavorText;
    [SerializeField] private TMP_Text drinkNameText;
    [SerializeField] private Image drink;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        currentDrink.Clear();
        for (int i = 0; i < 6; i++)
        {
            inventory[i] = FindObjectOfType<PlayerInventoryController>().InventoryContents[i];
        }
        foreach (GameObject go in DrinkLayers)
        {
            go.SetActive(false);
        }
        UpdateText();

    }

    public void AddIngredient (int IngredientEnumNumber)
    {
        if (currentDrink.Count < 5 && inventory[IngredientEnumNumber] > 0)
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
            /*currentDrinkStats += new Vector3(possibleIngredients[IngredientEnumNumber].getSweet(),
                possibleIngredients[IngredientEnumNumber].getSour(),
                possibleIngredients[IngredientEnumNumber].getSpicy());*/
            inventory[IngredientEnumNumber]--;

            UpdateText();
        }
    }
    public void RemoveIngredient(int positionToRemove)
    {
        inventory[(int)currentDrink[positionToRemove].getIngType()]++;
        currentDrink.RemoveAt(positionToRemove);
        UpdateText();
    }
    private void UpdateText()
    {
        currentDrinkStats = Vector3.zero;
        foreach (Ingredient ing in currentDrink)
        {
            currentDrinkStats += new Vector3(ing.getSweet(), ing.getSour(), ing.getSpicy());
        }

        sweetText.GetComponent<TMP_Text>().text = currentDrinkStats.x.ToString();
        sourText.GetComponent<TMP_Text>().text = currentDrinkStats.y.ToString();
        saltyText.GetComponent<TMP_Text>().text = currentDrinkStats.z.ToString();

        for (int i = 0; i < 6; i++)
        {
            inventoryTexts[i].text = inventory[i].ToString();
        }

        int count = 0;
        foreach (GameObject go in DrinkLayers)
        {
            if (count < currentDrink.Count)
            {
                go.SetActive(true);
                go.GetComponent<Image>().sprite = currentDrink[count].getSprite();
            }
            else
            {
                go.SetActive(false);
            }
            count++;
        }
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
        Debug.Log(currentDrinkStats);

        for (int i = 0; i < 6; i++)
        {
            FindObjectOfType<PlayerInventoryController>().InventoryContents[i] = inventory[i];
        }

        if (currentDrinkStats == currentOrder)
        {
            OpenDrinkCompleteMenu();
        }
        else
        {
            StartCoroutine(CloseMenu());
        }
    }
    public void CloseDrinkCompleteMenu()
    {
        drinkCompleteMenu.SetActive(false);
        orderComplete = true;
        FindObjectOfType<GameManager>().DrinkCompleted(IngredientProfit * currentDrink.Count);
        FindObjectOfType<GameManager>().ShowMoney();
        currentCustomer.CustomerComplete();
        StartCoroutine(CloseMenu());
    }
    private void OpenDrinkCompleteMenu()
    {
        drinkCompleteMenu.SetActive(true);
        drink.sprite = currentCustomer.getOrder().GetDrinkSprite();
        drinkNameText.text = currentCustomer.getOrder().getDrinkName();
        flavorText.text = currentCustomer.getOrder().getDrinkFlavorText();
    }
    public IEnumerator CloseMenu()
    {
        yield return new WaitForSeconds(0.2f);
        
        currentCustomer.isOrdering = false;
        FindObjectOfType<PlayerPlatformerController>().ManageDrinkMenuStatus(null);
    }
    public void TriggerMenuClose()
    {
        StartCoroutine(CloseMenu());
    }

    public void NewOrder(CustomerController customer)
    {
        orderComplete = false;
        currentCustomer = customer;
        currentOrder = customer.getOrder().GetDrinkContents();

        orderSweetText.GetComponent<TMP_Text>().text = currentOrder.x.ToString();
        orderSourText.GetComponent<TMP_Text>().text = currentOrder.y.ToString();
        orderSaltyText.GetComponent<TMP_Text>().text = currentOrder.z.ToString();
        FindObjectOfType<GameManager>().HideMoney();
        if (customer.getOrder().GetDrinkSprite() != null) 
        {
            customerPortrait.GetComponent<Image>().sprite = customer.getOrder().GetDrinkSprite();
        }
        UpdateText();
    }
}
