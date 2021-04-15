using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Setup Fields")]
    public float attackSpeed;
    Vector2 moveVec;
    public GameObject hpBar;

    [Header("Stats")]
    public float damage;
    public float moveSpeed;
    public float health;
    public float blockAmount;

    // Local Variables
    float currentHealth;
    Animator anim;
    Rigidbody2D rb;
    bool dashing;
    bool attacking;
    bool blocking;
    [HideInInspector] public EnemyController collided;
    AttackHitBox ahb;
    HPBar hp;

    public TalentSO talents;

    public void UpdateUI()
    {
        CallbackHandler.instance.UpdateDamage((damage + PlayerInventory.instance.GetGearTotals() + talents.addedFlat) * talents.dmgMulti * attackSpeed);
    }


    public static PlayerController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one player exists!");
            Destroy(this.gameObject);
        }
        instance = this;


        moveVec = Vector2.right;
        currentHealth = health;

        ahb = GetComponentInChildren<AttackHitBox>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponentInChildren<HPBar>();

        anim.SetFloat("SpeedMultiplier", attackSpeed);
    }

    private void Start()
    {
        CallbackHandler.instance.updateUI += UpdateUI;

        Invoke("UpdateUI", 0.1f);
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.updateUI -= UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (collided) ? Vector2.zero : (dashing) ? moveVec * moveSpeed * 5.0f : moveVec * moveSpeed;

        if (!IsPointerOverUIObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                // If in range - Attack
                if (collided)
                {
                    attacking = false;
                    anim.SetTrigger("AttackClick");
                    return;
                }
                // If not in range - Dash forward
                dashing = true;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // If in range - Attack
                if (collided)
                {
                    attacking = false;
                    blocking = true;
                    return;
                }
                // If not in range - Dash forward
                dashing = true;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            blocking = false;
        }

        SetAnims();
    }

    void SetAnims()
    {
        anim.SetBool("Blocking", blocking);
        anim.SetBool("Attacking", attacking);
        anim.SetFloat("MoveSpeed", Mathf.Clamp01(rb.velocity.x));
        anim.SetBool("Collided", collided);
        anim.SetFloat("SpeedMultiplier", attackSpeed);
    }

    public void SetAttacking()
    {
        attacking = true;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= Mathf.Clamp(_damage - blockAmount, 0, _damage);
        hp.UpdateHealth(currentHealth / health);
        if (currentHealth <= 0.0f)
        {
            Die();
            return;
        }
    }
    void Die()
    {
        anim.SetTrigger("Death");
        Destroy(this.gameObject, 3.0f);
    }

    public void DealDamage()
    {
        if (collided)
            collided.TakeDamage(GetVariable(VariableType.DAMAGE));
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public float GetVariable(VariableType _type)
    {
        switch(_type)
        {
            case VariableType.DAMAGE:
            {
                return Mathf.RoundToInt((damage + PlayerInventory.instance.GetGearTotals() + talents.addedFlat) * talents.dmgMulti);
            }
            case VariableType.ATKSPD:
            {
                return attackSpeed;
            }
            case VariableType.HP:
            {
                return Mathf.RoundToInt((health + talents.addedHealth) * talents.healthMultiplier);
            }
            default:
                return 0.0f;
        }
    }

    #region CollisionEvents
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
        if (collision.gameObject.GetComponent<EnemyController>())
            collided = collision.gameObject.GetComponent<EnemyController>();
    
        dashing = false;
        SetAttacking();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() == collided)
            collided = null;
    }
    #endregion CollisionEvents
}

public enum VariableType
{
    DAMAGE,
    ATKSPD,
    HP,
}
