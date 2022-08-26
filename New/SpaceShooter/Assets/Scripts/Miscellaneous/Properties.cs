using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public const string MAIN_CAMERA = "Main Camera";

    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_LEVEL_1 = "Level1";
    public const string SCENE_LEVEL_COMPLETE = "LevelComplete";
    public const string SCENE_GAME_OVER = "GameOver";

    public const string PLAYER_TAG = "Player";
    public const string PLAYER_BULLET_TAG = "PlayerBullet";
    public const string PLAYER_LASER_TAG = "PlayerLaser";
    public const string ENEMY_TAG = "Enemy";
    public const string ENEMY_BOSS_TAG = "EnemyBoss";
    public const string ENEMY_BULLET_TAG = "EnemyBullet";
    public const string ENEMY_STUN_BULLET_TAG = "EnemyStunBullet";
    public const string POWERUP_TAG = "PowerUp";

    public const string UI_CANVAS = "UI_Canvas";
    public const string TRANSITION = "Transition";
    public const string EXPLOSION_PARTICLE = "Explosion";
    public const string EXPLOSION_PARTICLE_BOSS_1 = "BigExplosion1";
    public const string EXPLOSION_PARTICLE_BOSS_2 = "BigExplosion2";
    public const string EXPLOSION_PARTICLE_BOSS_3 = "BigExplosion3";
    public const string EXPLOSION_PARTICLE_BOSS_4 = "BigExplosion4";
    public const string EXPLOSION_PARTICLE_BOSS_5 = "BigExplosion5";
    public const string EXPLOSION_PARTICLE_BOSS_6 = "BigExplosion6";
    public const string EXPLOSION_PARTICLE_BOSS_7 = "BigExplosion7";
    public const string EXPLOSION_PARTICLE_BOSS_8 = "BigExplosion8";
    public const string EXPLOSION_PARTICLE_BOSS_9 = "BigExplosion9";
    public const string EXPLOSION_PARTICLE_BOSS_10 = "BigExplosion10";

    public const string PLAYER = "Player";
    public const string SPACESHIP_NAME = "Spaceship";
    public const string PLAYER_SHIELD = "Shield";
    public const string PLAYER_BULLET = "PlayerBullet(Clone)";
    public const string PLAYER_LASER = "PlayerLaser(Clone)";
    public const float PLAYER_MAX_HEALTH = 100f;

    public const string POWERUP_SINGLEBULLET = "PowerUp_SingleBullet(Clone)";
    public const string POWERUP_TRIPLEBULLETS = "PowerUp_TripleBullets(Clone)";
    public const string POWERUP_SPREAD = "PowerUp_Spread(Clone)";
    public const string POWERUP_LASER = "PowerUp_Laser(Clone)";
    public const string POWERUP_SHIELD = "PowerUp_Shield(Clone)";

    public const float PLAYER_MOVE_THRESHOLD = 0.25f;
    public const float BACKGROUND_Z_POSITION = 3000;
    public const float PLAYER_Z_POSITION = 2000;

    public const string SCREEN_BOUNDARY_OUTER = "ScreenBoundaryOuter";
    public const float SCREEN_BOUNDARY_OFFSET_OUTER = 200;
    public const string SCREEN_BOUNDARY_BOTTOM_LEFT_OUTER = "BottomLeftOuter";
    public const string SCREEN_BOUNDARY_TOP_LEFT_OUTER = "TopLeftOuter";
    public const string SCREEN_BOUNDARY_TOP_RIGHT_OUTER = "TopRightOuter";
    public const string SCREEN_BOUNDARY_BOTTOM_RIGHT_OUTER = "BottomRightOuter";

    public const string SCREEN_BOUNDARY_INNER = "ScreenBoundaryInner";
    public const float SCREEN_BOUNDARY_OFFSET_INNER = -100;
    public const string SCREEN_BOUNDARY_BOTTOM_LEFT_INNER = "BottomLeftInner";
    public const string SCREEN_BOUNDARY_TOP_LEFT_INNER = "TopLeftInner";
    public const string SCREEN_BOUNDARY_TOP_RIGHT_INNER = "TopRightInner";
    public const string SCREEN_BOUNDARY_BOTTOM_RIGHT_INNER = "BottomRightInner";

    public const string ENEMY = "Enemy";    // This variable is used to store the kill count of each enemy
    public const string ENEMY_DRONE_WITH_BULLETS = "EnemyDroneWithBullets(Clone)";
    public const string ENEMY_DRONE_NAME = "XianSpaceship";
    public const string ENEMY_DESTRUCTION_AUDIO_SOURCE = "EnemyDestructionAudioSource";
    public const string ENEMY_DRONE_WITH_LASER = "EnemyDroneWithLasers(Clone)";
    public const string ENEMY_RAMMER = "EnemyRammer(Clone)";

    public const float BOSS_MAXIMUM_HEAlTH = 1000f;

    public const string UI_REST = "REST: ";
    public const string UI_SCORE = "SCORE: ";

    public const string PLAYER_PREFS_SCORE_KEY = "Score";
    public const string PLAYER_PREFS_LEVEL_KEY = "Level";
    public const int ENEMY_RAMMER_HIT_POINT = 100;
    public const int ENEMY_DRONE_WITH_BULLETS_HIT_POINT = 100;
    public const int ENEMY_DRONE_WITH_LASER_HIT_POINT = 200;
    public const int ENEMY_BOSS_HIT_POINT = 5000;
    public const int POWERUP_POINT = 50;

    public const string ANIMATOR_MAIN_MENU_IS_START_PRESSED_TRIGGER = "isStartPressed";
    public const string ANIMATOR_LEVEL_IS_LEVEL_OVER = "isLevelOver";
}
