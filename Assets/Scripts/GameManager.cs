using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int money;
    [SerializeField] public int maxMoney;

    [SerializeField] private List<Sprite> portraits;

    [SerializeField] private List<DrinkData> drinks;
    [SerializeField] private List<DrinkData> availableDrinks;
    [SerializeField] private List<CustomerController> customers;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        PlayerPlatformerController.isMixing = false;
        foreach (CustomerController customer in customers)
        {
            AssignDrinks(customer);
            //int rand = Random.Range(0, portraits.Count);
            //customer.portrait = portraits[rand];
            //portraits.RemoveAt(rand);
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
        moneyText.text = money + " / " + maxMoney;
        if (money >= maxMoney)
        {
            QuotaComplete();
        }
    }

    public void HideMoney()
    {
        moneyText.gameObject.SetActive(false);
    }
    public void ShowMoney()
    {
        moneyText.gameObject.SetActive(true);
    }
    public void QuotaComplete()
    {
        Debug.Log("Game won");
    }
}
