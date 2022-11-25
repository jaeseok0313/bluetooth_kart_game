using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class time_record : MonoBehaviour
{
    public float time;
    int min;
    public TextMeshProUGUI[] Clocktext;
    public TextMeshProUGUI[] Clocktext1;
    bool Timeon = false;
    //public CheckpointManager ranking;
    public TextMeshProUGUI[] ranking;

    public GameManager manager;

    public int playercheck;
    public int aicheck;
    public int aicheck2;
    int max;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Timeon)
        {
            time += Time.deltaTime;
            TimeSpan timespan = TimeSpan.FromSeconds(time);
            Clocktext[0].text = string.Format("{0:D2}:{1:D2}:{2:##}", timespan.Minutes,
                timespan.Seconds, timespan.Milliseconds);
        }

        /*if (maxx(playercheck, aicheck, aicheck2) == 1) //플레이어가 1등이면
        {
            ranking[0].text = "player";
            if (max2(aicheck, aicheck2) == 2)
            {
                ranking[1].text = "ai_1";
                ranking[2].text = "ai_2";
            }
            else
            {
                ranking[1].text = "ai_2";
                ranking[2].text = "ai_1";
            }
        }
        else if (maxx(playercheck, aicheck, aicheck2) == 2) //ai_1이 1등이면
        {
            ranking[0].text = "ai_1";
            if (max2(playercheck, aicheck2) == 2)
            {
                ranking[1].text = "player";
                ranking[2].text = "ai_2";
            }
            else
            {
                ranking[1].text = "ai_2";
                ranking[2].text = "player";
            }
        }
        else if (maxx(playercheck, aicheck, aicheck2) == 3) //ai_2이 1등이면
        {
            ranking[0].text = "ai_2";
            if (max2(playercheck, aicheck) == 2)
            {
                ranking[1].text = "player";
                ranking[2].text = "ai_1";
            }
            else
            {
                ranking[1].text = "ai_1";
                ranking[2].text = "player";
            }
        }*/
        if (playercheck>=aicheck)
        {
            ranking[0].text = "Player 1";
            ranking[1].text = "Player 2";
            if(manager.Player_rap==1)
                manager.rap[0].text = "2/2";
            if (manager.Ai_rap == 1)
                manager.rap[1].text = "2/2";


        }
        if(playercheck<aicheck)
        {
            ranking[0].text = "Player 2";
            ranking[1].text = "Player 1";
            if (manager.Ai_rap == 1)
                manager.rap[0].text = "2/2";
            if (manager.Player_rap == 1)
                manager.rap[1].text = "2/2";
        }
    }
    public void time_on()
    {
        Timeon = true;
    }
    public void time_off()
    {
        Timeon = false;
        Clocktext1[0].text = Clocktext[0].text;
    }
    int maxx(int n, int n2, int n3)
    {
        if (n > n2 && n > n3)
        {
            return 1;
        }
        else if (n2 > n && n2 > n3)
        {
            return 2;
        }
        else
            return 3;
    }
    int max2(int n, int n2)
    {
        if (n >= n2)
        {
            return 2;

        }
        else
            return 3;
    }


}