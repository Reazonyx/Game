using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject MissionCompleted;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // MissionCompleted.setActive(true);
            AudioManager.AudioInstance.musicSource.Stop();
            AudioManager.AudioInstance.PlayClip("Win", 1f);
        }
    }
}
