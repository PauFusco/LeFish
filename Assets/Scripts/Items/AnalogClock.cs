using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    [SerializeField]
    private Transform _hourHand;
    
    [SerializeField]
    private Transform _minuteHand;
    
    [SerializeField]
    private Transform _secondHand;

    private int _previousSeconds;
    private int _timeInSeconds;

    public float jumpValue = 1f;

    // Update is called once per frame
    void Update()
    {
        ConvertTimeToSeconds();
        RotateClockHands();
    }

    private int ConvertTimeToSeconds()
    {
        int currentSeconds = DateTime.Now.Second;
        int currentMinutes = DateTime.Now.Minute;
        int currentHour = DateTime.Now.Hour;

        if (currentHour >= 12)
        {
            currentHour -= 12;
        }

        _timeInSeconds = currentSeconds + (currentMinutes*60) + (currentHour * 60 * 60);
        return _timeInSeconds;
    }
    private void RotateClockHands()
    {
        float secondHandPerSecond = 360f / 60f;
        float minuteHandPerSecond = 360f / (60f * 60f);
        float hourHandPerSecond = 360f / (60f * 60f * 12f);

        if (_timeInSeconds != _previousSeconds)
        {
            _secondHand.localRotation = Quaternion.Euler(0, 0, _timeInSeconds * secondHandPerSecond * jumpValue);
            _minuteHand.localRotation = Quaternion.Euler(0, 0, _timeInSeconds * minuteHandPerSecond * jumpValue);
            _hourHand.localRotation = Quaternion.Euler(0, 0, _timeInSeconds * hourHandPerSecond * jumpValue);
        }
        _previousSeconds = _timeInSeconds;
    }
}
