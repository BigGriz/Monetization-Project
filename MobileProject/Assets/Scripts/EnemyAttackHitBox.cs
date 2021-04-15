using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitBox : MonoBehaviour
{
    public bool check;
    PlayerController collided;
    EnemyController ec;
    private void Awake()
    {
        ec = GetComponentInParent<EnemyController>();
    }

    public void Toggle(bool _toggle)
    {
        check = _toggle;
    }

    private void Update()
    {
        if (collided && check)
        {
            check = false;
            ec.DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collided = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == collided)
            collided = null;
    }
}
