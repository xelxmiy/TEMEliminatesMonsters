using System;
using System.Diagnostics;
using System.Collections.Generic;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TEMEliminatesMonsters.Src.KeyEvents;
using TEMEliminatesMonsters.Src.Updateables;
using TEMEliminatesMonsters.Src.Map;
using TEMEliminatesMonsters.Src.Controllers;
using TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners.Concrete.Husks;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Systems.EnemySystems.Husk;
using TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners.Concrete;
using TEMEliminatesMonsters.Src.FileLoading;
using TEMEliminatesMonsters.Src.Entities;

namespace TEMEliminatesMonsters.Src;

public class TEM : Game
{
	// Graphics
	private readonly GraphicsDeviceManager _graphics;
	private ScreenController _screenController;
	private CameraController _cameraController;
	public OrthographicCamera Camera;
	public SpriteBatch SpriteBatch;
	public static int ScreenWidth { get => 1920; }
	public static int ScreenHeight { get => 1080; }



	// World
	public TileMap Map;
	private World _world;
	private HuskSpawnerFactory _huskSpawnerFactory;
	private readonly int _TileMapSize = 256;

	// Other
	public static FastRandom Random;
	public static Vector2 MousePosition
	{
		get
		{
			float width = Mouse.GetState().X;
			float height = Mouse.GetState().Y;

			//this is done because despite us registering the screen width as 1920x1080 the mouse doesn't recognize that, so it still thinks the screen is 800x480 
			// and this property outputs the wrong value otherwise
			if (!Instance._screenController.IsFullscreen)
			{
				width *= ScreenWidth / 800f;
				height *= ScreenHeight / 480f;
			}

			return new Vector2(width, height) + Instance.Camera.Position;
		}
	}
	public static KeyboardEventChecker KeyEventChecker { get; private set; }
	public static TEM Instance { get; private set; }

	/// <summary>
	/// Creates the game and initializes core objects
	/// </summary>
	public TEM ()
	{
		_graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		Window.AllowAltF4 = true;
		IsMouseVisible = true;
		Instance = this;
	}

	/// <summary>
	/// Makes the game cover the whole screen
	/// </summary>
	public void GoFullScreen ()
	{
		_screenController.ToggleFullscreen();
	}

	/// <summary>
	/// initializes non-core objects
	/// </summary>
	protected override void Initialize ()
	{
		base.Initialize();

		SpriteBatch = new SpriteBatch(GraphicsDevice);

		KeyEventChecker = new();
		_screenController = new(_graphics, Window);
		BoxingViewportAdapter viewportAdapter = new(Window, GraphicsDevice, ScreenWidth, ScreenHeight);

		Camera = new OrthographicCamera(viewportAdapter);
		_cameraController = new(Camera);

		InitializeKeyEvents();

		Map = new(FileManager.Tiles[$"Tiles\\Grass Flat"], 2, _TileMapSize, _TileMapSize);

		_world = new WorldBuilder()
		.AddSystem(new WorldUpdateSystem<HuskMovementSystem>())
		.AddSystem(new EnemyRenderSystem<HuskMovementSystem>())
		.AddSystem(new WorldUpdateSystem<HuskSpawner>())
		.AddSystem(new SpawnerRenderSystem<HuskSpawner>())
		// .AddSystem(Isystem system) 
		.Build();

		_huskSpawnerFactory = new(_world);

		Random = new(Math.Abs((int)(DateTime.UtcNow.Ticks + Environment.UserName.GetHashCode())));

		//spawns 10 random spawners, for demonstration
		for (int i = 0; i < 10; i++)
		{
			_huskSpawnerFactory.Create(new(Random.Next(ScreenWidth), Random.Next(ScreenHeight)));
		}

	}
	/// <summary>
	/// Adds methods to key press events 
	/// </summary>
	public void InitializeKeyEvents ()
	{
		KeyboardEventManager.GetEvent(Keys.F11) += GoFullScreen;
	}

	/// <summary>
	/// loads assets
	/// </summary>
	protected override void LoadContent ()
	{
		FileManager.LoadEvilsToDictionary();
		FileManager.LoadIconsToDictionary();
		FileManager.LoadItemsToDictionary();
		FileManager.LoadTilesToDictionary();
	}

	/// <summary>
	/// Runs every frame, updates objects
	/// </summary>
	/// <param name="gameTime">Game uptime</param>
	protected override void Update (GameTime gameTime)
	{
		// Game Updates
		_world.Update(gameTime);
		UpdateableManager.UpdateAll(gameTime);
		base.Update(gameTime);
	}

	/// <summary>
	/// draws/renders objects to the screen
	/// </summary>
	/// <param name="gameTime"></param>
	protected override void Draw (GameTime gameTime)
	{
		//clear previously drawn stuff, if you see bright magenta, you're out of bounds!
		GraphicsDevice.Clear(Color.Magenta);

		//begin drawing
		var transformMatrix = Camera.GetViewMatrix();
		SpriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

		// render the TileMap
		Map.Render(SpriteBatch);

		//render the particles
		// TODO

		//render the entities
		_world.Draw(gameTime);

		//render the items
		// TODO

		//finish drawing
		SpriteBatch.End();

		base.Draw(gameTime);
	}
}