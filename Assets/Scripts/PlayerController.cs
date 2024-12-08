using System.Collections;
using System.Collections.Generic;
using Tankfender;
using UnityEngine;

namespace Tankfender
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private int currentLives = 3;
        [SerializeField] private int maxLives = 3;
        [SerializeField] private AudioClip deathSound;

        private GameManager gameManager;
        private SoundManager soundManager;
        // Start is called before the first frame update
        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            soundManager = FindObjectOfType<SoundManager>();
            currentLives = gameManager.GetCurrentLives();
            maxLives = gameManager.GetMaxLives();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                TakeDamage();
            }
        }

        public void TakeDamage()
        {
            ReduceLifeByOne();
        }


        void ReduceLifeByOne()
        {
            currentLives -= 1;
            gameManager.ReduceLivesByOne();
            soundManager.PlaySFX(deathSound);
            //anim explode
            if (currentLives > 0)
            {
                Respawn();
            }
            else
            {
                gameManager.LoseGame();
            }
        }

        void Respawn()
        {
            //set base animation
            this.transform.position = gameManager.GetSpawnPoint().position;
            //invulnerable
        }
    }

}
