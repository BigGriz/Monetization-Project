using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPShop : MonoBehaviour
{
    // Local Variables
    Animator anim;
    MENUOPTION option = MENUOPTION.IAP;

    #region Setup
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        CallbackHandler.instance.changeMenu += ChangeMenu;
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
