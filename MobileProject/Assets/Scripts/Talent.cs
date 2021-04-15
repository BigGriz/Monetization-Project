using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TalentType
{
    Passive,
    Active
}

public class Talent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TalentType type;
    public Talent talentDependency;
    TMPro.TextMeshProUGUI text;

    public TalentSO talent;

    private void Awake()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.SetText(talent.level.ToString());
        talent = Instantiate(talent);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (talent.level < talent.maxLevel && PlayerInventory.instance.SpendTalentPoint())
        {
            if (talent.level == 0 && type == TalentType.Active)
            {
                // Add to Action Bar
                talent.AddLevel();
                text.SetText(talent.level.ToString());
                Tooltip.instance.SetText(talent);
                return;
            }
            talent.AddLevel();
            text.SetText(talent.level.ToString());
            Tooltip.instance.SetText(talent);
            CallbackHandler.instance.UpdateTalents();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.instance.SetText(talent);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.SetEmpty();
    }
}
