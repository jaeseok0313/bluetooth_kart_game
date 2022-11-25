using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public time_record timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CheckpointManager>() != null)
        {
            other.GetComponent<CheckpointManager>().CheckPointReached(this);
            if (other.gameObject.tag == "Player")
            {
                timer.playercheck += other.GetComponent<CheckpointManager>().CurrentCheckpointIndex;

            }
            if (other.gameObject.tag == "Player 2")
            {
                timer.aicheck += other.GetComponent<CheckpointManager>().CurrentCheckpointIndex;

            }

            /*if (other.gameObject.tag == "agent")
            {
                timer.aicheck += other.GetComponent<CheckpointManager>().CurrentCheckpointIndex;

            }
            if (other.gameObject.tag == "agent2")
            {
                timer.aicheck2 += other.GetComponent<CheckpointManager>().CurrentCheckpointIndex;

            }*/
        }
    }

}