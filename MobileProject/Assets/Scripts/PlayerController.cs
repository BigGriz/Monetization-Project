using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Setup Fields")]
    public float attackSpeed;
    Vector2 moveVec;
    public HPBar hp;
    public PlayerEnergy en;

    [Header("Stats")]
    public float damage;
    public float moveSpeed;
    public float health;
    public float blockAmount;
    public float energy;
    public float energyRegen;

    // Local Variables
    float currentHealth;
    public float currentEnergy;
    float energyTimer;
    Animator anim;
    Rigidbody2D rb;
    bool dashing;
    bool attacking;
    bool blocking;
    bool flurry;
    [HideInInspector] public EnemyController collided;
    AttackHitBox ahb;

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
        currentHealth = GetVariable(VariableType.HP);

        ahb = GetComponentInChildren<AttackHitBox>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //hp = GetComponentInChildren<HPBar>();

        anim.SetFloat("SpeedMultiplier", attackSpeed);
    }

    private void Start()
    {
        CallbackHandler.instance.updateUI += UpdateUI;
        CallbackHandler.instance.togglePause += TogglePause;

        Invoke("UpdateUI", 0.1f);
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.updateUI -= UpdateUI;
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

    public void GiveEnergy()
    {
        if (CallbackHandler.instance.settings.paused)
            return;

        currentEnergy++;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, energy);
        en.UpdateEnergy(currentEnergy / energy);

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

    void UpdateEnergy()
    {
        // No passive energy regen during shield
        if (!shield)
        {
            energyTimer -= Time.deltaTime;
            if (energyTimer <= 0)
            {
                energyTimer += energyRegen;
                currentEnergy++;
            }
        }

        /*if (!IsPointerOverUIObject() && Input.GetMouseButtonDown(0))
            GiveEnergy();*/

        currentEnergy = Mathf.Clamp(currentEnergy, 0, energy);

        if (vampirism)
        {
            currentEnergy -= Time.deltaTime * vampDrain;
            if (currentEnergy <= 0)
            {
                vampirism = false;
                Debug.LogWarning("Vamp fell off");
            }
        }

        en.UpdateEnergy(currentEnergy / energy);
    }

    public float flurryTimer;
    public float flurryClickTimer;
    public bool Flurry(float _cost, float _duration)
    {
        if (!flurry && currentEnergy >= _cost)
        {
            currentEnergy -= _cost;
            flurryTimer = _duration;
            flurry = true;
            return true;
        }
        return false;
    }

    public bool vampirism;
    float vampDrain = 6.0f;
    float vampDuration;
    public bool Vampirism(float _cost, float _duration)
    {
        if (!vampirism && currentEnergy >= _cost)
        {
            vampDrain = _cost;
            vampDuration = _duration;
        
            vampirism = true;
            return true;
        }
        return false;
    }

    bool shield;
    public void Shield(float _cost)
    {
        shield = !shield;
    }

    void FlurryAttack()
    {
        flurryTimer -= Time.deltaTime;
        flurryClickTimer -= Time.deltaTime;
        
        if (flurryTimer <= 0.0f)
        {
            flurry = false;
            return;
        }


        // If in range - Attack
        if (collided && flurryClickTimer <= 0.0f)
        {
            flurryClickTimer = 1.0f;
            attacking = false;
            anim.SetTrigger("AttackClick");
            return;
        }
        if (!collided)
            dashing = true;
    }

    bool rage;
    float rageTimer;
    public bool Rage(float _cost, float _duration)
    {
        if (currentEnergy >= _cost && !rage)
        {
            Debug.LogWarning("Successfully raging");
            currentEnergy -= _cost;
            rage = true;
            rageTimer = _duration;
            return true;
        }
        return false;
    }


    void UpdateHealth()
    {
        if (currentHealth > GetVariable(VariableType.HP))
            currentHealth -= Time.deltaTime * 3.0f;
    }



    // Update is called once per frame
    void Update()
    {
        if (CallbackHandler.instance.settings.paused)
        {
            return;
        }

        hp.UpdateHealth(currentHealth / GetVariable(VariableType.HP));
        HPTextUI.instance.SetText((int)currentHealth, (int)GetVariable(VariableType.HP));

        rb.velocity = (collided) ? Vector2.zero : (dashing) ? moveVec * moveSpeed * 5.0f : moveVec * moveSpeed;

        if (collided)
        {
            AudioManager.instance.StopAudio("Walk");
            AudioManager.instance.StopAudio("Run");
        }
        else
        {
            AudioManager.instance.PlayAudio(dashing ? "Run" : "Walk");
            AudioManager.instance.StopAudio(dashing ? "Walk" : "Run");
        }


        UpdateEnergy();
        UpdateHealth();

        if (rage)
        {
            rageTimer -= Time.deltaTime;
            if (rageTimer <= 0)
            {
                rage = false;
            }
        }

        if (vampirism)
        {
            vampDuration -= Time.deltaTime;
            if (vampDuration <= 0)
            {
                vampirism = false;
            }
        }


        if (flurry)
        {
            FlurryAttack();
        }

        /*if (!IsPointerOverUIObject())
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
        }*/

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
        if (shield)
        {
            if (_damage / 2.0f < currentEnergy)
            {
                currentEnergy -= _damage / 2.0f;
                _damage = _damage / 2.0f;
            }
            else
            {
                float temp = currentEnergy - _damage / 2.0f;
                currentEnergy = 0.0f;
                _damage += temp;
                Debug.LogWarning("Shield Run Dry");
            }
            en.UpdateEnergy(currentEnergy / energy);
        }

        currentHealth -= Mathf.Clamp(_damage - GetVariable(VariableType.ARMOR), 0, _damage);
        hp.UpdateHealth(currentHealth / GetVariable(VariableType.HP));
        if (currentHealth <= 0.0f)
        {
            Die();
            return;
        }
    }
    void Die()
    {
        anim.SetTrigger("Death");
        AudioManager.instance.PlayAudio("Death");
        CallbackHandler.instance.FadeToSameLevel();
        anim.SetTrigger("Revive");
    }

    public void ResetCharacter()
    {
        currentHealth = GetVariable(VariableType.HP);
        currentEnergy = 0;
        hp.UpdateHealth(currentHealth / GetVariable(VariableType.HP));
        en.UpdateEnergy(currentEnergy / energy);
    }

    public void DealDamage()
    {
        if (collided)
        {
            AudioManager.instance.PlayAudio("Sword");

            collided.TakeDamage(GetVariable(VariableType.DAMAGE) * (rage ? 2.0f : 1.0f));
            if (vampirism)
            {
                currentHealth += (GetVariable(VariableType.DAMAGE) / 2.0f) * (rage ? 2.0f : 1.0f);
                hp.UpdateHealth(currentHealth / GetVariable(VariableType.HP));
            }
        }
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
                return Mathf.RoundToInt((health + talents.addedHealth) * (1.0f + talents.healthMultiplier));
            }
            case VariableType.ARMOR:
            {
                return Mathf.RoundToInt(PlayerInventory.instance.GetArmorTotals());
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
    ARMOR,
}
