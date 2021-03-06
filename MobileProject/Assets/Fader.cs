using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public delegate void FadeFunc();
    public FadeFunc fadeFunc;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Fade(bool _out)
    {
        animator.SetBool("Fade", _out);
    }

    public void FadeFunction()
    {
        if (fadeFunc != null)
        {
            fadeFunc();
            fadeFunc = null;
        }
    }
}
