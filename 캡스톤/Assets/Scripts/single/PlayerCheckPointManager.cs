using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPointManager : MonoBehaviour
{


    public float MaxTimeToReachNextCheckpoint = 30f;
    public float TimeLeft = 30f;

    public KartAgent kartAgent;
    public Checkpoint1 nextCheckPointToReach;

    public int CurrentCheckpointIndex;
    private List<Checkpoint1> Checkpoints;
    private Checkpoint1 lastCheckpoint;

    public event Action<Checkpoint1> reachedCheckpoint;



    void Start()
    {
        Checkpoints = FindObjectOfType<Checkpoints1>().checkPoints;
        ResetCheckpoints();
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        TimeLeft = MaxTimeToReachNextCheckpoint;

        SetNextCheckpoint();
    }

    private void Update()
    {

        TimeLeft -= Time.deltaTime;

        if (TimeLeft < 0f)
        {
            kartAgent.AddReward(-1f);
            kartAgent.EndEpisode();
        }

    }

    public void CheckPointReached(Checkpoint1 checkpoint)
    {
        if (nextCheckPointToReach != checkpoint) return;

        lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;
        SetNextCheckpoint();
    }

    private void SetNextCheckpoint()
    {
        if (Checkpoints.Count > 0)
        {
            TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];

        }
    }
}