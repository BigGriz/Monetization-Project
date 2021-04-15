using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region Singleton
    public static PlayerInventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Player Inventory exists!");
            Destroy(this);
        }
        instance = this;
    }
    #endregion Singleton
    #region Callbacks & Setup
    private void Start()
    {
        CallbackHandler.instance.addGear += AddGear;

        Invoke("InitialSetup", 0.1f);
    }
    void InitialSetup()
    {
        CallbackHandler.instance.UpdateCoins(coins);
        CallbackHandler.instance.UpdateTalentPoints(talentPoints);
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.addGear -= AddGear;
    }
    #endregion Callbacks & Setup

    public int coins;
    public int xp;
    public int xpRequired;
    public int level = 1;
    public int talentPoints;
    public List<Gear> gear;

    public void GiveXP(int _xp)
    {
        xp += _xp;
        if (xp >= xpRequired)
        {
            xp -= xpRequired;
            level++;
        }
        CallbackHandler.instance.UpdateXP(level, (float)xp / (float)xpRequired);
    }

    public void AddGear(Gear _gear)
    {
        gear.Add(_gear);
    }

    public int GetGearTotals()
    {
        int dmg = 0;
        foreach(Gear n in gear)
        {
            dmg += n.damage;
        }
        return (dmg);
    }

    public void GiveCoin(int _coin)
    {
        coins += _coin;
        CallbackHandler.instance.UpdateCoins(coins);
    }
    public bool SpendCoin(int _coin)
    {
        if (coins >= _coin)
        {
            coins -= _coin;
            CallbackHandler.instance.UpdateCoins(coins);
            return true;
        }
        return false;
    }
    public bool SpendTalentPoint()
    {
        if (talentPoints > 0)
        {
            talentPoints--;
            return true;
        }
        return false;
    }
}
