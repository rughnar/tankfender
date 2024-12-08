using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tankfender
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject head;
        [SerializeField] private KeyCode shootKey;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletLifeTime = 2f;
        [SerializeField] private float timeToReload = 2f;
        [SerializeField] private Transform shootOrigin;
        [SerializeField] private int currentAmmo = 5;
        [SerializeField] private int maxAmmo = 5;
        [SerializeField] private AudioClip shoot;

        private GameManager gameManager;
        private SoundManager soundManager;

        private float lastBulletTime = 0f;


        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            soundManager = FindObjectOfType<SoundManager>();
        }

        void Update()
        {
            if (Input.GetKeyDown(shootKey)) Shoot();
        }

        void Shoot()
        {
            if (currentAmmo > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, head.transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(head.transform.up * bulletSpeed, ForceMode2D.Impulse);
                bullet.GetComponent<BulletBehaviour>().objectiveTag = "Enemy";
                gameManager.BulletShot();
                soundManager.PlaySFX(shoot);
                Destroy(bullet, bulletLifeTime);
                currentAmmo -= 1;
            }
            else
            {
                Reload();
            }

        }

        void Reload()
        {
            if (Time.fixedTime - lastBulletTime > timeToReload)
            {
                currentAmmo = maxAmmo;
                gameManager.Reload();
            }
        }


        public void SetInitialParameters(int currentAmmo, int maxAmmo)
        {
            this.currentAmmo = currentAmmo;
            this.maxAmmo = maxAmmo;
        }
    }

}
