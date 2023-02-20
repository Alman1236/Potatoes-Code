using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpTimer;
    Coroutine decreaseTimeRoutine;
    float timeLeft;

    static public Timer instance;

    void Awake()
    {
        GenerateInstance();
    }

    void GenerateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There should be only one Timer.cs in every scene! ", gameObject);
            Debug.Break();
        }
    }

    private void Start()
    {
        tmpTimer.text = LevelData.instance.timeToCompleteLevel.ToString("00.00");
    }
    public void StartTimer()
    {
        ResetTimeLeft();
        AudioManager.instance.PlayTimerSound(true);
        decreaseTimeRoutine = StartCoroutine(DecreaseTime());
    }

    public void StopTimer()
    {
        ResetTimeLeft();
        AudioManager.instance.PlayTimerSound(false);
        StopDecreasingTime();
    }

    public void StopDecreasingTime()
    {
        AudioManager.instance.PlayTimerSound(false);
        StopCoroutine(decreaseTimeRoutine);
    }
    
    IEnumerator DecreaseTime()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            timeLeft -= Time.fixedDeltaTime;
            tmpTimer.text = timeLeft.ToString("00.00");
            CheckIfTimeEnded();
        }
    }

    void CheckIfTimeEnded()
    {
        if (timeLeft <= 0)
        {
            StopTimer();
            PhasesManager.instance.OnTimeLeftEnd();
        }
    }

    void ResetTimeLeft()
    {
        timeLeft = LevelData.instance.timeToCompleteLevel;
        tmpTimer.text = timeLeft.ToString("00.00");
    }
}