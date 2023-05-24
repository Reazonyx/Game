using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTile : MonoBehaviour
{
    public int healAmount = 10;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
    }
}