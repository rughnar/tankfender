using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime = 2f;
    [SerializeField] private float timeBetweenShoots = 0.5f;
    [SerializeField] private Transform shootOrigin;


    private float lastShootTime = 0f;



    void Awake()
    {
        lastShootTime = 0f;
    }

    void FixedUpdate()
    {
        if (Time.fixedTime - lastShootTime > timeBetweenShoots) Shoot();
        Move();
    }

    void Shoot()
    {
        lastShootTime = Time.fixedTime;
        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, head.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(head.transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bullet, bulletLifeTime);
    }

    void Move()
    {

    }
}
