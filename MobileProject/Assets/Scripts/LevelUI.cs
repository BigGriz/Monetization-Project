using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    #region Setup
    List<TMPro.TextMeshProUGUI> textComponents = new List<TMPro.TextMeshProUGUI>();
    private void Awake()
    {
        foreach(TMPro.TextMeshProUGUI n in GetComponentsInChildren<TMPro.TextMeshProUGUI>())
        {
            textComponents.Add(n);
        }
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

    public void UpdateLevel(string _area, int _level)
    {
        textComponents[0].SetText(_area);
        textComponents[1].SetText(_level.ToString());
    }
}
