using System.Collections;
using System.Collections.Generic;
using Tankfender;
using Unity.VisualScripting;
using UnityEngine;

namespace Tankfender
{
    public class EnemyController : MonoBehaviour
    {

        [SerializeField] private int currentLives = 1;
        [SerializeField] private int maxLives = 1;
        [SerializeField] private AudioClip deathSound;

        private EnemyManager enemyManager;
        private SoundManager soundManager;
        private EnemyAttack enemyAttack;
        private EnemyMovement enemyMovement;
        // Start is called before the first frame update
        void Awake()
        {
            enemyManager = FindObjectOfType<EnemyManager>();
            soundManager = FindObjectOfType<SoundManager>();
            enemyAttack = GetComponent<EnemyAttack>();
            enemyMovement = GetComponent<EnemyMovement>();

            //currentLives = gameManager.GetCurrentLives();
            //maxLives = gameManager.GetMaxLives();
        }

        public void TakeDamage()
        {
            ReduceLifeByOne();
        }

        void ReduceLifeByOne()
        {
            currentLives -= 1;
            soundManager.PlaySFX(deathSound);
            //anim explode
            if (currentLives <= 0)
            {
                enemyAttack.enabled = false;
                enemyMovement.enabled = false;
                enemyManager.EnemyTakenDown();
                Destroy(this.gameObject, 0.5f);

            }
        }
    }

}
