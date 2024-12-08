using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private KeyCode shootKey;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime = 2f;
    [SerializeField] private Transform shootOrigin;

    void Update()
    {
        if (Input.GetKeyDown(shootKey)) Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, head.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(head.transform.up * bulletSpeed, ForceMode2D.Impulse);
        bullet.GetComponent<BulletBehaviour>().objectiveTag = "Enemy";
        Destroy(bullet, bulletLifeTime);
    }
}
