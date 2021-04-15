using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackHandler : MonoBehaviour
{
    #region Singleton
    public static CallbackHandler instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one UIController exists!");
            Destroy(this);
        }
        instance = this;
    }
    #endregion Singleton

    public event Action updateUI;
    public void UpdateUI()
    {
        if (updateUI != null)
            updateUI();
    }

    public event Action<float> updateDamage;
    public void UpdateDamage(float _damage)
    {
        if (updateDamage != null)
            updateDamage(_damage);      
    }

    public event Action<int> updateCoins;
    public void UpdateCoins(int _coins)
    {
        if (updateCoins != null)
            updateCoins(_coins);
    }

    public event Action<string, int> updateLevel;
    public void UpdateLevel(string _area, int _level)
    {
        if (updateLevel != null)
            updateLevel(_area, _level);
    }

    public event Action<int, float> updateXP;
    public void UpdateXP(int _level, float _xp)
    {
        if (updateXP != null)
            updateXP(_level, _xp);
    }

    public event Action<Gear> addGear;
    public void AddGear(Gear _gear)
    {
        if (addGear != null)
            addGear(_gear);
        UpdateUI();
    }

    public event Action<int> updateTalentPoints;
    public void UpdateTalentPoints(int _points)
    {
        if (updateTalentPoints != null)
            updateTalentPoints(_points);
    }

    public event Action updateTalents;
    public void UpdateTalents()
    {
        if (updateTalents != null)
            updateTalents();
        UpdateUI();
    }

    public event Action<MENUOPTION> changeMenu;
    public void ChangeMenu(MENUOPTION _option)
    {
        if (changeMenu != null)
            changeMenu(_option);
    }
}

public enum MENUOPTION
{
    CHARACTER,
    TALENT,
    SHOP,
    BACKPACK
}
