using UnityEngine;

[CreateAssetMenu(fileName = "DrinkData", menuName = "Drinks Data")]
public class DrinkData : ScriptableObject
{
    [SerializeField] private Vector3 DrinkOrders;
    [SerializeField] private string DrinkName;
    [SerializeField] private Sprite DrinkSprite;
}
