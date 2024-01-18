using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TEMEliminatesMonsters.Src.Controllers;

/// <summary>
/// fullscreens the game, i totally stole all this and it doesn't even work right, sorry!
/// </summary>
public class ScreenController
{

	bool _isFullscreen = false;
	bool _isBorderless = false;
	int _width = 0;
	int _height = 0;
	private readonly GraphicsDeviceManager _graphics;
	private readonly GameWindow _window;

	/// <summary>
	/// initializes the ScreenController
	/// </summary>
	/// <param name="graphicsDeviceManager">this game's Graphics Device Manager</param>
	/// <param name="gameWindow">this game's Game Window</param>
	public ScreenController (GraphicsDeviceManager graphicsDeviceManager, GameWindow gameWindow)
	{
		_graphics = graphicsDeviceManager;
		_window = gameWindow;
	}

	/// <summary>
	/// toggles full screen
	/// </summary>
	public void ToggleFullscreen ()
	{
		bool oldIsFullscreen = _isFullscreen;

		if (_isBorderless)
		{
			_isBorderless = false;
		}
		else
		{	
			_isFullscreen = !_isFullscreen;
		}

		ApplyFullscreenChange(oldIsFullscreen);
	}

	/// <summary>
	/// toggles borderless fulscreen
	/// </summary>
	public void ToggleBorderless ()
	{
		bool oldIsFullscreen = _isFullscreen;

		_isBorderless = !_isBorderless;
		_isFullscreen = _isBorderless;

		ApplyFullscreenChange(oldIsFullscreen);
	}

	/// <summary>
	/// toggles fullscreen change
	/// </summary>
	/// <param name="oldIsFullscreen">if the current s_state is non-borderless fullscreen</param>
	private void ApplyFullscreenChange (bool oldIsFullscreen)
	{
		if (_isFullscreen)
		{
			if (oldIsFullscreen)
			{
				ApplyHardwareMode();
			}
			else
			{
				SetFullscreen();
			}
		}
		else
		{
			UnsetFullscreen();
		}
	}
	/// <summary>
	/// applies a hardware switch to the game's graphics
	/// </summary>
	private void ApplyHardwareMode ()
	{
		_graphics.HardwareModeSwitch = !_isBorderless;
		_graphics.ApplyChanges();
	}

	/// <summary>
	/// sets the game to fullscreen
	/// </summary>
	private void SetFullscreen ()
	{
		_width = _window.ClientBounds.Width;
		_height = _window.ClientBounds.Height;

		_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
		_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
		_graphics.HardwareModeSwitch = !_isBorderless;

		_graphics.IsFullScreen = true;
		_graphics.ApplyChanges();
	}

	/// <summary>
	/// removes fulscreen
	/// </summary>
	private void UnsetFullscreen ()
	{
		_graphics.PreferredBackBufferWidth = _width;
		_graphics.PreferredBackBufferHeight = _height;
		_graphics.IsFullScreen = false;
		_graphics.ApplyChanges();
	}
}
