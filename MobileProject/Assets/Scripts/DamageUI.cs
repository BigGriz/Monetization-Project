using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    #region Setup
    TMPro.TextMeshProUGUI textComponent;
    private void Awake()
    {
        textComponent = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    #endregion Setup
    #region Callbacks
    // Start is called before the first frame update
    void Start()
    {
        CallbackHandler.instance.updateDamage += UpdateDamage;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateDamage -= UpdateDamage;
    }
    #endregion Callbacks

    public void UpdateDamage(float _damage)
    {
        textComponent.SetText("DPS: " + (Mathf.RoundToInt((_damage) * 100.0f)/100.0f).ToString());
    }
}
