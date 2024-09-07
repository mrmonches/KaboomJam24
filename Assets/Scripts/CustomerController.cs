using UnityEngine;

public class CustomerController : MonoBehaviour
{
    private PlayerPlatformerController playerController;

    private DrinkData order;

    public void setOrder(DrinkData order)
    {
        this.order = order;
    }

    public DrinkData getOrder()
    {
        return order;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerPlatformerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController = null;
    }

    private void FixedUpdate()
    {
        if (playerController != null && playerController.GetInteractionStatus())
        {
            playerController.ManageDrinkMenuStatus(order);
        }
    }
}
