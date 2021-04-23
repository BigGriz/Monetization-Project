using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Data/Gear", order = 1)]
public class Gear : ScriptableObject
{
    public Sprite sprite;
    new public string name;
    public int level;
    public int damage;
    public int upgradeCost;
    public float armour;
    public int levelRequirement;

    public void Upgrade()
    {
        if (PlayerInventory.instance.SpendCoin(upgradeCost))
        {
            // change this to upgrade stats
            armour += 1;
            damage += 1;
            upgradeCost += 1;
            level += 1;
        }
    }
}
