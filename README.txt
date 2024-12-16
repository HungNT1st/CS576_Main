// GAME
    - Ideas: 
        + New idea of an RPG game educating players about environmental problems such as forest fire, villains cutting down trees, and garbage littering,... 
        + Encourage players to protect the environment through actions like putting down forest fires, cleaning up trash, stopping people from damaging the forest, and preserving the habitat of wild animals. 

    - Key elements:
        + Main character: has walking, running, jumping, attacking and watering animations and can interact very well with surrounding elements like pets and pills.
        + Educational components: putting down forest fire, stopping villains from damaging the environment, collecting trash, adopt wild life animals.
        + Obstacles: Forest fire, red pills with potential to decrease health.
        + Enemies: Villains cutting down trees.
        + Items: Blue pills to increase speed, Yellow pills to decrease speed, Red pills to either increase or decrease speed with 50% chance each.

    - Mechanics:
        + Implement collision and interaction between different components well.
        + Shadow of game components like trees, pets and villains.

    - AI:
        + Pets like rabbit and monkey automatically follows main character.
        + Villains automatically attack trees and run to next trees to continue attacking after destroying their current corresponding tree.
        + Villains and forest fires deal more damage to the trees as the world health increases to significantly increase the difficulty of the game (much harder to win).
        + Automatically generate questions from AI models like ChatGPT when fighting villains to educate about environmental preservation.

    - Physics:
        + Implemented our own physics and used many different animations for main character, pets and villains.
        + Implemented visual and sound effects on interactions between different components.

    - User interface:
        + Implemented main menu with how to play tutorial, as well as clear in-game instructions for players.
        + Implemented win, lose, pause and retry menus.

    - Performance:
        + Our game runs very smoothly without lagging or framerate issues.

    - Aesthetics and Sound Effects:
        + Used online assets and art components in game to significantly increase the visual effects of our game.
        + Implemented sound effects of player actions, coin actions, planting trees, putting down fire, planting trees, spawning pets, taking down villains.

// TEAM
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
    - Wrote scripts to handle object random spawning on terrain around players in RandomTerrainSpawner.cs and implemented trash collecting system in Trash.cs.
    - Wrote script to handle HUD system with programmatic animations.
    - Wrote scripts to handle tree planting in TreePlantingHandler.cs.
    - Wrote scripts to handle in-game and menu audio management, including finding sfx, music for entities.
    - Implemented environment visual changes based on environment health in GameManager.cs.
    - Contributed to finding and importing assets such as player model, trash model, and particle systems.
    - Contributed to player animation state controller and animation.
    - Created text pop-up system to communicate with player.
    - Created Cinemachine camera work to follow player.
    - Responsible for creating and polishing menu and in-game UI and visual elements.
    - Set up terrain and camera work for main menu.
    - Contributed to debugging fire system, coin manager, scene loading.
    - Contributed to debugging referencing errors. 

3. Chien Nguyen
    - Wrote scripts to randomly spawn villains throughout the terrain to attack the trees in VillainSpawner.cs and VillainBehavior.cs with online assets from Unity Store for villain model and animations.
    - Wrote script to handle the villian's attack and running animation in VillainAnimationController.cs.
    - Wrote script to handle the villain's AI of attacking the trees and automatically running to the next tree to continue attacking in VillainBehavior.cs.
    - Wrote script to handle tree destruction by villains in TreeReference.cs.
    - Wrote script to handle villain's interaction with player (fighting with main player to die or deal damage to player) in VillainBehavior.cs.
    - Created Coin Manager system in CoinManager.cs to handle coin collection through actions that are beneficial to the environment like capturing villains, putting down fire, and collecting trash in VillainBehavior, FireManager.cs and Trash.cs.
    - Implemented using coins collected to plant trees and spawn pets in TreePlantingHandler.cs and PetsAddingHandler.cs.
    - Contributed to writing player instruction in main menu.
    - Contributed to debugging putting down fire and collecting trash.
    - Contributed to debugging player attack villian.
4. Tung Ngo
    - Created fire system: Implemented fire spawning mechanics and fire spread behavior across the terrain.
    - Developed fire extinguishing mechanics: Created water spray system and fire-water interaction logic.
    - Implemented health system: Created player health mechanics that responds to environmental hazards like fire.
    - Created pill/health pickup system: Designed and implemented collectible health items for player recovery.
    - Wrote FireManager.cs to handle fire spawning, spread, and extinction mechanics.
    - Wrote HealthSystem.cs to manage player health and damage interactions.
    - Assisted in debugging environment interactions and player mechanics.
    - Helped balance gameplay mechanics for fire spread and damage systems.
5. Tung Nguyen
    - Created and edited pills and items prefabs: Designed prefabs for pills and items, adding collider boxes for interaction and detection.
    - Edited fire prefabs: Edited fire prefabs' colliders for interaction and helped debug their spawn behavior and extinguishment mechanics.
    - Implemented water spray effect: Created the water spray effect using Unity's Particle System and scripted its interaction with fire prefabs for realistic gameplay.
    - Assisted debugging: Helped debug key features, including issues with spawning pills, fire objects, player health loss when in fire, and extinguishing logic.
    - Created initial terrain prototype: Designed an early terrain version for initial testing, which was later replaced with the final terrain.
