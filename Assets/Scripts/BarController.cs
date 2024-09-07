using System.Collections;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField] private float DelayTimer;
    private float delayTimerCounter;
    [SerializeField] private float CooldownTimer;

    [SerializeField] private bool canRestock;

    [SerializeField] private PlayerInventoryController playerInventoryController;

    private PlayerPlatformerController playerController;

    private void Awake()
    {
        canRestock = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canRestock)
        {
            playerController = collision.gameObject.GetComponent<PlayerPlatformerController>();

            playerInventoryController = playerController.GetComponent<PlayerInventoryController>();

            playerController.InteractiveUIStatus(true);
        }
    }

    public float GetDelayTimer()
    {
        return DelayTimer;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerPlatformerController>() == playerController)
        {
            playerController.InteractiveUIStatus(false);

            playerController = null;

            playerInventoryController = null;
        }
    }
    private IEnumerator RestockCooldown()
    {
        if (!canRestock)
        {
            playerInventoryController.FullRestock();
            yield return new WaitForSeconds(CooldownTimer);
            print("allowed to restock");
            canRestock = true;
        }
    }

    private void FixedUpdate()
    {
        if (playerController != null && playerController.GetInteractionStatus() && canRestock)
        {
            delayTimerCounter -= Time.deltaTime;
        }
        else
        {
            delayTimerCounter = DelayTimer; 
        }

        if (delayTimerCounter <= 0)
        {
            canRestock = false;
            StartCoroutine(RestockCooldown());
        }
    }
}
