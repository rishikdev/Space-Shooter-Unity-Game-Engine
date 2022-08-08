using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public const string MAIN_CAMERA = "Main Camera";

    public const string PLAYER_TAG = "Player";
    public const string PLAYER_BULLET_TAG = "PlayerBullet";
    public const string ENEMY_TAG = "Enemy";
    public const string ENEMY_BULLET_TAG = "EnemyBullet";
    public const string POWERUP_TAG = "PowerUp";

    public const string PLAYER = "Player";
    public const string SPACESHIP_NAME = "Spaceship";
    public const string PLAYER_SHIELD = "Shield";
    public const string PLAYER_BULLET = "PlayerBullet(Clone)";
    public const string PLAYER_LASER = "PlayerLaser(Clone)";

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
    public const float SCREEN_BOUNDARY_OFFSET_INNER = -50;
    public const string SCREEN_BOUNDARY_BOTTOM_LEFT_INNER = "BottomLeftInner";
    public const string SCREEN_BOUNDARY_TOP_LEFT_INNER = "TopLeftInner";
    public const string SCREEN_BOUNDARY_TOP_RIGHT_INNER = "TopRightInner";
    public const string SCREEN_BOUNDARY_BOTTOM_RIGHT_INNER = "BottomRightInner";

    public const string ENEMY_DRONE = "EnemyDrone(Clone)";
    public const string ENEMY_DRONE_NAME = "XianSpaceship";
}
