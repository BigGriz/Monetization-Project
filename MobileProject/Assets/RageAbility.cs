using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// causes clicks to do massive damage for awhile
public class RageAbility : AbilityUI
{
    public override void Press()
    {
        Debug.LogWarning("Rage Pressed");
        PlayerController.instance.Rage(cost);
    }
}
