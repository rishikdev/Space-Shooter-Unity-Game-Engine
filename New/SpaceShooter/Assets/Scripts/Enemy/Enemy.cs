using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyRammerKilledCount = 0;
    [SerializeField] private int enemyWithBulletsKilledCount = 0;
    [SerializeField] private int enemyWithLasersKilledCount = 0;

    public bool isBossActive = false;
    public bool isBossDefeated = false;

    public void SetEnemyRammerKilledCount()
    {
        enemyRammerKilledCount = enemyRammerKilledCount + 1;
    }

    public void SetEnemyWithBulletsKilledCount()
    {
        enemyWithBulletsKilledCount = enemyWithBulletsKilledCount + 1;
    }

    public void SetEnemyWithLasersKilledCount()
    {
        enemyWithLasersKilledCount = enemyWithLasersKilledCount + 1;
    }

    public int GetEnemyRammerKilledCount()
    {
        return enemyRammerKilledCount;
    }

    public int GetEnemyWithBulletsKilledCount()
    {
        return enemyWithBulletsKilledCount;
    }

    public int GetEnemyWithLasersKilledCount()
    {
        return enemyWithLasersKilledCount;
    }

    public void ActivateBoss()
    {
        isBossActive = true;
        isBossDefeated = false;
    }

    public void DeactivateBoss()
    {
        isBossActive = false;
        isBossDefeated = true;
    }
}
