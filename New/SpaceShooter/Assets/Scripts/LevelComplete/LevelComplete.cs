using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private TMP_Text Text_LevelComplete;
    [SerializeField] private TMP_Text Text_Score;

    private void Awake()
    {
        Text_LevelComplete.text = Text_LevelComplete.text + PlayerPrefs.GetString(Properties.PLAYER_PREFS_LEVEL_KEY);
        Text_Score.text = Properties.UI_SCORE + PlayerPrefs.GetString(Properties.PLAYER_PREFS_SCORE_KEY);
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.SetString(Properties.PLAYER_PREFS_SCORE_KEY, 0.ToString());
        SceneManager.LoadScene(Properties.SCENE_MAIN_MENU);
    }
}
