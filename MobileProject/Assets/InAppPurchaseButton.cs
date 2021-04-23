using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InAppPurchaseButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        //AudioManager.instance.ToggleAudio();
        //Open IAPMenu;
        CallbackHandler.instance.ChangeMenu(MENUOPTION.IAP);
    }
}
