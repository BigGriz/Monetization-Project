using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    /*float timer;
    PlayerController pc;
    Vector2 offset;*/

    /*private void Update()
    {
        if (CallbackHandler.instance.settings.paused || !pc)
            return;

        this.transform.position = (Vector2)pc.transform.position + offset;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
    }*/

    private void OnMouseDown()
    {
        if (!CallbackHandler.instance.settings.paused)
        {
            CallbackHandler.instance.TogglePause(true);
            CallbackHandler.instance.ChangeMenu(MENUOPTION.SHOP);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            pc = collision.gameObject.GetComponent<PlayerController>();
            offset = this.transform.position - pc.transform.position;
            timer = 30.0f;
        }
    }*/
}
