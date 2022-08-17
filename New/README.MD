# [Space Shooter (New)](https://rishikdev.github.io/SpaceShooter/)
### About Space Shooter
Space Shooter is a 3D top-down ***shoot 'em up*** game inspired by the classic arcade games of the 1970s and 1980s such as [Galaga](https://en.wikipedia.org/wiki/Galaga),
and [Asteroids](https://en.wikipedia.org/wiki/Asteroids_(video_game)), as well as some shareware games from the late 1990s such as [DemonStar](https://en.wikipedia.org/wiki/DemonStar).

The game is playable [here](https://rishikdev.github.io/SpaceShooter/).

### Controls
Players can use either a gamepad (recommended), or a keyboard and a mouse to play the game.

| Controller | Move | Rotate | Shoot |
| :--------: | :--: | :----: | :---: |
| **Gamepad** | Left Stick | Right Stick | Right Trigger (single shot) <br> Right Shoulder Button (auto-fire) |
| **Keyboard** | WASD <br> Arrow Keys | <, > | Mouse Left Click (single shot) <br> Spacebar (auto-fire) | 

### Features
In Space Shooter, the players begin with 3 lives (in the game, lives are referred to as Rests, just like the old games), and a simple gun that shoots a single
line of bullets. During the gameplay, random power ups will spawn at random locations and move across the screen which the players can get to modify their gun.
The game offers the following ***Power Ups***:
* **Single Bullets** - Same as the default gun which the player starts with. It appears as a glowing white/silver sphere with the letter **D** written on it.
* **Triple Bullets** - With this power up, the player's gun shoots three bullets instead of one. It appears as a glowing red sphere with the letter **T** written on it.
* **Spread** - With this power up, the player's gun shoots three sets of three bullets with some angle between each set. It appears as a glowing blue sphere with **S** written on it.
* **Laser** - With this power up, the player's gun shoots two rays of laser beams. It deals more damage to the enemy. It appears are a glowing yellow sphere with **L** written on it.
* **Shield** - This power up grants the player invincibility for 30 seconds. It forms a green shield around the player. However, if you touch the boss, you will still die. It appears are a glowing green sphere with **P** written on it.

When the player dies, the spaceship respawns after three seconds with invincibility which lasts for five seconds.

### Points
Players are awarded points for destroying enemies and getting power ups. The following table shows the amount of points for each kill and power up.

| Enemy/Power Up | Points |
| :-: | :---: |
| **Ramming ship** | 100 |
| **Drones shooting bullets** | 100 |
| **Drones shooting lasers** | 200 |
| **Boss** | 5,000 |
| **Power Up** | 50 |

Players are also awarded an extra life for each 10,000 points.

### Enemies
Currently, this game has only one level with the following enemies:
* **Ramming ship** - This spaceship does not shoot any projectile. It just rams itself into the player and explodes.
* **Drones shooting bullets** - These drones appear and start circling the player while shooting bullets at the same time.
* **Drones shooting lasers** - These drones appear and start circling the player while firing laser beams at the same time.

### Boss Fight
The level 1 boss appears once 15 of the two drones each have been killed by the player. This boss shoots a lightning bolt as soon as it appears which makes the player unable to move.
The player can still shoot and rotate. This lasts for 10 seconds, and during this time, the player's health bar changes colour from green to blue.

The boss also shoots bullets and lasers in sets of threes which the player must dodge or risk getting killed almost instantaneously.

As mentioned above, the player will die immediately upon touching the boss. The game has been programmed in such as way that as soon as the boss appears, the Shield power up spawns and moves towards the player.

### Game Engine
I used Unity Game Engine to develop this game. The game was built for the WebGL platform.

### Where to Play
You can play the game online [here](https://rishikdev.github.io/SpaceShooter/). For some reason, Safari is not able to render this website. So please use any other web browser if you decide to play this game.

### Next Steps
With one level complete, the basic template of the game is ready. To design the next levels, not much work is needed as the enemy movement and shooting programs are already written.
