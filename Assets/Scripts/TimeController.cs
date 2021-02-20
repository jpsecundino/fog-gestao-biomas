using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float time = 0f;
    public TimeSpan currenttime;
    public Text timetext;
    public Text daystext;

    public int days;
    public int speed;


    // Update is called once per frame
    void Update()
    {
        ChangeTime();
    }

    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if (time > 86400)
        {
            days += 1;
            time = 0;
        }

        currenttime = TimeSpan.FromSeconds(time);
        string[] temptime = currenttime.ToString().Split(":"[0]);
        timetext.text = temptime[0] + ":" + temptime[1];
        string daysstr = days.ToString();
        daystext.text = daysstr;

    }
}
