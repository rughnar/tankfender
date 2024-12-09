using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Tankfender
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private GameObject head;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletLifeTime = 4f;
        [SerializeField] private float timeBetweenShoots = 2f;
        [SerializeField] private Transform shootOrigin;


        private float lastShootTime = 0f;



        void Awake()
        {
            lastShootTime = 0f;
        }

        void FixedUpdate()
        {
            if (Time.fixedTime - lastShootTime > timeBetweenShoots) Shoot();
        }

        void Shoot()
        {
            lastShootTime = Time.fixedTime;
            GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, head.transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(head.transform.up * bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<BulletBehaviour>().objectiveTag = "Player";
            Destroy(bullet, bulletLifeTime);
        }

    }

}
