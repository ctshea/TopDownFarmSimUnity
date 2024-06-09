using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ClockControl : MonoBehaviour
{
    public TextMeshProUGUI TMPro;
    public float timeScale = 1f;
    public int dayStartTime = 6;

    bool timeActive = true;
    int year, day, hour, minute;
    string amPm = "am";
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime(0, 1, 6, 0);
        StartCoroutine(TimeUpdate());
        
    }

    private void Update()
    {
        TMPro.SetText("day: " +day + "    " + hour + ":" + minute.ToString("D2") + " " + amPm);
        if (Input.GetButtonDown("Jump"))
        {
            clockStateSwitch();
        }
    }

    public void currentTime(int year, int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
        this.year = year;
    }

    public void clockStateSwitch()
    {
        if (timeActive) timeActive = false;
         else timeActive = true;
    }


    public void updateClock()
    {
        if (timeActive) minute++;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour == 12 && minute == 0)
        {
            if (amPm == "am") amPm = "pm";
            else amPm = "am";
        }
        else if (hour == 13) hour = 1;
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
           /* while (!timeActive)
            {
                yield return null;
            }*/
            yield return new WaitForSeconds(timeScale);
            updateClock();
        }

    }

    public int getDay()
    {
        return day;
    }

    public void incrementDay()
    {
        day++;
        hour = 6;
        minute = 0;
    }

}
