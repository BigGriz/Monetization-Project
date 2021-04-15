using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Setup Fields")]
    public float moveSpeed = 1.0f;
    public GameObject hpBar;
    [Header("Enemy Stats")]
    float currentHealth;
    public float health = 10.0f;
    public int xpReward;
    public int coinReward;

    // Local Variables
    Vector2 moveVec;
    Animator anim;
    Rigidbody2D rb;
    PlayerController collided;
    BoxCollider2D bc;
    EnemyAttackHitBox ahb;
    HPBar hp;
    GameObject col;

    private void Awake()
    {
        ahb = GetComponentInChildren<EnemyAttackHitBox>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        hp = GetComponentInChildren<HPBar>();

        moveVec = -Vector2.right;
        currentHealth = health;

        hpBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (col || !bc.enabled) ? Vector2.zero : moveVec * moveSpeed;

        SetAnims();
    }

    void SetAnims()
    {
        anim.SetFloat("MoveSpeed", Mathf.Clamp01(Mathf.Abs(rb.velocity.x)));
        anim.SetBool("Collided", collided);
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        hp.UpdateHealth(currentHealth / health);
        if (currentHealth <= 0.0f)
        {
            Die();
            return;
        }
    }

    public void DealDamage()
    {
        if (collided)
            collided.TakeDamage(1.0f);
    }

    void Die()
    {
        bc.enabled = false;
        anim.SetTrigger("Death");
        Destroy(this.gameObject, 3.0f);
        hpBar.SetActive(false);


        PlayerInventory.instance.GiveCoin(coinReward);
        PlayerInventory.instance.GiveXP(xpReward);
    }

    #region CollisionChecks
    public void HitBoxOn()
    {
        ahb.Toggle(true);
    }
    public void HitBoxOff()
    {
        ahb.Toggle(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        col = collision.gameObject;
        if (collision.gameObject.GetComponent<PlayerController>())
            collided = collision.gameObject.GetComponent<PlayerController>();

        if (collided)
        {
            hpBar.SetActive(true);
            hp.UpdateHealth(currentHealth / health);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == collided)
        {
            // Why is this check needed?
            if (collided)
            {
                // Prevents potential error
                if (collided.collided == this)
                    collided.collided = null;
            }

            collided = null;
            col = null;
        }
    }
    #endregion CollisionChecks
}
