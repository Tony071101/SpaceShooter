using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;

public class Player : BaseCharacter
{
    private new Camera camera;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private VisualSystem visualSystem;
    private Enemy enemy;
    private int shieldStrength = 0;
    private float cameraHeight;
    private float cameraWidth;
    private float cameraLeftBound;
    private float cameraRightBound;
    private float cameraBottomBound;
    private float cameraTopBound;
    private float clampedX;
    private float clampedY;
    private bool isBlocking;
    private bool speedIncreased = false;
    private bool attackIncreased = false;
    private void Start()
    {
        healthSystem.OnDead += HealthSystem_OnDead;

        //Get Camera bound
        camera = Camera.main;
        cameraHeight = camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;
        cameraLeftBound = camera.transform.position.x - cameraWidth;
        cameraRightBound = camera.transform.position.x + cameraWidth;
        cameraBottomBound = camera.transform.position.y - cameraHeight;
        cameraTopBound = camera.transform.position.y + cameraHeight;

        enemy = FindObjectOfType<Enemy>();
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        Move();

        if(Input.GetMouseButton(0) && shootTimer >= timeBetweenShots)
        {
            Shoot();
            shootTimer = 0f;
        }
        
    }

    public override void Move()
    {
        // Retrieve input values
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0f) * speed * Time.deltaTime;

        //Keep character inside camera bound
        Vector3 targetPosition = transform.position + movement;
        clampedX = Mathf.Clamp(targetPosition.x, cameraLeftBound + 0.5f, cameraRightBound - 0.5f);
        clampedY = Mathf.Clamp(targetPosition.y, cameraBottomBound + 0.5f, cameraTopBound - 0.5f);
        targetPosition = new Vector3(clampedX, clampedY, 0f);

        // Apply movement to the game object
        Vector3 adjustedMovement = targetPosition - transform.position;
        transform.Translate(adjustedMovement);
    }

    //Character Take Damage
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            if (isBlocking)
            {
                shieldStrength--;
                if(shieldStrength > 0)
                {
                    Destroy(collision.gameObject);
                    return;
                }
                isBlocking = !isBlocking;
                visualSystem.DeactivateShieldUI();
            }
            else
            {
                int enemyAttackDmg = enemy.GetAttackDmgToPlayer();
                healthSystem.Damage(enemyAttackDmg);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            if(isBlocking)
            {
                shieldStrength = 0;
                isBlocking = !isBlocking;
                visualSystem.DeactivateShieldUI();
            }
            else
            {
                healthSystem.Damage(10); //For instant death, 10 is just to be sure
            }
        }
    }

    protected override void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Destroy(gameObject);
        FindObjectOfType<GameManager>().GameOver();
        score = 0;
    }

    public void IncreaseSpeed(float factor)
    {
        if (!speedIncreased)
        {
            speed += factor;
            speedIncreased = true;
        }
    }

    public void DecreaseSpeed(float factor)
    {
        if (speedIncreased)
        {
            speed -= factor;
            speedIncreased = false;
        }
    }
    public void IncreaseDamage(int amount)
    {
        if (!attackIncreased)
        {
            attackDmg += amount;
            attackIncreased = true;
        }
    }

    public void DecreaseDamage(int amount)
    {
        if (attackIncreased)
        {
            attackDmg -= amount;
            attackIncreased = false;
        }
    }

    public void Block(int amount)
    {
        if (amount > shieldStrength)
        {
            isBlocking = true;
            shieldStrength = amount;
        }
    }

    public int GetAttackDmgToEnemy() => this.attackDmg;
}
