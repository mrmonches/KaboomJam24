using UnityEngine;

[CreateAssetMenu(fileName = "DrinkData", menuName = "Drinks Data")]
public class DrinkData : ScriptableObject
{
    [SerializeField] private Vector3 DrinkContents;
    [SerializeField] private string DrinkName;
    [SerializeField] private Sprite DrinkSprite;

    public Vector3 GetDrinkContents()
    {
        return DrinkContents;
    }

    public string GetDrinkName()
    {
        return DrinkName;
    }

    public Sprite GetDrinkSprite()
    {
        return DrinkSprite;
    }
}
