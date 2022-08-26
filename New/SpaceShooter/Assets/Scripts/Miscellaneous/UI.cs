using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] public double currentScore;
    [SerializeField] private TMP_Text Text_Score;
    [SerializeField] private TMP_Text Text_Rest;
    [SerializeField] public TMP_Text Text_Ugh_Diddums;
    [SerializeField] public Image Image_BossHealthBar;
    [SerializeField] private Image Image_PlayerHealthBar;
    [SerializeField] private AudioClip oneUpAudioClip;

    private double lastScore;
    private DestroyPlayer player;
    private AudioSource audioSource;
    private double tempScore;   // This variable stores temporary score. Once it reaches 1000, it will be reset to 0
    private Color playerHealthBarNormalColour;
    private Color playerHealthBarStunnedColour;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        lastScore = 0;
        tempScore = 0;

        Text_Score.text = Properties.UI_SCORE + 0.ToString();
        Text_Ugh_Diddums.enabled = false;
        PlayerPrefs.SetString(Properties.PLAYER_PREFS_SCORE_KEY, currentScore.ToString());

        player = GameObject.Find(Properties.PLAYER).GetComponent<DestroyPlayer>();

        audioSource = GetComponent<AudioSource>();

        playerHealthBarNormalColour = Image_PlayerHealthBar.color;
        playerHealthBarStunnedColour = new Color(0, 1, 1);
        playerHealthBarStunnedColour.a = playerHealthBarNormalColour.a;
        // Players start with 3 lives
        UpdateRest(3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lastScore != currentScore)
        {
            Text_Score.text = Properties.UI_SCORE + currentScore.ToString();
            PlayerPrefs.SetString(Properties.PLAYER_PREFS_SCORE_KEY, currentScore.ToString());
        }

        lastScore = currentScore;
    }

    public void UpdateCurrentScore(int score)
    {
        currentScore = currentScore + score;
        tempScore = tempScore + score;

        // If the player scores 10000 points, increment the remaining lives by 1
        // Maximum lives that a player may have is 9
        if (tempScore >= 10000 && player.rest < 9)
        {
            tempScore = 0;
            player.rest = Mathf.Min(player.rest + 1, 9);
            audioSource.PlayOneShot(oneUpAudioClip);
        }

        UpdateRest(player.rest);
    }

    public void UpdateRest(int newRest)
    {
        Text_Rest.text = Properties.UI_REST + newRest.ToString();
    }

    public void UpdateHealth(float currentHealth)
    {
        Image_PlayerHealthBar.fillAmount = currentHealth / Properties.PLAYER_MAX_HEALTH;
    }

    public void UpdateBossHealth(float currentHealth)
    {
        Image_BossHealthBar.fillAmount = currentHealth / Properties.BOSS_MAXIMUM_HEAlTH;
    }

    public void DisableBossHealthBar()
    {
        Image_BossHealthBar.transform.parent.gameObject.SetActive(false);
    }

    public void ChangePlayerHealthBarColour(bool isPlayerStunned)
    {
        if (isPlayerStunned)
            Image_PlayerHealthBar.color = playerHealthBarStunnedColour;

        else
            Image_PlayerHealthBar.color = playerHealthBarNormalColour;
    }
}
