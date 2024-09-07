using System;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    // Unity can't serialize or draw a 2D array, nothing will happen
    private int[,] InventoryContents = { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };

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
}
