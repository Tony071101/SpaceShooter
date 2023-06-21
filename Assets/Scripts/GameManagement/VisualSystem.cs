using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static PowerUp;

public class VisualSystem : MonoBehaviour
{
    [SerializeField] private GameObject deathAnim;
    [SerializeField] private GameObject shieldPrefab_1;
    [SerializeField] private GameObject shieldPrefab_2;
    [SerializeField] private GameObject shieldPrefab_3;
    [SerializeField]
    private HealthSystem healthSystem;
    private GameObject activeShieldUI;

    private void Start()
    {
        healthSystem.OnDead += DeathAnimation;
    }

    public void DeathAnimation(object sender, EventArgs e)
    {
        // Instantiate the death animation prefab
        GameObject deathPrefab = Instantiate(deathAnim, transform.position, Quaternion.identity);
        Animator animator = deathPrefab.GetComponent<Animator>();

        // Get the duration of the death animation
        float deathAnimDuration = animator.GetCurrentAnimatorStateInfo(0).length;

        // Destroy the death animation object after the animation duration
        Destroy(deathPrefab, deathAnimDuration);
    }

    public void ActivateShieldUI(PowerUp.PowerUpType powerUpType)
    {
        // Destroy the existing shield UI, if any
        if (activeShieldUI != null)
        {
            if (powerUpType == PowerUpType.NormalShield)
            {
                if(powerUpType == PowerUpType.AverageShield)
                {
                    DeactivateShieldUI();
                }
                return;
            }
            else if(powerUpType == PowerUpType.AverageShield)
            {
                if(powerUpType == PowerUpType.StrongShield)
                {
                    DeactivateShieldUI();
                }
                return;
            }
            DeactivateShieldUI();
        }

        // Instantiate the appropriate shield UI prefab
        GameObject shieldPrefab = null;
        switch (powerUpType)
        {
            case PowerUpType.NormalShield:
                shieldPrefab = shieldPrefab_1;

                break;
            case PowerUpType.AverageShield:
                shieldPrefab = shieldPrefab_2;

                break;
            case PowerUpType.StrongShield:
                shieldPrefab = shieldPrefab_3;
                break;
        }

        if (shieldPrefab != null)
        {
            activeShieldUI = Instantiate(shieldPrefab, transform);
        }
    }

    public void DeactivateShieldUI()
    {
        if (activeShieldUI != null)
        {
            Destroy(activeShieldUI);
            activeShieldUI = null;
        }
    }
}
