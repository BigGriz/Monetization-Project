using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    Vector3 localScale;

    private void Awake()
    {
        localScale = transform.localScale;
    }

    public void UpdateHealth(float _hp)
    {
        localScale.x = _hp;
        transform.localScale = localScale;
    }
}
