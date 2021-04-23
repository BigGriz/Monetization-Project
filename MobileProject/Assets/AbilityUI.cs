using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityUI : MonoBehaviour, IPointerDownHandler
{
    public Ability ability;
    public Image activeImage;
    public bool unlocked;
    Image image;
    public Image lockState;
    public Image validEnergy;

    public void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        ability.timer = 0;
        activeImage.fillAmount = ability.timer / ability.duration;

        CallbackHandler.instance.unlockAbility += UnlockAbility;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.unlockAbility -= UnlockAbility;
    }

    public virtual void Update()
    {
        if (CallbackHandler.instance.settings.paused)
            return;

        validEnergy.enabled = (ability.timer <= 0 && PlayerController.instance.currentEnergy <= ability.cost && unlocked);

        if (ability.timer > 0)
        {
            ability.timer -= Time.deltaTime;
            activeImage.fillAmount = ability.timer / ability.duration;
            return;
        }
        ability.timer = 0;
        activeImage.fillAmount = ability.timer / ability.duration;
    }

    public void UnlockAbility(Ability _ability)
    {
        if (ability == _ability)
            unlocked = true;

        lockState.enabled = !unlocked;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (unlocked)   
            Press();
    }

    public virtual void Press()
    {

    }
}
