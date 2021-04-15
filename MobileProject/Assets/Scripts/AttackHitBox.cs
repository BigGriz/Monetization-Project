using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public bool check;
    EnemyController collided;
    PlayerController pc;
    private void Awake()
    {
        pc = GetComponentInParent<PlayerController>();
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
            pc.DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            collided = collision.gameObject.GetComponent<EnemyController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() == collided)
            collided = null;
    }
}
