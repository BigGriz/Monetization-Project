using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5 Succcessive attacks with double anim speed
public class FlurryAbility : AbilityUI
{
    public override void Press()
    {
        Debug.LogWarning("Flurry Pressed");
        PlayerController.instance.Flurry(cost);
    }
}
