﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TEMEliminatesMonsters.Src.KeyEvents;
using TEMEliminatesMonsters.Src.Updateables;
using MonoGame.Extended.Entities;
using TEMEliminatesMonsters.Src.Entities.ResourceNodes.Spawners;
using TEMEliminatesMonsters.Src.Map;
using TEMEliminatesMonsters.Src.Map.Tiles;
using TEMEliminatesMonsters.Src.Controllers;
using TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Systems;
using System;
using TEMEliminatesMonsters.Src.Entities.Resource_Nodes.Spawners.Concrete.Husks;

namespace TEMEliminatesMonsters.Src;

public class TEM : Game
{
	private CameraController _cameraController;
	private Fullscreener _fullscreener;
	private World _world;
	private HuskSpawnerFactory _huskSpawnerFactory;
	private readonly GraphicsDeviceManager _graphics;
	private readonly int _TileMapSize = 256;

	public TileMap Map;
	public Texture2D _zombie; // TODO: this is a test texture, remove this and replace it 
	public OrthographicCamera Camera;
	public Dictionary<string, Texture2D> Tiles = new();
	public SpriteBatch SpriteBatch;

	public static Vector2 MousePosition { get => Mouse.GetState().Position.ToVector2(); }

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
		_fullscreener.ToggleFullscreen();
	}

	/// <summary>
	/// initializes non-core objects
	/// </summary>
	protected override void Initialize ()
	{
		base.Initialize();

		SpriteBatch = new SpriteBatch(GraphicsDevice);

		KeyEventChecker = new();
		_fullscreener = new(_graphics, Window);
		BoxingViewportAdapter viewportAdapter = new(Window, GraphicsDevice, 1920, 1080);

		Camera = new OrthographicCamera(viewportAdapter);
		_cameraController = new(Camera);

		InitializeKeyEvents();

		Map = new(Tiles[$"{TileTexture.Metal_MiddleMiddle}"], 2, _TileMapSize, _TileMapSize);

		_world = new WorldBuilder()
		.AddSystem(new WorldUpdateSystem())
		.AddSystem(new WorldRenderSystem())
		// .AddSystem(Isystem system) 
		.Build();

		_huskSpawnerFactory = new(_world, Tiles[$"{TileTexture.Metal_Blocked_MiddleMiddle}"]);
		{
			FastRandom fr = new(Math.Abs((int)(DateTime.UtcNow.Ticks + Environment.UserName.GetHashCode())));
			for (int i = 0; i < 10; i++)
			{
				_huskSpawnerFactory.Create(new(fr.Next(1920), fr.Next(1080)));
			}
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
		_zombie = Content.Load<Texture2D>("zombie");

		foreach (string file in Directory.GetFiles("Content\\Tiles\\").Select(Path.GetFileNameWithoutExtension))
		{
			Debug.WriteLine(file);
			string s = "Tiles\\" + file;
			Texture2D texture = Content.Load<Texture2D>(s);
			Tiles.Add(file, texture);
		}
	}

	/// <summary>
	/// Runs every frame, updates objects
	/// </summary>
	/// <param name="gameTime">Game uptime</param>
	protected override void Update (GameTime gameTime)
	{
		// Debug
		Debug.WriteLine(gameTime.TotalGameTime);

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

		//render the entities
		_world.Draw(gameTime);

		//render the items

		//finish drawing
		SpriteBatch.End();

		base.Draw(gameTime);
	}
}