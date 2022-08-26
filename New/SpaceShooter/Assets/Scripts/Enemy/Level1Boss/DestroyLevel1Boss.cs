using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevel1Boss : MonoBehaviour
{
    [SerializeField] private float bossHealth = 1000f;
    [SerializeField] private AudioClip bulletImpactAudioClip;
    [SerializeField] private GameObject[] explosionParticles;

    private float explosionInterval;
    private Enemy enemy;
    private UI ui;

    private PlayEnemyDestructionSound playEnemyDestructionSound;
    private AudioSource audioSource;

    public static bool isBossDead;
    private int explosionIndex;

    private Transition transition;
    private float transitionTime;

    private void Awake()
    {
        bossHealth = Properties.BOSS_MAXIMUM_HEAlTH;

        explosionInterval = 0.25f;

        audioSource = GetComponent<AudioSource>();
        ui = GameObject.Find(Properties.UI_CANVAS).GetComponent<UI>();
        playEnemyDestructionSound = GameObject.Find(Properties.ENEMY_DESTRUCTION_AUDIO_SOURCE).GetComponent<PlayEnemyDestructionSound>();

        enemy = GameObject.Find(Properties.ENEMY).GetComponent<Enemy>();
        isBossDead = false;
        explosionIndex = 0;

        transition = GameObject.Find(Properties.TRANSITION).GetComponent<Transition>();
        transitionTime = 2.5f;
    }

    private void Update()
    {
        if(isBossDead)
        {
            explosionInterval = explosionInterval - Time.deltaTime;

            if(explosionInterval <= 0)
            {
                explosionInterval = Random.Range(0.25f, 0.5f);

                explosionParticles[Mathf.Min(explosionIndex ++, explosionParticles.Length - 1)].SetActive(true);

                playEnemyDestructionSound.PlaySmallSpaceshipDestructionSound();
            }

            transitionTime = transitionTime - Time.deltaTime;

            if (transitionTime <= 0)
            {
                transition.OnLevelOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == Properties.PLAYER)
        {
            bossHealth = bossHealth - 5;

            if (bossHealth <= 0)
            {
                DestroyBoss();
            }

            else
            {
                audioSource.PlayOneShot(bulletImpactAudioClip);
                ui.UpdateBossHealth(bossHealth);
            }
        }

        else if(other.tag == Properties.PLAYER_BULLET_TAG)
        {
            Destroy(other.gameObject);

            bossHealth = bossHealth - 1;

            if (bossHealth <= 0)
            {
                DestroyBoss();
            }

            else
            {
                audioSource.PlayOneShot(bulletImpactAudioClip);
                ui.UpdateBossHealth(bossHealth);
            }
        }

        else if (other.tag == Properties.PLAYER_LASER_TAG)
        {
            Destroy(other.gameObject);

            bossHealth = bossHealth - 2;

            if (bossHealth <= 0)
            {
                DestroyBoss();
            }

            else
            {
                audioSource.PlayOneShot(bulletImpactAudioClip);
                ui.UpdateBossHealth(bossHealth);
            }
        }
    }

    private void DestroyBoss()
    {
        isBossDead = true;
        enemy.isBossDefeated = true;

        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        foreach (Transform child in transform)
            if (!child.name.Contains(Properties.EXPLOSION_PARTICLE))
                child.gameObject.SetActive(false);

        Destroy(gameObject, 2.5f);

        ui.UpdateCurrentScore(Properties.ENEMY_BOSS_HIT_POINT);
        ui.DisableBossHealthBar();

        PlayerPrefs.SetString(Properties.PLAYER_PREFS_LEVEL_KEY, 1.ToString());
    }
}
