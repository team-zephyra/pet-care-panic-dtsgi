using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]private AudioClip sfxProgressClip;
    [SerializeField]private AudioClip sfxTake;
    [SerializeField]private AudioClip sfxPut;
    [SerializeField]private AudioClip sfxBubble;

    private void Awake()
    {
    }

    private void Start()
    {
        if(audioSource == null)
        {
            audioSource = FindObjectOfType<GameAudioFX>().AudioSource;
        }
        //audioSource.clip = sfxProgressClip; 
    }

    public void PlaySFX()
    {
        audioSource.Play();
    }

    public void PlayOneShot(SfxType type)
    {
        switch(type)
        {
            case SfxType.Put: audioSource.PlayOneShot(sfxPut); break;
            case SfxType.Take: audioSource.PlayOneShot(sfxTake); break;
            case SfxType.Bubble: audioSource.PlayOneShot(sfxBubble); break;
            case SfxType.Progress: audioSource.PlayOneShot(sfxProgressClip); break;
            default: audioSource.PlayOneShot(sfxPut); break;
        }
    }
}

public enum SfxType{
    Progress,
    Take,
    Put,
    Bubble
}
