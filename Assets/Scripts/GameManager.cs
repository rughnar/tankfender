using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Tankfender
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject pauseScreen;

        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip loseSound;

        [SerializeField] private int currentPlayerAmmo = 5;
        [SerializeField] private int maxPlayerAmmo = 5;
        [SerializeField] private int currentPlayerLives = 3;
        [SerializeField] private int maxPlayerLives = 3;

        [SerializeField] Transform spawnTransform;
        public KeyCode resetKey = KeyCode.R;
        public KeyCode pauseKey = KeyCode.P;
        public KeyCode alternativeResumeKey = KeyCode.Escape;
        public int firstLevelBuildIndex = 4;
        private bool gameEnded = false;
        private bool gamePaused = false;
        private int sceneIndexToLoadIfReset;
        private SoundManager soundManager;

        private AmmoManager ammoManager;
        private HPManager hpManager;

        private EnemyManager enemyManager;



        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            sceneIndexToLoadIfReset = 1;
            if (SceneManager.GetActiveScene().buildIndex != firstLevelBuildIndex)
            {
                //         playerBehaviourController.SetBullets(PlayerPrefs.GetInt("PlayerBullets"));
                //         playerBehaviourController.SetHP(PlayerPrefs.GetFloat("PlayerHP"));
            }
        }

        void Awake()
        {
            //playerMovementController = FindObjectOfType<PlayerMovementController>();
            //playerBehaviourController = FindObjectOfType<PlayerBehaviourController>();
            soundManager = FindObjectOfType<SoundManager>();
            ammoManager = FindObjectOfType<AmmoManager>();
            ammoManager.SetParameters(currentPlayerAmmo, maxPlayerAmmo);
            hpManager = FindObjectOfType<HPManager>();
            hpManager.SetParameters(currentPlayerLives, maxPlayerLives);
            enemyManager = FindObjectOfType<EnemyManager>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(alternativeResumeKey)) if (gamePaused) Resume();

            if (Input.GetKeyDown(pauseKey))
            {
                if (!gamePaused) Pause();
                else Resume();
            }

            if (Input.GetKeyDown(resetKey))
            {
                if (gameEnded) SceneManager.LoadScene(sceneIndexToLoadIfReset, LoadSceneMode.Single);
            }

        }

        public void LoseGame()
        {
            Time.timeScale = 0;
            sceneIndexToLoadIfReset = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(ShowLoseScreen(1.5f));
            soundManager.PlaySFX(loseSound);

        }

        public IEnumerator ShowLoseScreen(float secondsToWait)
        {
            yield return new WaitForSecondsRealtime(secondsToWait);
            loseScreen.SetActive(true);
            gameEnded = true;
        }

        public void EndLevel()
        {
            if (SceneManager.sceneCountInBuildSettings - 1 != SceneManager.GetActiveScene().buildIndex)
            {
                //        PlayerPrefs.SetInt("PlayerBullets", playerBehaviourController.GetBullets());
                //        PlayerPrefs.SetFloat("PlayerHP", playerBehaviourController.GetHP());
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                WinGame();
            }
        }

        public void WinGame()
        {
            //   playerBehaviourController.Celebrate();

            winScreen.SetActive(true);
            gameEnded = true;
            sceneIndexToLoadIfReset = 0;
            Time.timeScale = 0;
            soundManager.PlaySFX(winSound);
        }

        public void Pause()
        {
            Time.timeScale = 0;
            //    playerMovementController.enabled = false;
            pauseScreen.SetActive(true);
            gamePaused = true;
        }

        public void Resume()
        {
            Time.timeScale = 1;
            //    playerMovementController.enabled = true;
            pauseScreen.SetActive(false);
            gamePaused = false;
        }

        public void BackToMainMenu() { SceneManager.LoadScene(0); }

        public void BulletShot() { ammoManager.ReduceAmmoBy1(); }

        public void Reload() { ammoManager.Reload(); }

        public void ReduceLivesByOne() { hpManager.ReduceHPBy1(); }

        public void RefillLives() { hpManager.Refill(); }

        public int GetCurrentLives() { return currentPlayerLives; }

        public int GetMaxLives() { return maxPlayerLives; }

        public Transform GetSpawnPoint() { return spawnTransform; }
    }

}
