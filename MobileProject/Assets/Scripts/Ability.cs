using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Data/Ability", order = 2)]
public class Ability : ScriptableObject
{
    public Sprite sprite;
    public int cost;
    public float duration;
    public int price;
    [HideInInspector] public float timer;
}
