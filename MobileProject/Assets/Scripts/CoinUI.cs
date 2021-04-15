using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
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
        CallbackHandler.instance.updateCoins += UpdateCoins;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateCoins -= UpdateCoins;
    }
    #endregion Callbacks

    public void UpdateCoins(int _coins)
    {
        textComponent.SetText(_coins.ToString());
    }
}
