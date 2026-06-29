using Godot;

namespace CigMage.Spells;

public partial class Fireball : RigidBody3D
{
	[Export] public float ForceAmount = 30;

	// --- GODOT OVERRIDES ---
	public override void _Ready()
	{
		Launch();
	}

	// --- PUBLIC API ---
	public void Launch()
	{
		ApplyCentralImpulse(GlobalTransform.Basis.Z.Normalized() * ForceAmount);
	}

	// --- EVENT HANDLERS ---
	public void OnBodyEntered(Node body)
	{
		QueueFree();
		// TODO: Add explosion logic here
	}
}
