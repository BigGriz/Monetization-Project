using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    #region Setup

    TMPro.TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    #endregion Setup
    #region Callbacks
    // Start is called before the first frame update
    void Start()
    {
        CallbackHandler.instance.updateLevel += UpdateLevel;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateLevel -= UpdateLevel;
    }
    #endregion Callbacks

    public void UpdateLevel(string _area, int _stage)
    {
        text.SetText(_area + " - " + _stage);
    }
}
