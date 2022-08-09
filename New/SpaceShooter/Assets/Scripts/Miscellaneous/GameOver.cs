using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text Text_Score;

    private void Awake()
    {
        Text_Score.text = Text_Score.text + " " + PlayerPrefs.GetString(Properties.PLAYER_PREFS_SCORE_KEY);
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.SetString(Properties.PLAYER_PREFS_SCORE_KEY, 0.ToString());
        SceneManager.LoadScene(0);
    }
}
