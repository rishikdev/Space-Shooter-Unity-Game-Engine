using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text Text_Score;

    private void Awake()
    {
        Text_Score.text = Properties.UI_SCORE + PlayerPrefs.GetString(Properties.PLAYER_PREFS_SCORE_KEY);
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.SetString(Properties.PLAYER_PREFS_SCORE_KEY, 0.ToString());
        SceneManager.LoadScene(Properties.SCENE_MAIN_MENU);
    }
}
