using System.Collections;
using UnityEngine;

public class EmptyParentController : MonoBehaviour
{
    [SerializeField] private float EmptyTimer;

    private void Awake()
    {
        StartCoroutine("ParentTimer");    
    }

    private IEnumerator ParentTimer()
    {
        if(true)
        {
            yield return new WaitForSeconds(EmptyTimer);
            Destroy(gameObject);
        }
    }
}
