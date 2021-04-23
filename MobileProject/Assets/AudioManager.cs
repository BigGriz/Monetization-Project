using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        AudioSource[] sources = GetComponentsInChildren<AudioSource>();
        foreach(AudioSource n in sources)
        {
            audio.Add(n.gameObject.name, n);
        }
    }

    Dictionary<string, AudioSource> audio = new Dictionary<string, AudioSource>();
    public List<AudioClip> deathSFX;
    public List<AudioClip> swordSFX;

    public void PlayAudio(string _name)
    {
        if (_name == "Death")
        {
            int rand = Random.Range(0, deathSFX.Count);
            audio[_name].clip = deathSFX[rand];
        }
        if (_name == "Sword")
        {
            int rand = Random.Range(0, swordSFX.Count);
            audio[_name].clip = swordSFX[rand];
        }

        if (!audio[_name].isPlaying)
            audio[_name].Play();
    }

    public void StopAudio(string _name)
    {
        audio[_name].Stop();
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    bool audioOn = true;
    public void ToggleAudio()
    {
        audioOn = !audioOn;
    }
}
