using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    // Local Variables
    Animator anim;
    bool show;

    public TMPro.TextMeshProUGUI damageText;
    public TMPro.TextMeshProUGUI atkSpdText;
    public TMPro.TextMeshProUGUI hpText;

    #region Setup
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    #endregion Setup

    private void Update()
    {
        // Need some sort of menu manager
        if (Input.GetKeyDown(KeyCode.C))
        {
            show = !show;
            anim.SetBool("Show", show);
        }
    }

    public void UpdateTextElements()
    {
        damageText.SetText("DAMAGE: " + PlayerController.instance.GetVariable(VariableType.DAMAGE).ToString());
        atkSpdText.SetText("ATK SPD: " + PlayerController.instance.GetVariable(VariableType.ATKSPD).ToString());
        hpText.SetText("HEALTH: " + PlayerController.instance.GetVariable(VariableType.HP).ToString());
    }
}
