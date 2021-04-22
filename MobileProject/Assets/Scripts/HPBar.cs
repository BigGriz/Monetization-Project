using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    Vector3 localScale;

    public virtual void Awake()
    {
        localScale = transform.localScale;
    }

    public virtual void UpdateHealth(float _hp)
    {
        localScale.x = _hp;
        transform.localScale = localScale;
    }
}
