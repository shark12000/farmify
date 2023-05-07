using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [SerializeField] private float secondsPerGameMinute = 60f;
    [SerializeField] private float secondsPerGameHour = 3600f;
    [SerializeField] private float secondsPerGameDay = 86400f;

    private float _elapsedSeconds;

    private int _currentDay;
    private int _currentHour;
    private int _currentMinute;

    public int CurrentDay => _currentDay;
    public int CurrentHour => _currentHour;
    public int CurrentMinute => _currentMinute;

    public event Action OnDayChanged;
    public event Action OnHourChanged;
    public event Action OnMinuteChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _elapsedSeconds += Time.deltaTime;

        if (_elapsedSeconds >= secondsPerGameMinute)
        {
            _elapsedSeconds -= secondsPerGameMinute;
            _currentMinute++;

            if (_currentMinute >= 60)
            {
                _currentMinute = 0;
                _currentHour++;

                if (_currentHour >= 24)
                {
                    _currentHour = 0;
                    _currentDay++;

                    if (OnDayChanged != null) OnDayChanged();
                }

                if (OnHourChanged != null) OnHourChanged();
            }

            if (OnMinuteChanged != null) OnMinuteChanged();
        }
    }

    public DateTime GetDateTime()
    {
        return new DateTime(1, 1, _currentDay, _currentHour, _currentMinute, 0);
    }

    public float GetSecondsPerGameMinute()
    {
        return secondsPerGameMinute;
    }

    public float GetSecondsPerGameHour()
    {
        return secondsPerGameHour;
    }

    public float GetSecondsPerGameDay()
    {
        return secondsPerGameDay;
    }
}
