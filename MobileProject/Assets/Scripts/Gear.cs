using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Data/Gear", order = 1)]
public class Gear : ScriptableObject
{
    [Header("Setup Fields")]
    public Sprite sprite;
    new public string name;
    public int levelRequirement;
    public int level;

    [Header("Stats")]
    public int damage;
    public float armour;

    [Header("Upgrade Variables")]
    public int dmgUpgrade;
    public int armUpgrade;
    public int upgradeCost;

    public void Upgrade()
    {
        if (PlayerInventory.instance.SpendCoin(upgradeCost))
        {
            // change this to upgrade stats
            armour += Mathf.RoundToInt(armUpgrade * (level * 0.5f));
            damage += Mathf.RoundToInt(dmgUpgrade * (level * 0.5f));
            upgradeCost += Mathf.CeilToInt(upgradeCost * level * 0.5f);
            level += 1;
            AudioManager.instance.PlayAudio("UI");
        }
    }
}
