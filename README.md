# First-Person Shooter (FPS) Game
This project is a First-Person Shooter game created as part of a workshop tutorial and major assignment. It reflects weekly progress and includes core gameplay mechanics such as patrol-based enemies, interactive environments, a dual-weapon system, and a full HUD. The game combines both outdoor and indoor scenarios to deliver a complete FPS experience.

## GamePlay Video
https://drive.google.com/file/d/1SmSo8le3AZu697Bo0ESPukzxZhon9BKA/view?usp=sharing

## Features Implemented
### Hybrid Terrain: Outdoor to Indoor
- The game begins in a mountainous outdoor setting with urban elements, transitioning into a secure indoor facility for close-combat encounters.

### Two Weapon Types
- Players can switch between:
  A precise, slow-firing pistol
  A rapid-fire machine gun with longer range

### Health and Shield System
- Players have both a life bar(named lives in the HUD Display) and a shield bar(named health in the HUD Display).
  When taking damage, the shield absorbs most of it until it reaches zero. After that, the life bar takes full damage. 

### HUD Display
- A clean and responsive HUD constantly shows:
  Lives
  Shield bar(Health)
  Current weapon
  Keys Collected
  Coins Collected

### Locked Doors and Key Mechanics
- Certain doors are locked and require the player to find and collect specific keys to proceed. Player has to get the key for that particular door otherwise it won't open with any other key. In the start of the GamePlay video above, player had to get a key and open the door otherwise it didn't open automtically. 

### Enemy Patrol AI with Alert Logic
- Enemies patrol the environment. This game has flying monsters as enemies. If they see the player, they engage and shoot(shooting is visible as red spotlight). If they hear footsteps nearby, they perform a 360-degree scan before resuming patrol.

### Complete Sound Integration
- Sound effects are implemented for:
 Walking/running
 Shooting (both for enemies and player)
 Picking up lives
 Picking up coins and keys
 Dying
 Respawning

### Game Over & Restart Logic
- A game over screen appears upon player death. The player can restart the level from the beginning.

## Optional Bonus Features Implemented
### Pickups (Lives, Keys, Coins)
- Extra lives, coins are scattered around the map for the player to collect. 

### Checkpoint System
- If the player dies after reaching a checkpoint, they respawn at the latest checkpoint instead of starting over. Gameplay video above demonstrates when a player has life in the inventory and he looses his health then he respawns with a fading screen. 

### Dead Zones
Hazardous areas like cliff edges have been added to increase risk and encourage navigation skills.

