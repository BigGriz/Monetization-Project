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
    public int points;
    public bool isBoss;
    public float damage;

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
    private void Start()
    {
        CallbackHandler.instance.togglePause += TogglePause;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.togglePause -= TogglePause;
    }

    public void TogglePause(bool _pause)
    {
        if (_pause)
        {
            rb.velocity = Vector3.zero;
            anim.speed = 0.0f;
            return;
        }
        anim.speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (CallbackHandler.instance.settings.paused)
        {
            return;
        }

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
            collided.TakeDamage(damage * Random.Range(0.9f, 1.1f));
    }

    void Die()
    {
        bc.enabled = false;
        anim.SetTrigger("Death");
        AudioManager.instance.PlayAudio("Death");
        Destroy(this.gameObject, 3.0f);
        hpBar.SetActive(false);


        PlayerInventory.instance.GiveCoin(coinReward);
        PlayerInventory.instance.GiveXP(xpReward);

        if (isBoss)
            CallbackHandler.instance.SpawnPortal();
    }

    public void MultiplyStats(float _multiplier)
    {
        health *= _multiplier * _multiplier;
        damage *= _multiplier;
        xpReward = Mathf.RoundToInt(xpReward *_multiplier);
        coinReward = Mathf.RoundToInt(coinReward * _multiplier);

        if (isBoss)
        {
            health *= _multiplier;
            damage *= _multiplier;
            xpReward = Mathf.RoundToInt(xpReward * _multiplier);
            coinReward = Mathf.RoundToInt(coinReward * _multiplier);
        }
        currentHealth = health;
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
