using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDown : MonoBehaviour
{
    [Header("UI Properties")]
    [SerializeField] private Transform panelBlocker;
    [SerializeField] private Image ImgTextNotif;
    [SerializeField] private Image ImgNumberCounter;

    [Header("Setup")]
    [SerializeField] private Sprite[] counterSprite;
    [SerializeField] private Sprite[] notifSprite;
    [SerializeField] private AudioClip countdownClip;
    [SerializeField] private AudioClip startClip;

    [SerializeField] private AudioSource aud;
    [SerializeField] private AudioSource inGameMusic;

    private void Start()
    {
        if(aud == null)
        {
            aud = GameManager.instance.audioSource;
        }
    }

    public void StartCountDown()
    {
        panelBlocker.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }

    public void UpdateCounter(int _number)
    {
        ImgNumberCounter.sprite = counterSprite[_number-1];
    }

        
    public void UpdateTextNotif(int _idx)
    {
        /* 0 for Start
         * 1 for TimesUp
         * 2 for Finish
         */

        ImgTextNotif.sprite = notifSprite[_idx];
    }

    public void ShowStart()
    {
        UpdateTextNotif(0);
        StartCoroutine(ShowImage(ImgTextNotif, 1));
        StartInGameMusic();
    }

    public void ShowTimesUp()
    {
        UpdateTextNotif(1);
        panelBlocker.gameObject.SetActive(true);
        StartCoroutine(ShowImage(ImgTextNotif, 3));
    }

    public void ShowFinish()
    {
        UpdateTextNotif(2);
        panelBlocker.gameObject.SetActive(true);
        StartCoroutine(ShowImage(ImgTextNotif, 3));
    }

    private IEnumerator CountDown()
    {
        ImgNumberCounter.enabled = true;
        int cd = 3;
        while (cd > 0)
        {
            ImgNumberCounter.sprite = counterSprite[cd-1];
            aud.PlayOneShot(countdownClip);
            yield return new WaitForSeconds(1);
            cd--;
        }

        ImgNumberCounter.enabled = false;
        ShowStart();
        
    }

    private IEnumerator ShowImage(Image _image, int _time)
    {
        ImgTextNotif.enabled = false;
        ImgNumberCounter.enabled = false;

        aud.PlayOneShot(startClip);
        _image.enabled = true;

        
        yield return new WaitForSeconds(_time);
        panelBlocker.gameObject.SetActive(false);

        _image.enabled = false;
    }

    private void StartInGameMusic()
    {
        if (!inGameMusic.isPlaying)
        {
            inGameMusic.Play();
        }
    }
}
