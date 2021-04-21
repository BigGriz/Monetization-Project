using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    // Local Variables
    Animator anim;
    bool show;
    public GameObject talentGroup;
    public GameObject talentImagePrefab;

    public TMPro.TextMeshProUGUI damageText;
    public TMPro.TextMeshProUGUI atkSpdText;
    public TMPro.TextMeshProUGUI hpText;

    #region Setup
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    #endregion Setup
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.addTalent += AddTalent;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.addTalent -= AddTalent;
    }
    #endregion Callbacks

    public void AddTalent(TalentSO _talent)
    {
        Instantiate(talentImagePrefab, talentGroup.transform).GetComponent<UnityEngine.UI.Image>().sprite = _talent.sprite;
    }

    public void UpdateTextElements()
    {
        damageText.SetText("DAMAGE: " + PlayerController.instance.GetVariable(VariableType.DAMAGE).ToString());
        atkSpdText.SetText("ATK SPD: " + PlayerController.instance.GetVariable(VariableType.ATKSPD).ToString());
        hpText.SetText("HEALTH: " + PlayerController.instance.GetVariable(VariableType.HP).ToString());
    }
}
