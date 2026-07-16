using Godot;

namespace CigMage.UI;

public partial class UIManager : Control
{
	private Control _pausePanel;
	private Control _playerUIPanel;
	
	
	// --- GODOT OVERRIDES ---
	public override void _Ready()
	{
		_pausePanel = GetNode<Control>("PausePanel");
		_playerUIPanel = GetNode<Control>("PlayerUIPanel");
		_pausePanel.Hide();
		_playerUIPanel.Show();
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("toggle_pause"))
		{
			switch (GetTree().Paused)
			{
				case true:
					Unpause();
					break;
				case false:
					Pause();
					break;
			}
		}
	}

	private void Pause()
	{
		_playerUIPanel.Hide();
		Input.MouseMode = Input.MouseModeEnum.Visible;
		_pausePanel.Show();
		GetTree().Paused = true;
	}

	private void Unpause()
	{
		_pausePanel.Hide();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		_playerUIPanel.Show();
		GetTree().Paused = false;
	}
}
