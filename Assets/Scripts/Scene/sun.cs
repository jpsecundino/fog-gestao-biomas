using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class sun : MonoBehaviour
{
    public float time;
    public TimeSpan currenttime;
    public Transform SunMoonTransform;
    public Light Sun;
    public Text timetext;
    public Text daystext;
    public int days;

    public float intensity;
    public float sunOrMoon;
    public Color fogday = Color.gray;
    public Color fognight = Color.black;

    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        SunMoonTransform.rotation = Quaternion.Euler(new Vector3(sunOrMoon * ((time - 21600) / 86400 * 360), 0, 0));
        if (time > 43200)
            intensity = 1 - (43200 - time) / 43200;
        else
            intensity = 1 - ((43200 - time) / 43200 * -1);

        RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity * intensity);

        Sun.intensity = intensity;

    }
}