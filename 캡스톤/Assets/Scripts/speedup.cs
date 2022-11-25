using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedup : MonoBehaviour
{
    public GameObject kart2;
    public int onoff = 0;
    public int onoff2 = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("123");
            onoff = 1;
        }
        if (other.gameObject.tag == "Player 2")
        {
            Debug.Log("123");
            onoff2 = 1;
        }
    }
}