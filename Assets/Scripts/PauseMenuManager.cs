using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private PlayerInventoryController inventoryController;
    [SerializeField] private List<TMP_Text> IngredientList = new List<TMP_Text>();

    private void OnEnable()
    {
        UpdateIngredientText();

        Time.timeScale = 0f;
    }

    private void UpdateIngredientText()
    {
        for (int i = 0; i <= 5; i++)
        {
            print(i);
            IngredientList[i].text = ("" + inventoryController.InventoryContents[i]);
        }
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("SamLevelTest");
    }

    public void MainMenuLoad()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}