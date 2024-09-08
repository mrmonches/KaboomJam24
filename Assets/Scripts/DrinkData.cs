using UnityEngine;

[CreateAssetMenu(fileName = "DrinkData", menuName = "Drinks Data")]
public class DrinkData : ScriptableObject
{
    [SerializeField] private Vector3 DrinkContents;
    [SerializeField] private Sprite DrinkSprite;
    [SerializeField] private string DrinkName;
    [SerializeField] private string DrinkFlavorText;

    public Vector3 GetDrinkContents()
    {
        return DrinkContents;
    }

    public string getDrinkName()
    {
        return DrinkName;
    }
    public Sprite GetDrinkSprite()
    {
        return DrinkSprite;
    }

    public string getDrinkFlavorText()
    {
        return DrinkFlavorText;
    }
}
