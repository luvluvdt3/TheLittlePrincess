# The Little Princess 
## 🎥 Demo
<video width="100%" height="auto" controls>
  <source src="./docs/Demo.mp4" type="video/mp4">
</video>

## Unity version
Unity version to run the project: ``2022.3.19f1``. At the beginning of the course we were told that the version of Unity does not matter as long as all our team members are using the same. Because one of our team members has been running Unity on Linux we were based on what version he has.

## 💻 Download link
[Download Link](https://unice-my.sharepoint.com/:f:/g/personal/thi-thanh-tu_duong_etu_unice_fr/EmiQF3KPhnxOhHb5YcVa6fMBjvNq4zBjLD0lzIThnMXazA?e=9WFeYT)

## 💡 How to use
- Go to *Build* folder
- Launch TheLittlePrincess.exe
- Enjoy

If you import the game using unity package and launch the game you won't be able to change scene because the index won't be declared in the parameter.
You'll be able to launch individual scene but every transition will lead to an error after the teleportation effect.

#### How to play in general
- Control character with WASD / ZQSD or arrow keys
- Rotate the planet on the third planet by holding the left mouse button and zooming with the mouse wheel.
- To skip dialogue, enter SPACE or wait for 15 seconds

## ⚙️What we did
<hr/>
<h3 style="text-align: center;">Menu</h3>

#### UI
We used `UI Samples` with some small modifications in content to create it.

#### Features
- *New Game*: Start the game from the beginning
- *How To Play*: The panel showing the manual of the game
- *Quit*: Exit the game
- The 3 planet symbols buttons:
  - Allow to start by choice from planet 1, 2 or 3

<hr/>
<h3 style="text-align: center;">Planet 1</h3>

```As the planet of forms and motions, it was created by an ancien wizard that admires and desires to create a museum-like planet to preserve the beauty of different beautiful creatures that he has found```

#### Features
- Import Blender files with animations
- Control animations/materials/sounds/... dynamically
- Usage of PostProcessing to enhance visual effect
- Player control with keyboard
- Dynamic Camera following different characters
- Control of particles system

#### Importing Blender files
- Tutorials:
  - [How to EXPORT MATERIALS from Blender to Unity 2023](https://www.youtube.com/watch?v=yloupOUjMOA)
  - [How to Bake Textures in Blender and Export to Unity](https://www.youtube.com/watch?v=x4mySebugl0)

<hr/>
<h3 style="text-align: center;">Planet 2</h3>

```The planet of velocity and purpose: the race planet. Modern and futuristic, it is a place where the most advanced technology is used to create the most thrilling experience.```

#### UI

The planet starts with a menu where the player can choose the number of laps and the number of opponents. The player can also pause the game and go back to the main menu.

#### Controls

Apart from the classic controls, the `v` key allows the player to change camera view. There are 3 camera views available:
- The main camera
- An interior camera (this one is not really useful because the car was not designed to be seen from the inside)
- A first person view from the front of the car

Also pressing `escape` will open a pause menu that freeze the game for the player to be able to take a break if needed.

#### Race mechanics

Checkpoints are put along the race. They are intentionally visible, so that the player can see where to go and not try to cut the circuit.

The player can do as many laps as they want, with as many bots as they want. However, the more the number of opponent the more chaotic the race will be, as the circuit is quite small.

Physics are enhanced compared to the TDs so the driving experience is more realistic.

#### Features implemented

Almost all features from the TDs are implemented in the game. The main difference is the camera animation at the beginning: it was removed because it didn't look good in the game.

The AI is similar but it handles better collisions with obstacles.

And just to make thing clear, yes the asset 'Racing starter kit' that we used contains similar script to the one used in our submission, it's because in order to solve some of our issues we took inspiration from it by reverse engeneering some features to see how to use wheel collider correctly for example. The main thing we took directly from this asset is the race circuit in itself.

<hr/>
<h3 style="text-align: center;">Transition 2-3</h3>

The main characters movement from the 1st planet have been used as well as the camera tracking. The local character chases the player moving his head in the user's direction (done by **[Animation Rigging](https://docs.unity3d.com/Packages/com.unity.animation.rigging@1.3/manual/index.html)**).

#### Developed/Modified elements
* Gravitation changes
* Camera does not goes through the colliders (In the worst cases it moves towards the user)
* Blinking light
* Colliding all the scene

<hr/>
<h3 style="text-align: center;">Planet 3 - Real-time 3D weather channel</h3>

```The lab of interaction and exploration, where the user can learn about the current weather in different countries and the climate change in the past.```

Arya, still breathing heavily with adrenaline in her blood after the Planet of the Present, moves forward towards the future. However, it turns out that it's not as bright and colorful as she thought. Here, she learns about the pressing problems facing humanity, particularly climate change, and how throughout their history, humanity has been destroying the environment. This makes her contemplate.

#### Features

We have implemented a real-time weather channel using the [OpenMeteo API](https://open-meteo.com/), which provides 10,000 calls per day for free from one IP address. After the limit is exceeded, the IP is blocked by the API. While 10,000 calls per day may seem like a lot, because we fetch coordinates for several countries simultaneously (in order to display the real-time weather of what the user sees), as well as a climate change graph for each selected country (which requires approximately 7 calls at once), it becomes extremely easy to reach the limit. Additionally, the API sometimes returns an internal error due to the number of simultaneous calls. This is why sometimes you need to click on a country twice to see the climate change graph.

In our planet, we use several complex mathematical calculations, including:
1.  Converting 3D coordinates into 2D coordinates to transform what the user actually sees at the exact moment on Earth and search for countries within the user's field of view.
2.  Transforming 2D coordinates (latitude and longitude) into 3D (X, Y, Z) coordinates for displaying weather labels on Earth and rotating these labels. While not ideal, it is at least readable and understandable.
An interesting point to mention is how we search for countries that the user sees. Although there are only 195 countries in the world, we have created our planet targeting a larger number of coordinates for future optimization. To optimize the search in the list of coordinates, we utilized a data structure called KD tree, commonly used in Olympiad programming contests.



#### Course topics

Planet 3 covers deeply several course topics, including:
1.  Patterns:
	1.1. Observer: Whenever one action finishes its execution, it notifies all dependent actions (e.g., LoadData - WeatherAPI - WeatherDisplay, etc.).
	1.2. Builder: Rest-API link constructor.

2.  Immersive maps: We are using an exocentric map representation for a planet 3. In spatial representation, an exocentric description (or map) is one that is viewed from an external viewpoint, as if you’re looking at a situation objectively. In our case, it's like viewing the Earth from space.
3. Architecture: MVC architecture pattern




#### Developed/Modified elements
All scripts, controllers, and UI elements, except for the models listed in the section below, have been implemented by our team. We did not have any specific examples or tutorials on how to implement this in Unity; we simply drew inspiration from TV weather forecasts.


## Used external ressources
### General Usage:
- [Sherbb's Particle Collection](https://assetstore.unity.com/packages/vfx/particles/sherbb-s-particle-collection-170798)
- [Dreamteck Splines](https://assetstore.unity.com/packages/tools/utilities/dreamteck-splines-61926)
- [Fantasy Skybox FREE](https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353)
- [Editor Auto Save](https://assetstore.unity.com/packages/tools/utilities/editor-auto-save-234445)
- [Stylized fire tornado](https://sketchfab.com/3d-models/stylized-fire-tornado-fe6bd0b87eca4c73bef78b0c6d874169)
- [Dialogue System for Unity](https://assetstore.unity.com/packages/tools/behavior-ai/dialogue-system-for-unity-11672)

### Menu:
  - [UI Samples](https://assetstore.unity.com/account/assets)
  - [Ambien Music : Forest Walk by Alexander Nakarada](https://www.chosic.com/download-audio/28063/)
  - [Celtic Knot](https://sketchfab.com/3d-models/celtic-knot-d63e93a560314b00aa9f90aea7e0d51a)
  - [Elemental Symbols](https://sketchfab.com/3d-models/elemental-symbols-wip-37e9306a76c543859d54475d8af1fe5c)

### Planet 1:
  - [Rocky Hills Environment](https://assetstore.unity.com/packages/3d/environments/landscapes/rocky-hills-environment-light-pack-89939)
  - [Hypersturm2.mp3](https://freesound.org/people/Kastenfrosch/sounds/162466/)
  - [message.mp3](https://freesound.org/people/Kastenfrosch/sounds/162464/)
  - [Ambient Music](https://pixabay.com/music/beautiful-plays-reflected-light-147979/)
  - [Infinian](https://sketchfab.com/3d-models/infinian-lineage-series-1279221a3196400d92a078319a5b3f40)
  - [Fairy](https://sketchfab.com/3d-models/fairy-the-legend-of-zelda-botw-ce8425ea1cfb40e492906bb62d970969)
  - [Drugdör The Golem](https://sketchfab.com/3d-models/drugdor-the-golem-animated-19c1855bdb2c4cdc89da2cfb64da48cf)
  - [Eudora Default](https://sketchfab.com/3d-models/eudora-default-8fba3d3ad7b443ff9607a502b0571797)
  - [Him/TheOne](https://sketchfab.com/3d-models/himtheone-day-and-night-272fcc8c0fb3492e9f7c6e83a817e1fe)
  - [Tarisland - Dragon](https://sketchfab.com/3d-models/tarisland-dragon-high-poly-ecf63885166c40e2bbbcdf11cd14e65f)
  - [Lily Paddler](https://sketchfab.com/3d-models/lily-paddler-209414e1d3f146a0a43e110605083972)
  - [Dilophosaurus](https://sketchfab.com/3d-models/dilophosaurus-cross-x-men-1b193012c22341e0b19143798b3c1013)
  - [Chocobo](https://sketchfab.com/3d-models/chocobo-ffx-blue-0d48eb5c1d8f48159c2e5a2b66eebbd7)
  - [TerrorBird](https://sketchfab.com/3d-models/chocobo-ffx-blue-0d48eb5c1d8f48159c2e5a2b66eebbd7)
  - [Stylised Glowing Rune Rock](https://sketchfab.com/3d-models/stylised-glowing-rune-rock-39c23be7af5848408ad74a744a127880)
  - [Stylized PBR Glowing Rocky Base](https://sketchfab.com/3d-models/stylized-pbr-glowing-rocky-base-1ece1bb1688a4b838a46bb2b25dfeeb9)
  - [Blue Mushroom Cave](https://sketchfab.com/3d-models/blue-mushroom-cave-bc4aef8fa00647bb953a94be827629b2)
  - [Night Mushrooms](https://sketchfab.com/3d-models/night-mushrooms-3d-december-day-1-ca5552e7055f4a3499261c2a3abfb4b1)
  - [Arctic Ray](https://sketchfab.com/3d-models/arctic-ray-939d84d73bb541a782fdedc01f2299b9)
  - [Icy Dragon](https://sketchfab.com/3d-models/icy-dragon-2db9268227b943e6a41e88390f2875a6)
  - [Snow Dragon](https://sketchfab.com/3d-models/snow-dragon-46771d960d91450cac0f2f0e746f2545)
  - [Stylized Flying Bee Bird Rigged](https://sketchfab.com/3d-models/stylized-flying-bee-bird-rigged-dc6e35992a79471d890fa9bf558e3b25)
  - [Hollow Knight #146 - Markoth](https://sketchfab.com/3d-models/hollow-knight-146-markoth-f8d63f8fe0394342b08de45a14194ad4)
  - [Alien Plant](https://sketchfab.com/3d-models/alien-plant-142addd0188c49e18e6d836fb9dfe813)

### Transition 1-2:
  - [SimplePoly City](https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899)
  - [BOX-02 Robot](https://sketchfab.com/3d-models/box-02-robot-70795967b57e416ea8ca184af6005e21)

### Planet 2:
Unity store (mainly asset, script from Racing starter kit only to debug our features) :
  - [Tiny low poly cars](https://assetstore.unity.com/packages/3d/vehicles/land/tiny-low-poly-cars-180034)
  - [Racing starter kit](https://assetstore.unity.com/packages/templates/systems/racing-starter-kit-169750)


### Transition 2-3
Unity store (only models):
  - [SimplePoly City](https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899)
-   [Animated Gas Planet](https://assetstore.unity.com/packages/2d/textures-materials/sky/animated-gas-planet-92133) (+animation)
-   [Street Sign Pack](https://assetstore.unity.com/packages/3d/environments/urban/australian-street-sign-pack-213047)
-   [Sci-Fi music](https://assetstore.unity.com/packages/audio/music/sc-fi-music-214312)
-   [SpaceSkies Free](https://assetstore.unity.com/packages/2d/textures-materials/sky/spaceskies-free-80503)
-   [Modular Sci-Fi Corridor](https://assetstore.unity.com/packages/3d/environments/sci-fi/modular-sci-fi-corridor-142811)
SketchFab (models):
- [Alarm light](https://sketchfab.com/3d-models/alarm-light-933ac2e9d1924e2a973f8692a07972af)
- [Holo Globe](https://sketchfab.com/3d-models/holo-globe-ready-for-unreal-engine-1df6efc708ec4d1198474e102316872b)
-  [Ice staircase](https://sketchfab.com/3d-models/ice-staircase-b56bdd4dbd5c46cfba128dadc866c445)
- [Sci-fi table](https://sketchfab.com/3d-models/sci-fi-table-939567d9d0284e09969d23b4fd2a76ee)
- [Sci-Fi High Tech Computer Lowpoly](https://sketchfab.com/3d-models/sci-fi-high-tech-computer-lowpoly-036a7fd5f6f84e72bf836ace9fc81c79)
- [Sci-Fi Side Table & Monitors](https://sketchfab.com/3d-models/sci-fi-side-table-monitors-b3044f18d8f142ae930069cd5bda134d)
- [Rick](https://sketchfab.com/3d-models/rick-sanchez-from-rick-and-morty-5962dac6cd0347c78a86bfe7d995ec8e)
- [BOX-02 Robot](https://sketchfab.com/3d-models/box-02-robot-70795967b57e416ea8ca184af6005e21)

### Planet 3
Unity Store (only models, no scripts):
-   [Earth model](https://assetstore.unity.com/packages/3d/environments/sci-fi/planet-earth-free-23399)
-   [Dialog system](https://assetstore.unity.com/packages/tools/behavior-ai/dialogue-system-for-unity-11672)
-   [Bubble Font (Free Version)](https://assetstore.unity.com/packages/2d/fonts/bubble-font-free-version-24987)
External libraries:
- [KdTree](https://github.com/codeandcats/KdTree)
- [NGeoNames](https://github.com/RobThree/NGeoNames)
- [CSV parser](https://github.com/TinyCsvParser/TinyCsvParser)
- [XCharts](https://github.com/XCharts-Team/XCharts)
