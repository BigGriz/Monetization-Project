using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public Sprite play;
    public Sprite mute;

    UnityEngine.UI.Image image;
    private void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    public void SwitchSprite()
    {
        image.sprite = (image.sprite == play) ? mute : play;
    }
}
