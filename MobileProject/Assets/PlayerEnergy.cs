using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    Image image;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateEnergy(float _en)
    {
        image.fillAmount = _en;
    }
}
