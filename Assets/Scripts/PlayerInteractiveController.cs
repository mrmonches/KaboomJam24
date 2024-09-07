using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractiveController : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;

    private PlayerPlatformerController playerController;

    [SerializeField] private BarController barController;

    [SerializeField] private float Increment;
    private float incrementCalculation;

    [SerializeField] private Image Circle;

    private float timer;

    /*private void Awake()
    {
        playerController = PlayerObject.GetComponent<PlayerPlatformerController>();

        incrementCalculation = barController.GetDelayTimer() / Increment;

        Circle.fillAmount = 0;
    }

    public void ProgressCircle()
    {
        Circle.fillAmount += incrementCalculation;
    }

    private void FixedUpdate()
    {
        if ((!playerController.GetInteractionStatus() || !playerController.CheckUIStatus()) && Circle.fillAmount > 0)
        {
            Circle.fillAmount = 0f;
        }
        else if (playerController.GetInteractionStatus() && playerController.CheckUIStatus())
        {
            ProgressCircle();
        }*/

        // Testing Circle timer, use if change bar interaction length
        //if (Circle.fillAmount > 0 && Circle.fillAmount < 1)
        //{
        //    timer += Time.deltaTime;
        //}
        //else if (Circle.fillAmount >= 1)
        //{
        //    print(timer);
        //}
        //else
        //{
        //    timer = 0f;
        //}

        //incrementCalculation = barController.GetDelayTimer() / Increment;
    //}
}
