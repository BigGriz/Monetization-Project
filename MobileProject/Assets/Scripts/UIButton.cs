using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler
{
    [Header("Setup Requirements")]
    public Animator anim;
    public MENUOPTION option;

    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.changeMenu += ChangeMenu;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.changeMenu -= ChangeMenu;
    }
    public void ChangeMenu(MENUOPTION _option)
    {
        anim.SetBool("Show", option == _option ? !anim.GetBool("Show") : false);
    }
    #endregion Callbacks
    #region PointerEvents
    public void OnPointerDown(PointerEventData eventData)
    {
        Press();
    }
    #endregion PointerEvents

    public void Press()
    {
        CallbackHandler.instance.ChangeMenu(option);
    }
}
