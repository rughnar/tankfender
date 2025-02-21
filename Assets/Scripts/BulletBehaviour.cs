using System;
using System.Collections;
using System.Collections.Generic;
using Tankfender;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Tilemaps;

namespace Tankfender
{
    public class BulletBehaviour : MonoBehaviour
    {
        public string objectiveTag = "Enemy";
        public AudioClip wallbreak;
        private SoundManager soundManager;
        private Rigidbody2D rb2d;

        private ObstacleTilemapController obstacleTilemapController;
        private Tilemap obstacleTilemap;

        void Awake()
        {
            soundManager = FindFirstObjectByType<SoundManager>();
            rb2d = GetComponent<Rigidbody2D>();
            obstacleTilemapController = FindFirstObjectByType<ObstacleTilemapController>();
            obstacleTilemap = obstacleTilemapController.obstacleTilemap;
        }


        void FixedUpdate()
        {
            CheckForTile();
        }

        private void CheckForTile()
        {
            // Encuentra el Tilemap en la escena
            Vector3Int tilePosition = obstacleTilemap.WorldToCell(transform.position);

            // Verifica si hay un tile en la posición
            if (obstacleTilemap.GetTile(tilePosition) != null)
            {
                // Destruye el tile
                obstacleTilemap.SetTile(tilePosition, null);
                soundManager.PlaySFXFluctuatingPitch(wallbreak);
                Destroy(gameObject); // Destruye la bala
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.CompareTag(objectiveTag))
            {
                if (other.gameObject.tag == "Enemy")
                {
                    other.gameObject.GetComponent<EnemyController>().TakeDamage();
                }
                else
                {
                    other.gameObject.GetComponent<PlayerController>().TakeDamage();
                }
                Destroy(this.gameObject);
            }
        }

    }

}
