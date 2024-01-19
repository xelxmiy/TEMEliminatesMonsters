using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Diagnostics;
using TEMEliminatesMonsters.Src.KeyEvents;

namespace TEMEliminatesMonsters.Src.Controllers;

internal class CameraController : Updateables.IUpdateable
{
	private const float _baseMovementSpeed = 150f;

	private float _movementSpeed;

	private int _previousMouseX, _previousMouseY;

	private int _previousScrollValue;

	private readonly OrthographicCamera _camera;

	private static MouseState s_state;
	private static Vector2 MousePosition { get { return new Vector2(s_state.X, s_state.Y); } }

	/// <summary>
	/// Creates a new CameraController
	/// </summary>
	/// <param name="camera">_camera object</param>
	/// <param name="minZoom">Maximum screen zoom for this camera</param>
	/// <param name="maxZoom">Minimum screen zoom for this camera</param>
	public CameraController (OrthographicCamera camera, int minZoom = 1, int maxZoom = 5)
	{
		_movementSpeed = _baseMovementSpeed;
		_camera = camera;
		_camera.MaximumZoom = maxZoom;
		_camera.MinimumZoom = minZoom;
		_previousMouseX = Mouse.GetState().X;
		_previousMouseY = Mouse.GetState().Y;
		(this as Updateables.IUpdateable).AddSelfToUpdateables();
	}

	/// <summary>
	/// Updates the cameras position based on the mouse movement
	/// </summary>
	/// <param name="gameTime">The current gametime</param>
	public void Update (GameTime gameTime)
	{
		s_state = Mouse.GetState();

		if (s_state.RightButton == ButtonState.Pressed)
		{
			Vector2 movementVector = GetMovementDirection() * _movementSpeed * gameTime.GetElapsedSeconds();
			_camera.Move(movementVector);
			CheckBounds();
		}
		if (s_state.ScrollWheelValue != _previousScrollValue)
		{
			Zoom(s_state.ScrollWheelValue - _previousScrollValue);
		}
		_previousMouseX = (int)MousePosition.X;
		_previousMouseY = (int)MousePosition.Y;
		_previousScrollValue = s_state.ScrollWheelValue;
	}

	/// <summary>
	/// Keeps the camera in bounds of the Tilemap
	/// </summary>
	private void CheckBounds ()
	{
		if (_camera.Position.X < 0)
		{
			_camera.Position = new Vector2(0, _camera.Position.Y);
		}
		if (_camera.Position.Y < 0)
		{
			_camera.Position = new Vector2(_camera.Position.X, 0);
		}
		if (_camera.Position.X > TEM.Instance.Map.GridWidth)
		{
			_camera.Position = new Vector2(TEM.Instance.Map.GridWidth, _camera.Position.Y);
		}
		if (_camera.Position.Y > TEM.Instance.Map.GridLength)
		{
			_camera.Position = new Vector2(_camera.Position.X, TEM.Instance.Map.GridLength);
		}
	}

	/// <summary>
	/// Zooms in/out
	/// </summary>
	/// <param name="value">The Amount to zoom in</param>
	private void Zoom (float value)
	{
		value /= 960;
		if (_camera.Zoom + value <= _camera.MaximumZoom && _camera.Zoom + value >= _camera.MinimumZoom)
		{
			_camera.Zoom += value;
			_movementSpeed = _baseMovementSpeed / _camera.Zoom;
		}
	}

	/// <summary>
	/// Returns the movement direction for the camera if holding the mouse
	/// </summary>
	/// <returns></returns>
	private Vector2 GetMovementDirection ()
	{
		Vector2 difference = new Vector2(_previousMouseX, _previousMouseY) - MousePosition;
		float mouseSpeed = Vector2.Distance(new Vector2(_previousMouseX, _previousMouseY), MousePosition);

		if (difference == Vector2.Zero)
			return Vector2.Zero; //can't normalize the zero vector :(
		Vector2 movementDirection = Vector2.Normalize(difference);

		return movementDirection * mouseSpeed;
	}
}
