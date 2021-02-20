using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SunAndMoon : MonoBehaviour
{

    public Nature nature;
  
    public Transform SunMoonTransform;
    public Light Sun;

    public float intensity;
    public float sunOrMoon;
    public Color fogday = Color.gray;
    public Color fognight = Color.black;

    public TimeController timecontroller;

    private void Start()
    {
        timecontroller = GameObject.Find("TimeController").GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {   
        SunMoonTransform.rotation = Quaternion.Euler(new Vector3(sunOrMoon * ((timecontroller.time - 21600) / 86400 * 360), 0, 0));
        if (timecontroller.time > 43200)
            intensity = 1 - (43200 - timecontroller.time) / 43200;
        else
            intensity = 1 - ((43200 - timecontroller.time) / 43200 * -1);

        RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity * intensity);

        Sun.intensity = intensity;
    }

    
}
