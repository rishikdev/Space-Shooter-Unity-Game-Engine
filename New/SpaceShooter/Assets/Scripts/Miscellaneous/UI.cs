using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] public double currentScore;
    [SerializeField] private TMP_Text Text_Score;
    [SerializeField] private TMP_Text Text_Rest;
    [SerializeField] public TMP_Text Text_Ugh_Diddums;

    private double lastScore;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        lastScore = 0;

        Text_Ugh_Diddums.enabled = false;
        PlayerPrefs.SetString(Properties.PLAYER_PREFS_SCORE_KEY, currentScore.ToString());

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
    }

    public void UpdateRest(int newRest)
    {
        Text_Rest.text = Properties.UI_REST + newRest.ToString();
    }
}
