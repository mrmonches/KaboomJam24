using UnityEngine;

public class HazardController : MonoBehaviour
{
    [SerializeField] private Animator _hazardAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject incomingCollision = collision.gameObject;

        if (incomingCollision.CompareTag("Player"))
        {
            incomingCollision.GetComponent<PlayerPlatformerController>().ActivateIFrames();

            //Play animation
            _hazardAnimator.SetTrigger("Attack");

        }
    }
}
