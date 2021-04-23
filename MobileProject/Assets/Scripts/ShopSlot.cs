using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerDownHandler
{
    #region Setup
    public Image image;
    List<TMPro.TextMeshProUGUI> textComponents;
    private void Awake()
    {
        textComponents = new List<TMPro.TextMeshProUGUI>();
        foreach (TMPro.TextMeshProUGUI n in GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            textComponents.Add(n);
        }
    }
    #endregion Setup
    Ability ability;
    public GameObject locked;

    private void Start()
    {
        SetupAbility(ability);
        locked.SetActive(false);
    }

    private void Update()
    {
        textComponents[2].color = (PlayerInventory.instance.coins < ability.cost) ? Color.red : Color.white;
    }

    public void SetupAbility(Ability _ability)
    {
        ability = _ability;

        if (ability)
        {
            image.enabled = true;
            image.sprite = ability.sprite;
            textComponents[0].SetText(ability.name);
            textComponents[1].SetText("");
            textComponents[2].SetText(ability.cost.ToString());

            return;
        }

        image.enabled = false;
        textComponents[0].SetText("");
        textComponents[1].SetText("");
        textComponents[2].SetText("");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!ability)
            return;

        if (!locked.activeSelf && PlayerInventory.instance.SpendCoin(ability.cost))
        {
            CallbackHandler.instance.UnlockAbility(ability);
            locked.SetActive(true);
        }
    }
}
