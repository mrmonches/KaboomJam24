using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject incomingCollision = collision.gameObject;

        if (incomingCollision.CompareTag("Player"))
        {

        }
    }
}
