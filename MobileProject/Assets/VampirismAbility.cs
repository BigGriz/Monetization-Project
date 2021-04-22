using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismAbility : AbilityUI
{
    public override void Press()
    {
        Debug.LogWarning("Vamp Pressed");
        PlayerController.instance.Vampirism(cost);
    }
}
