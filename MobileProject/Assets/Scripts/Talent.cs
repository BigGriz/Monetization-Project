using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Talent : MonoBehaviour, IPointerDownHandler
{

    public TalentSO talent;

    #region Setup
    TMPro.TextMeshProUGUI text;
    Image image;
    TalentUI parent;

    private void Awake()
    {
        parent = GetComponentInParent<TalentUI>();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
    }

    public void SetupTalent(TalentSO _talent)
    {
        talent = _talent;
        SetupTalent();
    }
    #endregion Setup

    void SetupTalent()
    {
        text.SetText(talent.name);
        image.sprite = talent.sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (talent.level < talent.maxLevel && parent.clickable)
        {
            talent.AddLevel();
            text.SetText(talent.GetName());

            CallbackHandler.instance.AddTalent(talent);
            CallbackHandler.instance.UpdateTalents();
            CallbackHandler.instance.ChangeMenu(MENUOPTION.NONE);

            parent.SetUnClickable();
        }
    }
}
