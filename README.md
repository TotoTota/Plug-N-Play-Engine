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

To create an entity in the world, you need to create a class that inherits from Entity:
```csharp
public class MyEntity : Entity
{
    
}
```

The Entity class have 5 abtract classes: OnStart, OnUpdate, OnRender, OnKill, KeyDown, add this to your class:
```csharp
public class MyEntity : Entity
{
    public override void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        
    }

    public override void OnUpdate(double deltaTime)
    {
        
    }

    public override void OnRender(double deltaTime)
    {
        
    }

    public override void OnStart()
    {
        
    }

    public override void OnKill()
    {
        
    }
}
```

OnStart is called in the beginning, OnUpdate is called every frame like OnRender but OnRender focuses on Rendering, KeyDown is for KeyEvents and finally OnKill is called when the entity is killed.

To Renderer a square from the entity, you create a new SpriteRenderer and initialize it with the position, the size, the rotation-z in the OnStart function:
```csharp
public class MyEntity : Entity
{
    private SpriteRenderer _renderer;

    public override void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        
    }

    public override void OnUpdate(double deltaTime)
    {
        
    }

    public override void OnRender(double deltaTime)
    {
        
    }

    public override void OnStart()
    {
        _renderer = new Renderer(Position, Size, RotationZ);   
    }

    public override void OnKill()
    {
        
    }
}
```

For initializing position, I recommend you add the entity's Position, Size, RotationZ variable, then you can change the position throught code.

To Render your sprite renderer, call the function Render from the sprite renderer class:
```csharp
public class MyEntity : Entity
{
    private SpriteRenderer _renderer;

    public override void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        
    }

    public override void OnUpdate(double deltaTime)
    {
        
    }

    public override void OnRender(double deltaTime)
    {
        _renderer.Render();
    }

    public override void OnStart()
    {
        _renderer = new Renderer(Position, Size, RotationZ);   
    }

    public override void OnKill()
    {
        
    }
}
```

To be able to use key events on position, call OnUpdate from the sprite renderer class:
```csharp
public class MyEntity : Entity
{
    private SpriteRenderer _renderer;

    public override void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        
    }

    public override void OnUpdate(double deltaTime)
    {
        _renderer.Update();
    }

    public override void OnRender(double deltaTime)
    {
        _renderer.Render();
    }

    public override void OnStart()
    {
        _renderer = new Renderer(Position, Size, RotationZ);   
    }

    public override void OnKill()
    {
        
    }
}
```

**Now the entity is finally created, the two last step you need to do is:**
- spawn the entity
- change background color because it's white

To spawn the entity in the world, call the SpawnEntity function in the Environment class you already initialized
```csharp
var window = new Window("Game", 1280, 720, maximized: true);
var graphics = new Graphics();
var camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), Vector3.UnitY, -90.0f, 0.0f);
// Make sure for 2d that the yaw is -90.0f and the up vector is Vector3.UnitY
var environment = new Environment();
environment.SpawnEntity<MyEntity>();
new Game.Start(window, graphics, camera, environment);
```

Now you need to change the background color since by default the color is white, the default color of the sprite renderer is white also, you wont see anything.
To change it, call SetBackgroundColor in the Game class:
```csharp
var window = new Window("Game", 1280, 720, maximized: true);
var graphics = new Graphics();
var camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), Vector3.UnitY, -90.0f, 0.0f);
// Make sure for 2d that the yaw is -90.0f and the up vector is Vector3.UnitY
var environment = new Environment();
environment.SpawnEntity<MyEntity>();
Game.SetBackgroundColor(Color.CornFlowerBlue);
new Game.Start(window, graphics, camera, environment);
```

You did it, run it, and you will see a white square in the center.

## Dependencies

- System.Drawing
- Silk.Net
- StbImageSharp
