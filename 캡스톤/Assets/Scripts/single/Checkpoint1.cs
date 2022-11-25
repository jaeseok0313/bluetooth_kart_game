using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    public time_record1 timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CheckpointManager1>() != null)
        {
            other.GetComponent<CheckpointManager1>().CheckPointReached(this);

            if (other.gameObject.tag == "agent")
            {
                timer.aicheck += other.GetComponent<CheckpointManager1>().CurrentCheckpointIndex;

            }
            if (other.gameObject.tag == "agent2")
            {
                timer.aicheck2 += other.GetComponent<CheckpointManager1>().CurrentCheckpointIndex;

            }
        }
        if (other.GetComponent<PlayerCheckPointManager>() != null)
        {
            other.GetComponent<PlayerCheckPointManager>().CheckPointReached(this);
            if (other.gameObject.tag == "Player")
            {
                timer.playercheck += other.GetComponent<PlayerCheckPointManager>().CurrentCheckpointIndex;

            }
        }

    }

}