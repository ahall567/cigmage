using Godot;
using CigMage.PlayerCharacter;

namespace CigMage.Consumables;

public partial class Consumable : RigidBody3D
{
	[Export] private Area3D _pickupRange;

	// --- GODOT OVERRIDES ---
	public override void _Ready()
	{
		_pickupRange.BodyEntered += OnPickupRangeBodyEntered;
	}

	public override void _ExitTree()
	{
		_pickupRange.BodyEntered -= OnPickupRangeBodyEntered;
	}

	// --- EVENT HANDLERS ---
	private void OnPickupRangeBodyEntered(Node3D body)
	{
		if (body is Player player)
		{
			QueueFree();
		}
	}
}
