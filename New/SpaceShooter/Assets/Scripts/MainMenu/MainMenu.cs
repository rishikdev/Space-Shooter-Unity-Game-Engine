using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator mainMenuAnimator;

    public void StartGame()
    {
        mainMenuAnimator.SetTrigger(Properties.ANIMATOR_MAIN_MENU_IS_START_PRESSED_TRIGGER);
    }

    public void ShowControls()
    {

    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
