using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints1 : MonoBehaviour
{
    public List<Checkpoint1> checkPoints;

    private void Awake()
    {
        checkPoints = new List<Checkpoint1>(GetComponentsInChildren<Checkpoint1>());
    }
}
