using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    
    [SerializeField] private DrinkMenuManager.IngredientTypes type;
    [SerializeField] private int sweet;
    [SerializeField] private int sour;
    [SerializeField] private int spicy;


    public DrinkMenuManager.IngredientTypes getIngType()
    {
        return type;
    }
    public int getSweet()
    {
        return sweet;
    }
    public int getSour()
    {
        return sour;
    }
    public int getSpicy()
    {
        return spicy;
    }
    public Ingredient(int sweet, int sour, int spicy, DrinkMenuManager.IngredientTypes type)
    {
        this.sweet = sweet;
        this.sour = sour;
        this.spicy = spicy;
        this.type = type;
    }
}
