using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismAbility : AbilityUI
{
    public override void Press()
    {
        Debug.LogWarning("Vamp Pressed");
        if (PlayerController.instance.Vampirism(ability.cost, ability.duration))
        {
            ability.timer = ability.duration;
        }
    }

    public override void Update()
    {
        if (CallbackHandler.instance.settings.paused)
            return;

        validEnergy.enabled = (ability.timer <= 0 && PlayerController.instance.currentEnergy <= ability.cost && unlocked);

        if (!PlayerController.instance.vampirism)
        {
            ability.timer = 0;
            activeImage.fillAmount = ability.timer / ability.duration;
        }

        if (ability.timer > 0)
        {
            ability.timer -= Time.deltaTime;
            activeImage.fillAmount = ability.timer / ability.duration;
            return;
        }
    }
}
