using UnityEngine;

public class CustomerController : MonoBehaviour
{
    private PlayerPlatformerController playerController;

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
            print("into mixing");
        }
    }
}
