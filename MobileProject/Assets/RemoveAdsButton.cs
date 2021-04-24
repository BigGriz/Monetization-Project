using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveAdsButton : MonoBehaviour, IPointerDownHandler
{
    public static RemoveAdsButton instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("noAdsPurchased") == 1)
        {
            ChangeText("Purchased!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IAPManager.instance.BuyProductID("removeads");
        AudioManager.instance.PlayAudio("UI");
    }

    public TMPro.TextMeshProUGUI text;
    public void ChangeText(string _string)
    {
        text.SetText(_string);
    }
}
