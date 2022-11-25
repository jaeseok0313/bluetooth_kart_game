using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapcount : MonoBehaviour
{
    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//플레이어
        {
            manager.Player_rap += 1;

            if (manager.Player_rap == 2)
            {
                manager.playerGameEnd();
                manager.state = State.RacingDone;
            }
        }
        if (other.gameObject.tag == "Player 2") //플레이어 2
        {
            manager.Ai_rap += 1;

            if (manager.Ai_rap == 2)
            {
                manager.aiGameEnd();
                manager.state = State.RacingDone;
            }
        }
    }
}