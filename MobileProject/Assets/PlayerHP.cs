using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : HPBar
{
    Image image;

    public override void Awake()
    {
        image = GetComponent<Image>();
    }

    public override void UpdateHealth(float _hp)
    {
        image.fillAmount = _hp;
    }
}
