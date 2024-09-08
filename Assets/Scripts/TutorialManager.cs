using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> TutorialPopups = new List<GameObject>();
    [SerializeField] private GameObject drinkMenu;
    [SerializeField] private DrinkMenuManager menuManager;
    [SerializeField] private GameObject incorrectIngredientPopup;
    [SerializeField] private CustomerController customer;
    [SerializeField] private DrinkData drink2;
    public static bool SecondPopupDone;
    public static bool FifthPopupDone;
    public static bool SixthPopupDone;
    public static bool NinthPopupDone;


    private void Start()
    {
        PlayerPlatformerController.isMixing = true;
    }

    public void NextPopup(int index)
    {
        switch (index)
        {
            case 0:
                TutorialPopups[0].SetActive(false);
                PlayerPlatformerController.isMixing = false;
                menuManager.canClickIngredients = false;
                menuManager.canClickGlass = false;
                menuManager.canClose = false;
                menuManager.canShake = false;
                break;
            case 1:
                TutorialPopups[1].SetActive(false);
                TutorialPopups[2].SetActive(true);
                break;
            case 2:
                TutorialPopups[2].SetActive(false);
                TutorialPopups[3].SetActive(true);
                break;
            case 3:
                TutorialPopups[3].SetActive(false);
                TutorialPopups[4].SetActive(true);
                break;
            case 4:
                TutorialPopups[4].SetActive(false);
                TutorialPopups[5].SetActive(true);
                break;
            case 5:
                TutorialPopups[5].SetActive(false);
                menuManager.canClickIngredients = true;
                break;
            case 6:
                FifthPopupDone = true;
                menuManager.canClickIngredients = false;
                TutorialPopups[6].SetActive(true);
                break;
            case 7:
                TutorialPopups[6].SetActive(false);
                menuManager.canClickGlass = true;
                break;
            case 8:
                SixthPopupDone = true;
                TutorialPopups[7].SetActive(true);
                menuManager.canClickGlass = false;
                break;
            case 9:
                TutorialPopups[7].SetActive(false);
                TutorialPopups[8].SetActive(true);
                break;
            case 10:
                TutorialPopups[8].SetActive(false);
                menuManager.canClickIngredients = true;
                menuManager.canClickGlass = true;
                menuManager.canClose = true;
                menuManager.canShake = true;
                break;
            case 11:
                TutorialPopups[9].SetActive(true);
                customer.setOrder(drink2);
                PlayerPlatformerController.isMixing = true;
                break;
            case 12:
                TutorialPopups[9].SetActive(false);
                PlayerPlatformerController.isMixing = false;
                break;
            case 13:
                TutorialPopups[10].SetActive(true);
                PlayerPlatformerController.isMixing = true;
                break;
            case 14:
                TutorialPopups[10].SetActive(false);
                TutorialPopups[11].SetActive(true);
                break;
            case 15:
                SceneManager.LoadScene(2);
                break;
            default:
                break;
        }
    }

    public void incorrectIngredients()
    {
        if (incorrectIngredientPopup.activeInHierarchy)
        {
            incorrectIngredientPopup.SetActive(false);
            menuManager.canClickIngredients = true;
            menuManager.canClickGlass = true;
            menuManager.canClose = true;
            menuManager.canShake = true;
        }
        else
        {
            incorrectIngredientPopup.SetActive(true);
            menuManager.canClickIngredients = false;
            menuManager.canClickGlass = false;
            menuManager.canClose = false;
            menuManager.canShake = false;
        }
    }
    public void EnablePopup(int popup)
    {
        TutorialPopups[popup].SetActive(true);
    }
}
