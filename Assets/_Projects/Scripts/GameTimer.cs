using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public int timer = 0;
    public int durationMax = 120;
    public bool pauseTimer = false;

    private IEnumerator _timerCoroutine;
    public Action OnTimeOut;

    [Header("UI Setting")]
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        StartTimer(TimeOut);
    }

    private void TimeOut()
    {
        GameManager.instance.GameOver();
        Debug.Log("Your timer is up.");
    }

    public void StartTimer(Action onTimeOut)
    {
        OnTimeOut = onTimeOut;
        _timerCoroutine = TimeCounter();
        StartCoroutine(TimeCounter());
    }

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
            OnTimeOut = null;
        }
    }

    private IEnumerator TimeCounter()
    {
        while (timer < durationMax)
        {
            if (pauseTimer)
            {
                // wait a while before continue to avoid infinite loop
                yield return new WaitForEndOfFrame();
                continue;
            }

            //waiting 1 second in real time and increasing the timer value
            yield return new WaitForSecondsRealtime(1);
            timer++;
            UpdateTimer();
           
        }
        OnTimeOut?.Invoke();
    }

    

    private void UpdateTimer()
    {
        string minSec = string.Format("{0}:{1:00}", timer/60, timer%60);
        timerText.text = minSec;
    }
}
