using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endzone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_Overworld") || other.CompareTag("Player_Ghost"))
        {
            LevelManager.instance.WonRound();
        }
    }
}
