using UnityEngine;

[CreateAssetMenu(fileName = "DrinkData", menuName = "Drinks Data")]
public class DrinkData : ScriptableObject
{
    [SerializeField] private Vector3 DrinkContents;
    [SerializeField] private Sprite DrinkSprite;

    public Vector3 GetDrinkContents()
    {
        return DrinkContents;
    }
    public Sprite GetDrinkSprite()
    {
        return DrinkSprite;
    }
}
