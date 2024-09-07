using System;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    // Unity can't serialize or draw a 2D array, nothing will happen
    private int[,] InventoryContents = { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };

    [SerializeField] private int MaxItemAmount;

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
            case DrinkMenuManager.IngredientTypes.Ectoplasm:
                InventoryContents[0, 0] += amount;
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
            InventoryContents[i, 1] = MaxItemAmount;

            print(InventoryContents[i, 0] + " " + InventoryContents[i, 1] + "\n");
        }
    }
}
