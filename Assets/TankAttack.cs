using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private KeyCode shootKey;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime = 2f;

    void Update()
    {
        if (Input.GetKeyDown(shootKey)) Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, head.transform.position, head.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(head.transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bullet, lifeTime);
    }
}
