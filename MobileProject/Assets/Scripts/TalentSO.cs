using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talent", menuName = "Data/Talent", order = 1)]
public class TalentSO : ScriptableObject
{
    public Sprite sprite;
    public new string name;

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

    public string GetName()
    {
        return (name + " " + ToRoman(level));
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
        dmgMulti = 1.0f;
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

    public static string ToRoman(int number)
    {
        if ((number < 0) || (number > 3999)) return string.Empty;
        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900);
        if (number >= 500) return "D" + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C" + ToRoman(number - 100);
        if (number >= 90) return "XC" + ToRoman(number - 90);
        if (number >= 50) return "L" + ToRoman(number - 50);
        if (number >= 40) return "XL" + ToRoman(number - 40);
        if (number >= 10) return "X" + ToRoman(number - 10);
        if (number >= 9) return "IX" + ToRoman(number - 9);
        if (number >= 5) return "V" + ToRoman(number - 5);
        if (number >= 4) return "IV" + ToRoman(number - 4);
        if (number >= 1) return "I" + ToRoman(number - 1);
        return string.Empty;
    }
}
