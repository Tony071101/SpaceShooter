using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    private int health;

    private void Start()
    {
        health = GetComponent<BaseCharacter>().GetHealth();
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
            OnDead?.Invoke(this, EventArgs.Empty);

        if (gameObject.CompareTag("Player"))
        {
            UIManager.Instance.HealthUI(health);
        }
    }

    public int GetHeathSystem()
    {
        return health = FindObjectOfType<Player>().GetHealth();
    }
}
