using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tankfender
{
    public class BaseBehaviour : MonoBehaviour
    {
        [SerializeField] private int maxHP = 3;
        [SerializeField] private float currHP = 3;
        [SerializeField] private AudioClip destroyed;

        private GameManager gameManager;
        private SoundManager soundManager;

        void Awake()
        {
            currHP = maxHP;
            gameManager = FindObjectOfType<GameManager>();
            soundManager = FindObjectOfType<SoundManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                ReduceHP();
            }
        }


        void ReduceHP()
        {
            currHP -= currHP;

            if (currHP <= 0)
            {
                soundManager.PlaySFX(destroyed);
                gameManager.LoseGame();

            }

        }

    }

}
