using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : AbilityUI
{
    public override void Press()
    {
        Debug.LogWarning("Shield Pressed");
        PlayerController.instance.Shield(cost);
    }
}
