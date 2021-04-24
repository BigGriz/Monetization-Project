using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPTextUI : MonoBehaviour
{
    public static HPTextUI instance;


    TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        instance = this;

        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetText(int _hp, int _maxHp)
    {
        text.SetText(_hp + "/" + _maxHp);
    }
}
