using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        foreach (CustomerController customer in customers)
        {
            AssignDrinks(customer);

        }
    }
    public void AssignDrinks (CustomerController customer)
    {
        if (availableDrinks.Count <= 0)
        {
            foreach (DrinkData drink in drinks)
            {
                availableDrinks.Add(drink);
            }
        }
        int rand = Random.Range(0, availableDrinks.Count);
        Debug.Log(rand + " " + availableDrinks[rand].getDrinkName());
        customer.setOrder(availableDrinks[rand]);
        availableDrinks.RemoveAt(rand);
    }
    public bool hasReachedQuota()
    {
        return money >= maxMoney;
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
