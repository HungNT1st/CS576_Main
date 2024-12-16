1. Hung Nguyen
    - Create the terrain using some online assets from Unity Assets store for trees, skybox, ...
    - Create NavMesh for the terrain. 
    - Create 3 NPCs(pets) that follow the player using NavMeshAgent. Write FollowPlayer.cs for this. 
    - Create script PetsAddingHandler.cs to spawn pets. 
    - Create First Person Controller. Write FightVillian.cs to make player able to "fight" villian.
    - Edit Villian spawn strategy and animation in VillainSpawner.cs and VillainAnimationController.cs
    - Create Question system as a way for player to "fight" villian. Write QuestionScript.cs, OpenAIChatGPT.cs for generate random question by GPT. Create UI for question.
    - Create Game Manager to manage the game state. Write GameManager.cs for this. 
    - Create instruction for the game. Create CanvasController.cs for this. 
    - Write HealthUIManager.cs to control Health UI (not in used anymore).
    - Create Pause function for game. Write ResumeGame.cs and edit HUD.cs for this. 
    - Overall, write ConversationStarter.cs, FightVillian.cs, FollowPlayer.cs, GameManager.cs, HealthUIManager.cs, MainMenu.cs, OpenAIChatGPT.cs, PetsAddingHandler.cs, QuestionScript.cs, StartTerrain.cs, ResumeGame.cs, CanvasController.cs 
    and edit HUD.cs, VillainSpawner.cs, VillainAnimationController.cs
2. Tung Le
3. Chien Nguyen
    - Wrote scripts to randomly spawn villains throughout the terrain to attack the trees in VillainSpawner.cs and VillainBehavior.cs with online assets from Unity Store for villain model and animations.
    - Wrote script to handle the villian's attack and running animation in VillainAnimationController.cs.
    - Wrote script to handle the villain's AI of attacking the trees and automatically running to the next tree to continue attacking in VillainBehavior.cs.
    - Wrote script to handle tree destruction by villains in TreeReference.cs.
    - Wrote script to handle villain's interaction with player (fighting with main player to die or deal damage to player) in VillainBehavior.cs.
    - Created Coin Manager system in CoinManager.cs to handle coin collection through actions that are beneficial to the environment like capturing villains, putting down fire, and collecting trash in VillainBehavior, FireManager.cs and Trash.cs.
    - Implemented using coins collected to plant trees and spawn pets in TreePlantingHandler.cs and PetsAddingHandler.cs.
    - Contributed to writing player instruction in main menu.
4. Tung Ngo
5. Tung Nguyen
    - Created and edited pills and items prefabs: Designed prefabs for pills and items, adding collider boxes for interaction and detection.
    - Edited fire prefabs: Edited fire prefabs' colliders for interaction and helped debug their spawn behavior and extinguishment mechanics.
    - Implemented water spray effect: Created the water spray effect using Unity's Particle System and scripted its interaction with fire prefabs for realistic gameplay.
    - Assisted debugging: Helped debug key features, including issues with spawning pills, fire objects, player health loss when in fire, and extinguishing logic.
    - Created initial terrain prototype: Designed an early terrain version for initial testing, which was later replaced with the final terrain.
