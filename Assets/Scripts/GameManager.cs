using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private int maxMoney;

    [SerializeField] private List<DrinkData> drinks;
    [SerializeField] private List<DrinkData> availableDrinks;
    [SerializeField] private List<CustomerController> customers;

    private void Start()
    {
        
    }

    private void AssignDrinks()
    {
        foreach (CustomerController customer in customers)
        {
            if (availableDrinks.Count == 0)
            {
                availableDrinks = drinks;
            }
            int rand = Random.Range(0, drinks.Count - 1);
            customer.setOrder(drinks[rand]);
            drinks.RemoveAt(rand);
        }
       

    }
    public void DrinkCompleted(int profit)
    {
        money += profit;
        if (money >= maxMoney)
        {
            QuotaComplete();
        }
    }
    public void QuotaComplete()
    {
        Debug.Log("Game won");
    }
}
