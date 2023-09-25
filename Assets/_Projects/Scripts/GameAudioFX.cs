using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameAudioFX : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource= GetComponent<AudioSource>();
    }
    public AudioSource AudioSource { get => m_AudioSource; set => m_AudioSource = value; }
}
