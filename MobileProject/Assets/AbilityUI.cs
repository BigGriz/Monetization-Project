using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityUI : MonoBehaviour, IPointerDownHandler
{
    public Ability ability;

    public float cost;
    public bool unlocked;
    Image image;
    public Image lockState;

    public void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        CallbackHandler.instance.unlockAbility += UnlockAbility;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.unlockAbility -= UnlockAbility;
    }

    public void UnlockAbility(Ability _ability)
    {
        if (ability == _ability)
            unlocked = true;

        lockState.enabled = !unlocked;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Press();
    }

    public virtual void Press()
    {

    }
}
