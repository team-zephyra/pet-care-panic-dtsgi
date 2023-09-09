using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CounterAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]private AudioClip sfxProgressClip;
    [SerializeField]private AudioClip sfxTake;
    [SerializeField]private AudioClip sfxPut;

    private void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();  
        }
        audioSource.clip = sfxProgressClip; 
    }

    public void PlaySFX()
    {
        audioSource.Play();
    }

    public void PlayTake()
    {
        audioSource.PlayOneShot(sfxTake);
    }

    public void PlayPut()
    {
        audioSource.PlayOneShot(sfxPut);
    }
}
