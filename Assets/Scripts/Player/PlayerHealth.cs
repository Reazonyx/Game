using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    [SerializeField] private bool _isAlive = true;
    public float invincibilityTime = 0.5f;
    [SerializeField] private bool isInvincible = false;
    private float timeSinceDamaged = 0;
    public UnityEvent<GameObject, int> playerDamaged;
    public UnityEvent<GameObject, int> playerHealed;
    public UnityEvent<GameObject, int> playerDied;

    private int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    private int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0) IsAlive = false;
        }
    }

    public bool IsAlive
    {
        get => _isAlive;
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimatorTags.isAlive, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (!IsAlive || isInvincible) return;
        Health -= damageAmount;
        isInvincible = true;
        playerDamaged.Invoke(gameObject, damageAmount);
        if (Health<=0)
        {
            PlayerDeath();
        }
    }

    public void Heal(int healAmount)
    {
        if (!IsAlive || isInvincible) return;
        int maxHeal = Mathf.Max(MaxHealth - Health, 0);
        int actualHealingAmount = Mathf.Min(maxHeal, healAmount);
        Health += actualHealingAmount;
        isInvincible = true;
        playerHealed.Invoke(gameObject, healAmount);
    }

    private void Update()
    {
        if (!isInvincible) return;
        if (timeSinceDamaged > invincibilityTime)
        {
            isInvincible = false;
            timeSinceDamaged = 0;
        }
        timeSinceDamaged += Time.deltaTime;
    }

    public void PlayerDeath()
    {
        if (Health > 0)
        {
            Health = -1;
        }
        AudioManager.AudioInstance.PlayClip("GameOver", 1f);
    }
}