using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearSlot : MonoBehaviour, IPointerDownHandler
{
    #region Setup
    public Image image;
    public Image lockedImage;
    public Image costImage;
    List<TMPro.TextMeshProUGUI> textComponents;
    private void Awake()
    {
        textComponents = new List<TMPro.TextMeshProUGUI>();
        foreach(TMPro.TextMeshProUGUI n in GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            textComponents.Add(n);
        }
    }

    public bool locked;
    public Gear gear;
    private void Start()
    {
        gear = Instantiate(gear);
        // Equip Gear (if any)
        if (locked)
        {
            LockGear();
        }
        else
        {
            UnlockGear();
        }
        //EquipItem(gear);
    }

    #endregion Setup
    #region Utility
    public bool HasGear() { return (gear); }
    #endregion Utility

    private void Update()
    {
        if (locked)
        {
            if (PlayerInventory.instance.level >= gear.levelRequirement)
            {
                UnlockGear();
            }
        }

        if (!locked)
        {
            textComponents[3].color = (PlayerInventory.instance.coins < gear.upgradeCost) ? Color.red : Color.white;
        }
    }

    public void LockGear()
    {
        if (gear)
        {
            lockedImage.enabled = true;
            image.enabled = true;
            image.sprite = gear.sprite;
            costImage.enabled = false;
            textComponents[0].SetText(gear.name);
            textComponents[1].SetText("");
            textComponents[2].SetText("");
            textComponents[3].SetText("");

            return;
        }
    }

    public void UnlockGear()
    {
        locked = false;
        lockedImage.enabled = false;
        costImage.enabled = true;
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
            textComponents[1].SetText("LVL: " + gear.level.ToString());
            textComponents[2].SetText("DMG: " + gear.damage.ToString());
            textComponents[3].SetText(gear.upgradeCost.ToString());

            return;
        }

        image.enabled = false;
        textComponents[0].SetText("");
        textComponents[1].SetText("");
        textComponents[2].SetText("");
        textComponents[3].SetText("");
    }

    #region PointerEvents
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!gear || locked)
            return;

        // If Gear - Upgrade & Update UI Elements
        gear.Upgrade();
        EquipItem(gear);

        CallbackHandler.instance.UpdateUI();
        AudioManager.instance.PlayAudio("UI");
    }
    #endregion PointerEvents

}
