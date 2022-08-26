using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    private Enemy enemy;

    private void Start()
    {
        enemy = GameObject.Find(Properties.ENEMY).GetComponent<Enemy>();
    }

    public void OnLevelOver()
    {
        transitionAnimator.SetTrigger(Properties.ANIMATOR_LEVEL_IS_LEVEL_OVER);
    }

    public void OnFadeOutComplete()
    {
        // If player defeated the boss, load level complete scene, otherwise load game over scene
        if (enemy.isBossDefeated)
            SceneManager.LoadScene(Properties.SCENE_LEVEL_COMPLETE);

        else
            SceneManager.LoadScene(Properties.SCENE_GAME_OVER);
    }
}
