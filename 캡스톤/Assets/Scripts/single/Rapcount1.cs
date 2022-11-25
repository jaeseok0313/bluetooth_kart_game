using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapcount1 : MonoBehaviour
{
    public GameManager1 manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//플레이어
        {
            manager.Player_rap += 1;

            if (manager.Player_rap == 1)
            {
                manager.cur_Rap.text = "1/2";
            }
            else if (manager.Player_rap == 2)
            {
                manager.cur_Rap.text = "2/2";
            }
            else if (manager.Player_rap == 3)
            {
                manager.state1 = State1.RacingDone;
                manager.playerGameEnd();


            }
        }
        if (other.gameObject.tag == "agent") //ai 1번
        {
            manager.Ai_rap += 1;

            if (manager.Ai_rap == 3)
            {
                manager.state1 = State1.RacingDone;
                manager.playerGameEnd();
            }
        }
        if (other.gameObject.tag == "agent2")//ai2번
        {
            manager.Ai_rap2 += 1;

            if (manager.Ai_rap2 == 3)
            {
                manager.state1 = State1.RacingDone;
                manager.playerGameEnd();
            }
        }
        /*if (other.gameObject.name == "Player")//플레이어
        {
            manager.Player_rap += 1;

            if (manager.Player_rap == 1)
            {
                manager.cur_Rap.text = "1/2";
            }
            else if (manager.Player_rap == 2)
            {
                manager.cur_Rap.text = "2/2";
            }
            else if (manager.Player_rap == 3)
            {
                manager.playerGameEnd();
                manager.state1 = State1.RacingDone;
            }
        }
        if (other.gameObject.name == "Player 1") //ai 1번
        {
            manager.Ai_rap += 1;

            if (manager.Ai_rap == 3)
            {
                manager.aiGameEnd();
                manager.state1 = State1.RacingDone;
            }
        }*/
    }
}