using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        [SerializeField] private float timeBetweenShoots = 0.1f;
        private float timeSinceLastShot = 0f;

        private GameManager gameManager;
        private SoundManager soundManager;
        private bool reloading = false;


        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            soundManager = FindObjectOfType<SoundManager>();
            timeSinceLastShot = 0f;
        }

        void Update()
        {
            if (Input.GetKeyDown(shootKey)) Shoot();
        }

        void Shoot()
        {
            if (!reloading && Time.fixedTime - timeSinceLastShot >= timeBetweenShoots)
            {
                timeSinceLastShot = Time.fixedTime;
                currentAmmo -= 1;
                GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, head.transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(head.transform.up * bulletSpeed, ForceMode2D.Impulse);
                bullet.GetComponent<BulletBehaviour>().objectiveTag = "Enemy";
                gameManager.BulletShot();
                soundManager.PlaySFX(shoot);
                Destroy(bullet, bulletLifeTime);
                if (currentAmmo <= 0) { StartCoroutine(Reload()); }
            }

        }

        IEnumerator Reload()
        {
            reloading = true;
            yield return new WaitForSeconds(timeToReload);
            currentAmmo = maxAmmo;
            gameManager.Reload();
            reloading = false;
        }


        public void SetInitialParameters(int currentAmmo, int maxAmmo)
        {
            this.currentAmmo = currentAmmo;
            this.maxAmmo = maxAmmo;
        }
    }

}
