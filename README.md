# Plug-N-Play-Engine
![PlugNPlayLogo](https://github.com/TotoTota/Plug-N-Play-Engine/assets/124183283/2dd97e56-a4f0-494b-9613-43ffe1516d74)

A small 2D Game engine writen in 24h! here is the link to the youtube video:


## How to install Plug-N Play Engine

1. Create a .Net 6 console application
2. Install the [Plug_N_Play.Engine](https://www.nuget.org/packages/Plug_N_Play.Engine) package
3. Have fun!

## Documentation

To Create A window you start by calling start from the **Game** class:
```csharp
new Game.Start();
```

The Start function requires a window. Create a window class and initialize it with a title, width, height:
```csharp
var window = new Window("Game", 1280, 720);
new Game.Start(window);
```

If you want to maximize it, add:
```csharp
var window = new Window("Game", 1280, 720, maximized: true);
new Game.Start(window);
```

Now, the window needs the graphics api, Create a **Graphics** class:
```csharp
var window = new Window("Game", 1280, 720, maximized: true);
var graphics = new Graphics();
new Game.Start(window, graphics);
```

A game always need a camera. Create a new Camera and initialize it with position, UpVector, yaw, pitch:
```csharp
var window = new Window("Game", 1280, 720, maximized: true);
var graphics = new Graphics();
var camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), Vector3.UnitY, -90.0f, 0.0f);
// Make sure for 2d that the yaw is -90.0f and the up vector is Vector3.UnitY
new Game.Start(window, graphics, camera);
```

Of course, every entity is stored in a world. Create an Environment class and pass it to the Game:
```csharp
var window = new Window("Game", 1280, 720, maximized: true);
var graphics = new Graphics();
var camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), Vector3.UnitY, -90.0f, 0.0f);
// Make sure for 2d that the yaw is -90.0f and the up vector is Vector3.UnitY
var environment = new Environment();
new Game.Start(window, graphics, camera, environment);
```

Finnished, If you see a white window, you did everything right, if not create an issue and tell us your problem or error you ecountered.

## Deeper Documentation
