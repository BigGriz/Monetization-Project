using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    #region Setup
    Image image;
    TMPro.TextMeshProUGUI text;
    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    #endregion Setup
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.updateXP += UpdateXP;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateXP -= UpdateXP;
    }
    #endregion Callbacks

    public void UpdateXP(int _level, float _xp)
    {
        image.fillAmount = _xp;
        text.SetText("LEVEL " + _level.ToString() + "  -  " + Mathf.FloorToInt(_xp * 100.0f).ToString() + "%");
    }
}
