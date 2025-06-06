using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInventoryController : MonoBehaviour
{
    // Unity can't serialize or draw a 2D array, nothing will happen
    public int[] InventoryContents = {9, 9, 9, 9, 9, 9};

    [SerializeField] private int MaxItemAmount;
    [SerializeField] private int HazardDecrement;

    /// <summary>
    /// Don't know exactly what the inventory update looks like
    /// Rough example of how it could work
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void RefreshInventory(Enum type, int amount)
    {
        switch (type)
        {
            case DrinkMenuManager.IngredientTypes.Draculum:
                //InventoryContents[0, 0] += amount;
                break;
        }
    }

    /// <summary>
    /// Fully restocks player's inventory
    /// </summary>
    public void FullRestock()
    {
        for (int i = 0; i <= 5; i++)
        {
            InventoryContents[i] = MaxItemAmount;
        }
        if (FindObjectOfType<TutorialManager>() != null)
        {
            FindObjectOfType<TutorialManager>().NextPopup(13);
        }
    }

    public void LoseItems()
    {
        int index = Random.Range(0, 6);

        InventoryContents[index] -= HazardDecrement;

        if (InventoryContents[index] < 0)
        {
            InventoryContents[index] = 0;
        }
    }
}