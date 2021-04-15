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
    Gear gear;

    private void Start()
    {
        EquipItem(gear);
    }

    public void EquipItem(Gear _gear)
    {
        gear = _gear;

        if (gear)
        {
            image.enabled = true;
            image.sprite = gear.sprite;
            textComponents[0].SetText(gear.name);
            textComponents[1].SetText(gear.damage.ToString());
            textComponents[2].SetText(gear.cost.ToString());

            return;
        }

        image.enabled = false;
        textComponents[0].SetText("");
        textComponents[1].SetText("");
        textComponents[2].SetText("");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!gear)
            return;

        if (PlayerInventory.instance.SpendCoin(gear.cost))
            CallbackHandler.instance.AddGear(Instantiate(gear));
    }
}
