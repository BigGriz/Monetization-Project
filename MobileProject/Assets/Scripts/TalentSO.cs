using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talent", menuName = "Data/Talent", order = 1)]
public class TalentSO : ScriptableObject
{
    public int level = 0;
    public int maxLevel = 10;

    [Header("Added Damage Flat")]
    public int addedFlat = 0;
    public int addedDamageFlat;
    public float addedDamageFlatPerLevel;
    [Header("Added Damage Multipliers")]
    public float dmgMultiPerLvl;
    public float dmgMultiBase;
    public float dmgMulti;
    [Header("Added Health Flat")]
    public int addedHealth;
    [Header("Added Health Multiplier")]
    public float healthMultiplier;

    public void AddLevel()
    {
        level++; 
        // Flat Damage
        addedFlat += Mathf.RoundToInt((float)addedDamageFlat * Mathf.Pow(addedDamageFlatPerLevel, level));

        // Damage multiplier
        dmgMulti += dmgMultiBase * Mathf.Pow(dmgMultiPerLvl, level);
        // Round to one decimal place
        dmgMulti = (float)Mathf.RoundToInt(dmgMulti * 100.0f) / 100.0f;

        // not set up properly
        addedHealth += addedHealth;
        healthMultiplier += healthMultiplier;
    }

    public TalentSO GetNextLevel()
    {
        TalentSO copy = Instantiate(this);
        copy.AddLevel();

        return (copy);
    }

    public void ResetTotals()
    {
        addedFlat = 0;
        dmgMulti = 0;
        addedHealth = 0;
        healthMultiplier = 0;
    }

    public void AddTalent(TalentSO _talent)
    {
        addedFlat += _talent.addedFlat;
        dmgMulti += _talent.dmgMulti;
        addedHealth += _talent.addedHealth;
        healthMultiplier += _talent.healthMultiplier;
    }
}
