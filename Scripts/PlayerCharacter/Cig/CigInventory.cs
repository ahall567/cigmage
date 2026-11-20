using Godot;

namespace CigMage.PlayerCharacter.Cig;

/// <summary>
/// Tracks the amount of cigs the player has in inventory.
/// </summary>
public partial class CigInventory : Node
{
	[Export] private int _maxValue;
	
	private int _currentValue;
	public int CurrentValue
	{
		get => _currentValue;
		set
		{
			int clampedValue = Mathf.Clamp(value, 0, _maxValue);

			if (_currentValue != clampedValue)
			{
				_currentValue = clampedValue;
			}
		}
	}

	public bool IsEmpty => CurrentValue <= 0;
}
