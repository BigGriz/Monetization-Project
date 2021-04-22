using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("Setup Fields")]
    public List<Ability> ability;
    public GameObject shopSlotPrefab;
    public GameObject layoutGroup;

    // Local Variables
    Animator anim;
    MENUOPTION option = MENUOPTION.SHOP;

    #region Setup
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        CallbackHandler.instance.changeMenu += ChangeMenu;

        foreach (Ability n in ability)
        {
            ShopSlot temp = Instantiate(shopSlotPrefab, layoutGroup.transform).GetComponent<ShopSlot>();
            temp.SetupAbility(n);
        }
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.changeMenu -= ChangeMenu;
    }
    #endregion Setup

    public void ChangeMenu(MENUOPTION _option)
    {
        anim.SetBool("Show", option == _option ? true : false);
    }

    public void CloseShop()
    {
        ChangeMenu(MENUOPTION.NONE);
        CallbackHandler.instance.TogglePause(false);
    }
}
