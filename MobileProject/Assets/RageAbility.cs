using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// causes clicks to do massive damage for awhile
public class RageAbility : AbilityUI
{
    public override void Press()
    {
        Debug.LogWarning("Rage Pressed");
        if (PlayerController.instance.Rage(ability.cost, ability.duration))
        {
            ability.timer = ability.duration;
        }
    }
}
