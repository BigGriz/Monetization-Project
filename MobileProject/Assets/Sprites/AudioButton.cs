using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioButton : MonoBehaviour, IPointerDownHandler
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

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.ToggleAudio();
        SwitchSprite();
    }
}
