using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : BaseCharacter
{
    private PowerUpSpawner powerUpSpawner;
    [SerializeField] private HealthSystem healthSystem;
    private UIManager scoreUI;
    private Player player;
    [HideInInspector] public float powerUpSpawnChance;
    private float shootDuration = 1.5f;
    float elapsedTime = 0f;
    private GameManager gameManager;
    private void Start()
    {
        healthSystem.OnDead += HealthSystem_OnDead; 
        powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
        scoreUI = FindObjectOfType<UIManager>();
        StartCoroutine(ShootDelay(shootDuration));
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        Vector3 movement = Vector2.down * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private IEnumerator ShootDelay(float delay)
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= delay)
            {
                Shoot();    
                elapsedTime = 0f;
            }
            yield return null;
        }
    }

    //Enemy Take Damage
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            int playerAttackDmg = player.GetAttackDmgToEnemy();
            healthSystem.Damage(playerAttackDmg);
            Destroy(collision.gameObject);
        }
    }

    protected override void HealthSystem_OnDead(object sender, EventArgs e)
    {
        score++;
        scoreUI.IncreaseScore(score);
        powerUpSpawnChance = 0.2f;
        if (UnityEngine.Random.value < powerUpSpawnChance)
        {
            powerUpSpawner.SpawnPowerUp(transform.position);
        }
        Destroy(gameObject);
        if(score == 100)
        {
            score = 0;
            gameManager.NextLevel();
        }
    }

    public int GetAttackDmgToPlayer() => this.attackDmg;

    public int GetScore() => score;
}
