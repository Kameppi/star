﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    public int star, maxstar;
    public float distance, speed;
    public int musicCnt;

    public int[] musicChangeCount
    {
        get; set;
    }

    public static int Star
    {
        get; set;
    }
    public static int Distance
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxstar = 0;
        star = 0;
        musicCnt = 0;
        musicChangeCount = new int[7];
        musicChangeCount[0] = 0;
        musicChangeCount[1] = 30;
        musicChangeCount[2] = 60;
        musicChangeCount[3] = 100;
        musicChangeCount[4] = 150;
        musicChangeCount[5] = 200;
        musicChangeCount[6] = 250;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (star >= musicChangeCount[0] && star < musicChangeCount[1])
        {
            musicCnt = 0;
            speed = 3;
        }
        else if (star < musicChangeCount[2])
        {
            speed = 4;
            musicCnt = 1;
        }
        else if (star < musicChangeCount[3])
        {
            speed = 5;
            musicCnt = 2;
        }
        else if (star < musicChangeCount[4])
        {
            speed = 7;
            musicCnt = 3;
        }
        else if (star < musicChangeCount[5])
        {
            speed = 8;
            musicCnt = 4;
        }
        else if (star < musicChangeCount[6])
        {
            speed = 9;
            musicCnt = 5;
        }
        else
        {
            speed = 10;
        }
        TimeGenerator timeGenerator = GetComponent<TimeGenerator>();
        if (!timeGenerator.cameraMoveNow && Time.timeScale == 1f)
        {
            distance += speed / 30.0f;
            Distance = (int)distance;
        }
        else
        {
            speed = 0;
        }
        if (maxstar <= star)
        {
            maxstar = star;
            Star = maxstar;
        }
    }
}
