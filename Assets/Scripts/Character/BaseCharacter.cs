using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    private GameObject bullet;
    [SerializeField] protected float speed;
    [SerializeField] protected int health;
    [SerializeField] protected int attackDmg;
    protected float shootTimer = 0f;
    protected float timeBetweenShots = 0.27f;
    private float bulletSpeed = 5f;
    private float duration = 2f;
    protected static int score = 0;
    public abstract void Move();

    protected void Shoot()
    {
        bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Move the bullet, can modify move toward player later
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = shootPoint.up * bulletSpeed;

        Destroy(bullet, duration);
    }

    protected abstract void HealthSystem_OnDead(object sender, EventArgs e);

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    public int GetHealth() => health;

    public int GetAttackDmg() => attackDmg;

    public float GetSpeed() => speed;
}
