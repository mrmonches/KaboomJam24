using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private PlayerPlatformerController playerController;

    public bool isOrdering = false;
    [SerializeField] private bool isGone = false;

    [SerializeField] private DrinkData order;
    [SerializeField] private float TimeUntilBack = 3f;
    public void setOrder(DrinkData order)
    {
        this.order = order;
    }

    public IEnumerator ComeBack()
    {
        isGone = true;
        GetComponent<SpriteRenderer>().color = new Color (0, 0, 0, 0.5f);
        yield return new WaitForSecondsRealtime(TimeUntilBack);
        FindObjectOfType<GameManager>().AssignDrinks(this);
        isGone = false;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);

    }

    public void CustomerComplete()
    {
        StartCoroutine(ComeBack());
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

    private void OnTriggerStay2D(Collider2D collision)
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
        if (playerController != null && playerController.GetInteractionStatus() && !isOrdering && !isGone)
        {
            isOrdering = true;
            playerController.ManageDrinkMenuStatus(this);
        }
    }
}
