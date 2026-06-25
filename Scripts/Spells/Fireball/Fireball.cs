using Godot;
using FpsTest.Extensions;

namespace FpsTest.Spells;

public partial class Fireball : Path3D
{
	[Export] public float MovementSpeed = 100;
	[Export] private Label3D _explosionMarker;
	[Export] private PathFollow3D _pathFollow;

	public override void _Ready()
	{
		_pathFollow = this.GetChildByType<PathFollow3D>();
		_explosionMarker.Visible = false;
	}

	public override void _PhysicsProcess(double delta)
	{
		_pathFollow.Progress += (float)delta * MovementSpeed;

		if (_pathFollow.ProgressRatio >= 0.99f)
		{
			QueueFree();
		}
	}
}
