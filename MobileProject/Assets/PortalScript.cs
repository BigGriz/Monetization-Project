using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            // Play Anim
            // fade out screen
            // add this callback to enemyspawner etc.

            CallbackHandler.instance.FadeToNextLevel();
        }
    }
}
