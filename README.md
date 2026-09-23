# Unity Game Development Summary

| Field | Detail |
|---|---|
| **Name** |Courier Chaos|
| **Student Name(s)** | Bryan H|
| **Class / Course** | Courier Chaos|
| **Repository** | https://github.com/TempeHS/2026CT_GameDesign_CourierChaos_Bryan.H|
| **Unity Version** |6000.0.58f1|
| **Document Version** | v0.1|
| **Date** | 27/08/26|

---

## Table of Contents
1. [Game Overview](#1-game-overview)
2. [Video Walkthrough](#2-video-walkthrough)
3. [Game Mechanics](#3-game-mechanics)
4. [Visual Features](#4-visual-features)
5. [Audio Design](#5-audio-design)
6. [User Interface & HUD](#6-user-interface--hud)
7. [Scene & Level Design](#7-scene--level-design)
8. [Scripts & Programming](#8-scripts--programming)
9. [Development Techniques & Tutorials Acknowledged](#9-development-techniques--tutorials-acknowledged)
10. [Third-Party Content Acknowledgements](#10-third-party-content-acknowledgements)
11. [Challenges & Solutions](#11-challenges--solutions)
12. [Branch Development Summary](#12-branch-development-summary)

---

## 1. Game Overview


### 1.1 Genre
2d Platformer

### 1.2 Target Audience
Ages 7-15

### 1.3 Game Summary
Courier Chaos is a 2D platform action game where the player takes on the role of a courier. The aim is to travel through the level, collect parcels, talk to NPCs, and make it to the end of the route. I wanted the movement to feel quick and satisfying, so the player can jump, air-jump, dash, slide, and use walls to move around the level.

### 1.4 Win / Loss Conditions
| Condition | Description |
|---|---|
| Win |Reaching the end of the route and completing the level. The exact finish trigger depends on the scene. |
| Loss |Falling into a death zone or leaving the playable area. If a checkpoint has been activated, the player returns to it. |

### 1.5 Platform & Build Settings
| Setting | Detail |
|---|---|
| Target Platform | PC |
| Resolution |Any 16:9 Aspect Ratio |
| Build Type |Windows PC (exact architecture should be confirmed in Unity Build Settings) |

---

## 2. Video Walkthrough

### 2.1 Full Gameplay Walkthrough

<!--
  Embed a YouTube/Vimeo video or link to a file in the repository.
  YouTube embed syntax:
  [![Video Title](https://img.youtube.com/vi/VIDEO_ID/0.jpg)](https://www.youtube.com/watch?v=VIDEO_ID)

  OR link to a local file:
  [Watch Walkthrough Video](./docs/video/walkthrough.mp4)
-->

| Field | Detail |
|---|---|
| **Video Title** | |
| **Link / Embed** | |
| **Duration** | |
| **Description** | |

### 2.2 Feature Highlight Clips

| Clip | Description | Link |
|---|---|---|
| | | |
| | | |
| | | |

---

## 3. Game Mechanics

### 3.1 Core Mechanics
| ID | Mechanic | Description | Implemented In (Script/Object) |
|---|---|---|---|
| M-1 | Movement and acceleration | The player accelerates up to a maximum speed. Ground and air movement use different acceleration values, with friction slowing the player down. | `PlayerMovement.cs` |
| M-2 | Jumping | The player can jump from the ground and perform up to three air jumps. Coyote time and jump buffering make the controls feel more forgiving. | `PlayerMovement.cs` |
| M-3 | Dash and slide | The dash gives the player a quick burst of movement, while the slide lets them keep moving quickly along the ground. | `PlayerMovement.cs` |
| M-4 | Wall movement | The player can slide down walls and jump away from them when wall movement is enabled. | `PlayerMovement.cs` |
| M-5 | Parcels and checkpoints | Parcels disappear when collected, and checkpoints save the player’s position for respawning. | `ParcelController.cs`, `Checkpoint.cs`, `DeathZone.cs` |
| M-6 | NPC interaction | An icon appears near an NPC, allowing the player to interact and read dialogue with names and portraits. | `InteractionDetector.cs`, `NPC.cs`, `NPC Dialouge.cs` |

### 3.2 Player Controls
| Action | Input (Keyboard / Controller) | Description |
|---|---|---|
| Jump | Space Bar / configured `Jump` input | Ground jump plus up to three air jumps. |
| Move left/right | A/D or arrow keys | Horizontal movement with acceleration and friction. |
| Dash | Configured `Fire3` input | A short directional burst; one air dash is available before landing. |
| Slide | Configured `Fire1` input or S | A short ground slide that preserves forward momentum. |
| Interact | Input System `Interact` action | Interacts with a nearby NPC or other `IInteractable` object. |
| Pause | Tab | Opens or closes the pause menu. |
| Tips | H | Opens or closes the tips panel. |

### 3.3 Physics & Collision
| Feature | Description |
|---|---|
| Rigidbody2D movement | The player uses Unity’s 2D physics system, with movement updated in `FixedUpdate`. |
| Ground and wall checks | OverlapCircle checks detect the ground; raycasts detect left and right walls. |
| Collision triggers | Trigger colliders are used for parcels, checkpoints, death zones, out-of-bounds areas, and NPC interaction ranges. |
| Respawning | `DeathZone` returns the player to the static checkpoint position when available. |

### 3.4 Game Loop
| Stage | Description |
|---|---|
| Start / Initialisation | The start menu loads the scene named `Main`. The player also creates the ground and wall checks automatically if they are missing. |
| Core Loop | The player moves through the level, collects parcels, activates checkpoints, and talks to NPCs while avoiding hazards. |
| Win / End State | The repository scripts do not contain a universal win-condition controller; the finish behaviour is configured by the scene. |
| Restart | Death zones respawn the player at the last checkpoint. A full scene restart can be configured through Unity scene/build settings. |

### 3.5 Scoring & Progression
| Element | Description |
|---|---|
| Scoring System | There is currently no score counter. Parcels are collected as part of the level objective. |
| Difficulty Progression | Difficulty is created through platforming hazards, gaps, walls, out-of-bounds areas, and the level layout. |
| Unlockables / Levels | No unlock system is implemented in the inspected scripts. The start menu loads the `Main` scene. |

---

## 4. Visual Features

**Parallax** – The background moves at a different speed from the player, which gives the level more depth.

**Repeating background** – The background can repeat as the player explores, so one extremely large image is not needed.

**Clear sprites** – Keeping the images high quality helps the sprites stay sharp when viewed up close.


### 4.1 Particle Effects

| Effect Name | Purpose | Screenshot |
|---|---|---|
| | | |
| | | |
| | | |
| | | |

> Add screenshot images using: `![Effect Name](./docs/screenshots/effect_name.png)`

---

### 4.2 Cut Scenes & Cinematics

| Cut Scene | Trigger | Description | Screenshot / Still |
|---|---|---|---|
| | | | |
| | | | |
| | | | |

> Add screenshot images using: `![Cut Scene Name](./docs/screenshots/cutscene_name.png)`

---

### 4.3 Animations

| Animation | Object / Character | Description | Screenshot |
|---|---|---|---|
| Walking | Player character | The walking animation uses the sprite animation in `Assets/Animations/walk.anim`. | |
| Idle Down | Player character | The idle animation is stored in `Assets/Animations/Idle Down.anim`. | |
| Jump / Dash / Slide | Player character | The movement script changes the Animator state when the player jumps, dashes, walks, or slides. | |

> Add screenshot images using: `![Animation Name](./docs/screenshots/animation_name.png)`

---

### 4.4 Lighting & Post-Processing

| Feature | Description | Screenshot |
|---|---|---|
| | | |
| | | |
| | | |

> Add screenshot images using: `![Feature Name](./docs/screenshots/lighting_name.png)`

---

### 4.5 Shaders & Materials

| Shader / Material | Applied To | Description | Screenshot |
|---|---|---|---|
| | | | |
| | | | |
| | | | |

> Add screenshot images using: `![Shader Name](./docs/screenshots/shader_name.png)`

---

### 4.6 Additional Visual Screenshots

<!--
  Add any other notable screenshots here.
  Syntax: ![Description](./docs/screenshots/filename.png)
-->

| Description | Screenshot |
|---|---|
| | |
| | |
| | |

---

## 5. Audio Design

### 5.1 Music
| Track | Scene / Trigger | Source / Composer |
|---|---|---|
| `Courier Chaos BGM.MP3` | Background music asset available in `Assets/Audio`. Exact scene assignment is configured in Unity. | Not recorded in repository |
| `0921.MP3` | Audio asset available in `Assets/Audio`; exact trigger is configured in Unity. | Not recorded in repository |
| `TheFatRat_-_Xenogenesis_(mp3.pm).mp3` | Audio asset available in `Assets/Audio`; exact scene assignment is configured in Unity. | The FatRat / source and licence must be confirmed by the student |
| `skyrim-npc-music-harvest-dawn.mp3` | Audio asset available in `Assets/Audio`; likely NPC/dialogue use, but exact assignment must be confirmed in Unity. | Source and licence must be confirmed by the student |

### 5.2 Sound Effects
| Sound Effect | Trigger | Source |
|---|---|---|
| Jump sound | Plays when the player jumps or wall-jumps. | `PlayerMovement.jumpSound` |
| Walking sound | Plays while the player is moving on the ground and stops when they become idle, airborne, dashing, or sliding. | `PlayerMovement.walkingSound` |
| Checkpoint sound | Plays the first time a checkpoint is activated. | `Checkpoint.checkpointSound` |
| Other imported effects | `action_jump.mp3` and `slap-soundmaster13-49669815_4L20wGP.mp3` are present in `Assets/Audio`; exact use and licence should be confirmed. | Source not recorded |

### 5.3 Audio Implementation
| Feature | Description |
|---|---|
| Audio Mixer / Groups | No custom mixer implementation is visible in the inspected scripts. Audio is assigned through Unity `AudioSource` components. |
| Spatial / 3D Audio | No custom spatial-audio logic is visible in the inspected scripts. |
| Dynamic Audio | Walking, jumping, and checkpoint audio are triggered by gameplay state/events. |

---

## 6. User Interface & HUD

### 6.1 HUD Elements
| Element | Purpose | Screenshot |
|---|---|---|
| Velocity display | Shows the player’s horizontal velocity, vertical velocity, and overall speed using TextMesh Pro. | |
| Interaction icon | Lets the player know when they are close enough to interact with an NPC or object. | |
| Dialogue panel | Shows the NPC’s name, portrait, and dialogue one character at a time. | |

> Add screenshot images using: `![HUD Element](./docs/screenshots/hud_name.png)`

### 6.2 Menus
| Menu | Purpose | Screenshot |
|---|---|---|
| Main Menu | Starts the `Main` scene and provides an exit button. | |
| Pause Menu | Toggled with Tab and controlled through `PauseController`. | |
| Tips Screen | Toggled with H through `TipsMenuController`. | |
| Dialogue Menu | Pauses gameplay while NPC dialogue is active. | |
| Game Over Screen | No dedicated game-over controller is present in the inspected scripts. | |

> Add screenshot images using: `![Menu Name](./docs/screenshots/menu_name.png)`

---

## 7. Scene & Level Design

### 7.1 Scene List
| Scene Name | Purpose | Description |
|---|---|---|
| Main | Gameplay scene loaded by the start menu. | The scene name is referenced by `StartMenuController`; its scene file is not committed in the inspected repository files. |
| Start menu scene | Main-menu entry point. | The exact scene name is configured through Unity scene/build settings and is not referenced by script. |

### 7.2 Level / Environment Screenshots
| Level / Area | Description | Screenshot |
|---|---|---|
| | | |
| | | |
| | | |

> Add screenshot images using: `![Level Name](./docs/screenshots/level_name.png)`

### 7.3 Scene Management
| Feature | Description |
|---|---|
| Scene Loading Method | `SceneManager.LoadScene("Main")` is used by `StartMenuController`. |
| Persistent Data Between Scenes | `Checkpoint.savedPosition` is static and can persist while the application remains running. |
| Scene Transition Effects | `ScreenFader` provides coroutine-based fade-to-black and fade-from-black transitions for out-of-bounds teleporting. |

---

## 8. Scripts & Programming

### 8.1 Script Summary
| Script Name | Attached To | Responsibility |
|---|---|---|
| `PlayerMovement.cs` | Player | Rigidbody2D movement, jumping, air jumps, dash, slide, wall slide/jump, animation, and movement audio. |
| `ParcelController.cs` | Parcel | Destroys a parcel when it enters a trigger with the Player tag. |
| `Checkpoint.cs` / `DeathZone.cs` | Checkpoint / hazard | Saves a checkpoint position and respawns the player after death. |
| `Parallax.cs` | Background sprite | Moves and wraps background sprites relative to the camera. |
| `NPC.cs`, `NPC Dialouge.cs`, `InteractionDetector.cs`, `IInteractable.cs` | NPC / player interaction | Provides interactable objects, NPC dialogue data, dialogue UI, and interaction detection. |
| `DIALOGUE.cs` | Dialogue UI | Types dialogue lines and temporarily freezes player movement. |
| `MenuController.cs`, `PauseController.cs`, `StartMenuController.cs`, `TipsPopup.cs`, `ExitButton.cs` | UI/menu objects | Controls pause, start, tips, scene loading, and quitting. |
| `FadeBehaviour.cs`, `OutOfBounds.cs` | Screen/UI and hazard | Fades the screen and teleports the player when leaving the playable area. |
| `VelocityDisplay.cs` | HUD | Displays the player Rigidbody2D velocity and speed. |

### 8.2 Key Algorithms / Logic
| Feature | Script | Description |
|---|---|---|
| Buffered/coyote-time jumping | `PlayerMovement.cs` | A short jump buffer accepts early input and coyote time allows a jump shortly after leaving a platform. |
| Accelerated platform movement | `PlayerMovement.cs` | Ground and air acceleration, friction, speed caps, and directional facing create responsive movement. |
| Respawn checkpoint system | `Checkpoint.cs`, `DeathZone.cs` | A static position is saved on checkpoint activation and reused after entering a death zone. |
| Infinite parallax background | `Parallax.cs` | Background position is offset by camera movement and wrapped when it reaches its sprite length. |
| Typewriter dialogue | `NPC.cs`, `DIALOGUE.cs` | Dialogue is revealed one character at a time and can be advanced or completed by the player. |

### 8.3 Design Patterns Used
| Pattern | Where Applied | Justification |
|---|---|---|
| Component-based design | Unity MonoBehaviours | I separated different gameplay responsibilities into components that can be attached to Unity objects. |
| Interface-based interaction | `IInteractable` | This allows the player to interact with NPCs and other interactable objects using the same system. |
| Coroutine-based timed actions | `PlayerMovement`, `OutOfBounds`, `ScreenFader`, dialogue scripts | Coroutines are used for timed actions such as dashes, slides, screen fades, teleporting, and dialogue text. |

---

## 9. Development Techniques & Tutorials Acknowledged

> List every tutorial, course, video, or article that informed or guided your implementation. Include what you used it for and what you changed or adapted.

| # | Title | Author / Creator | URL / Source | What You Used It For | What You Changed / Adapted |
|---|---|---|---|---|---|
| 1 | | | | | |
| 2 | | | | | |
| 3 | | | | | |
| 4 | | | | | |
| 5 | | | | | |
| 6 | | | | | |
| 7 | | | | | |
| 8 | | | | | |

---

## 10. Third-Party Content Acknowledgements

> All third-party assets (art, audio, fonts, scripts, packages) must be listed here with their licence. Using an asset without acknowledgement may constitute academic misconduct.

### 10.1 Visual Assets
| Asset Name | Type | Creator / Source | Licence | URL | Used For |
|---|---|---|---|---|---|
| Player/background sprites | 2D sprites | Source not recorded in repository | Must be confirmed by the student | | Player and environment visuals |
| TextMesh Pro resources | UI resources | Unity Technologies / TextMesh Pro package | Unity package licence | https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest/ | Text and UI support |

### 10.2 Audio Assets
| Asset Name | Type | Creator / Source | Licence | URL | Used For |
|---|---|---|---|---|---|
| `Courier Chaos BGM.MP3` | Music | Source not recorded | Must be confirmed by the student | | Background music |
| `action_jump.mp3` | Sound effect | Source not recorded | Must be confirmed by the student | | Jump audio candidate |
| `TheFatRat_-_Xenogenesis_(mp3.pm).mp3` | Music | The FatRat / downloaded source name | Licence and permission must be confirmed | | Imported music asset |
| `skyrim-npc-music-harvest-dawn.mp3` | Music | Source not recorded | Licence and permission must be confirmed | | Imported music asset |

### 10.3 Scripts & Code Snippets
| Script / Snippet | Source | Licence | URL | Used For | Changes Made |
|---|---|---|---|---|---|
| Custom gameplay scripts | This repository | Student work | Repository project | Movement, interaction, UI, checkpoints, and hazards | Written for this project; no external snippet source recorded |

### 10.4 Unity Packages & Plugins
| Package Name | Version | Source | Licence | URL | Purpose |
|---|---|---|---|---|---|
| 2D Sprite | 1.0.0 | Unity Package Manager | Unity licence | https://docs.unity3d.com/Packages/com.unity.2d.sprite@1.0/manual/index.html | 2D sprite workflow |
| 2D Tilemap | 1.0.0 | Unity Package Manager | Unity licence | https://docs.unity3d.com/Packages/com.unity.2d.tilemap@latest/ | Tilemap level construction |
| Cinemachine | 3.1.7 | Unity Package Manager | Unity licence | https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/index.html | Camera tools |
| Input System | 1.14.2 | Unity Package Manager | Unity licence | https://docs.unity3d.com/Packages/com.unity.inputsystem@1.14/manual/index.html | Input actions and interaction |
| Universal Render Pipeline | 17.0.4 | Unity Package Manager | Unity licence | https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/manual/index.html | Rendering pipeline |

### 10.5 Fonts
| Font Name | Creator / Source | Licence | URL |
|---|---|---|---|
| Liberation Sans | TextMesh Pro package | SIL Open Font License; licence file included in project | https://scripts.sil.org/OFL |
| Unity / Roboto / Oswald / Bangers / Anton | TextMesh Pro examples and extras | Licence files are included beside the fonts; verify which fonts are used | |

---

## 11. Challenges & Solutions

| # | Challenge Encountered | How It Was Solved |
|---|---|---|
| 1 | Making the movement feel responsive while still using Rigidbody2D physics | I used separate ground and air acceleration, friction, speed limits, jump buffering, and coyote time in `PlayerMovement`. |
| 2 | Adding several movement abilities without them conflicting | I added air-jump limits, dash limits, sliding, wall sliding, and wall jumping with checks for the player’s current state. |
| 3 | Preventing the player from losing too much progress after falling | Checkpoints save a position, and the death zone sends the player back to the most recent checkpoint. |
| 4 | Stopping the player from moving during dialogue | The dialogue systems freeze the player or pause the game until the conversation is finished. |
| 5 | Making the background work across a long level | `Parallax` moves the background with the camera and repeats it when it reaches the edge of the sprite. |

---

## 12. Branch Development Summary

> One section per feature branch. Add or remove sections to match your repository. Branches should be named for the feature they implement e.g. `feature/player-movement`. Link each branch name directly to the branch in your GitHub repository.

---

### Branch 1 — `main`

| Field | Detail |
|---|---|
| **Branch Name** | `main` |
| **Purpose** | Stable, releasable version of the game |
| **Merged From** | |
| **Final Commit** | |

---

### Branch 2 — `Camera`

| Field | Detail |
|---|---|
| **Branch Name** | |
| **Feature Developed** | |
| **Merged Into** | |
| **Date Started** | |
| **Date Merged** | |

#### What Was Built
<!-- Describe what this branch added or changed -->

#### Key Commits
| Commit Message | What Changed |
|---|---|
| | |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| | |
| | |

#### Screenshot / Evidence
<!-- Add a screenshot of the feature working -->
> `![Feature Name](./docs/screenshots/branch_feature_name.png)`

---

### Branch 3 — `Tileset`

| Field | Detail |
|---|---|
| **Branch Name** | |
| **Feature Developed** | |
| **Merged Into** | |
| **Date Started** | |
| **Date Merged** | |

#### What Was Built


#### Key Commits
| Commit Message | What Changed |
|---|---|
| | |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| | |
| | |

#### Screenshot / Evidence
> `![Feature Name](./docs/screenshots/branch_feature_name.png)`

---

### Branch 4 — `"-best save"`

| Field | Detail |
|---|---|
| **Branch Name** | |
| **Feature Developed** | |
| **Merged Into** | |
| **Date Started** | |
| **Date Merged** | |

#### What Was Built


#### Key Commits
| Commit Message | What Changed |
|---|---|
| WIP added movement asset + first gamescene| Added many .meta files, mainly on playerinput and character rednering |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| | |
| | |

#### Screenshot / Evidence
> `![Feature Name](./docs/screenshots/branch_feature_name.png)`

---

### Branch 5 — `feature/`

| Field | Detail |
|---|---|
| **Branch Name** | |
| **Feature Developed** | |
| **Merged Into** | |
| **Date Started** | |
| **Date Merged** | |

#### What Was Built


#### Key Commits
| Commit Message | What Changed |
|---|---|
| | |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| | |
| | |

#### Screenshot / Evidence
> `![Feature Name](./docs/screenshots/branch_feature_name.png)`

---

### Branch 6 — `feature/`

| Field | Detail |
|---|---|
| **Branch Name** | |
| **Feature Developed** | |
| **Merged Into** | |
| **Date Started** | |
| **Date Merged** | |

#### What Was Built


#### Key Commits
| Commit Message | What Changed |
|---|---|
| | |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| | |
| | |

#### Screenshot / Evidence
> `![Feature Name](./docs/screenshots/branch_feature_name.png)`

---

### Branch Development Overview

> Complete this summary table once all branches are finished.

| Branch Name | Feature | Date Started | Date Merged | Status |
|---|---|---|---|---|
| `main` | Stable release | | | |
| `feature/` | | | | |
| `feature/` | | | | |
| `feature/` | | | | |
| `feature/` | | | | |
| `feature/` | | | | |

---

> **Student Declaration:** All work submitted is my own except where explicitly acknowledged above.
