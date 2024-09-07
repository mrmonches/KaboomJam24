using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkLayer
{
    [SerializeField] private GameObject layer;
    [SerializeField] private DrinkMenuManager.IngredientTypes currentType;

    public void setIngType(DrinkMenuManager.IngredientTypes type)
    {
        currentType = type;
    }

    public DrinkMenuManager.IngredientTypes getIngType()
    {
        return currentType;
    }

    public GameObject getLayer()
    {
        return layer;
    }

    public DrinkLayer(GameObject layer, DrinkMenuManager.IngredientTypes currentType)
    {
        this.layer = layer;
        this.currentType = currentType;
    }
}
