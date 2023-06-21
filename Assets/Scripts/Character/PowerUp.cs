using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        SpeedBlue,
        SpeedGreen,
        SpeedRed,
        DmgBlue,
        DmgGreen,
        DmgRed,
        NormalShield,
        AverageShield,
        StrongShield
    }
    [HideInInspector] public PowerUpType powerUpType;
    private float durationEffect = 5f;
    float speed = 1f;
    bool isPickup = false;
    float destroyNotPickUp = 10f;

    private void Start()
    {
        Invoke("StartDestroyTimer", destroyNotPickUp);
    }
    private void Update()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPickup = true;
            Effect(collision.gameObject);
            StartDurationTimer();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Effect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        VisualSystem visualSystem = playerObject.GetComponent<VisualSystem>();
        switch (powerUpType)
        {
            case PowerUpType.SpeedBlue:
                player.IncreaseSpeed(1f);
                break;
            case PowerUpType.SpeedGreen:
                player.IncreaseSpeed(1.5f);
                break;
            case PowerUpType.SpeedRed:
                player.IncreaseSpeed(2f);
                break;
            case PowerUpType.DmgBlue:
                player.IncreaseDamage(1);
                break;
            case PowerUpType.DmgGreen:
                player.IncreaseDamage(2);
                break;
            case PowerUpType.DmgRed:
                player.IncreaseDamage(3);
                break;
            case PowerUpType.NormalShield:
                player.Block(1);
                visualSystem.ActivateShieldUI(powerUpType);
                break;
            case PowerUpType.AverageShield:
                player.Block(2);
                visualSystem.ActivateShieldUI(powerUpType);
                break;
            case PowerUpType.StrongShield:
                player.Block(3);
                visualSystem.ActivateShieldUI(powerUpType);
                break;
        }
    }
    private void StartDurationTimer()
    {
        Invoke("EndDuration", durationEffect);
    }

    private void EndDuration()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject != null)
        {
            Player player = playerObject.GetComponent<Player>();
            switch (powerUpType)
            {
                case PowerUpType.SpeedBlue:
                    player.DecreaseSpeed(1f);
                    break;
                case PowerUpType.SpeedGreen:
                    player.DecreaseSpeed(1.5f);
                    break;
                case PowerUpType.SpeedRed:
                    player.DecreaseSpeed(2f);
                    break;
                case PowerUpType.DmgBlue:
                    player.DecreaseDamage(1);
                    break;
                case PowerUpType.DmgGreen:
                    player.DecreaseDamage(2);
                    break;
                case PowerUpType.DmgRed:
                    player.DecreaseDamage(3);
                    break;
            }
            Destroy(gameObject);

        }
        else
        {
            return;
        }
    }

    private void StartDestroyTimer()
    {
        if(!isPickup)
        {
            Destroy(gameObject);
        }
    }
    private void Move()
    {
        Vector3 movement = Vector2.down * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
