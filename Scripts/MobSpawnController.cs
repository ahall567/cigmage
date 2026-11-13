using CigMage.Extensions;
using Godot;
using CigMage.Mobs;
using CigMage.PlayerCharacter;

namespace CigMage.Environment;

public partial class MobSpawnController : Node3D
{
	[Export] public Player PlayerReference;
	[Export] public PackedScene mobScene;

	private Timer _spawnCooldown;

	// --- GODOT OVERRIDES ---
	public override void _Ready()
	{
		_spawnCooldown = new Timer();
		_spawnCooldown.WaitTime = 2f;
		_spawnCooldown.Timeout += OnCooldownExpired;
		AddChild(_spawnCooldown);
		
		_spawnCooldown.Start();
	}

	public override void _ExitTree()
	{
		if (_spawnCooldown != null) _spawnCooldown.Timeout -= OnCooldownExpired;
	}

	// --- PUBLIC API ---
	public void SpawnMob(Transform3D targetTransform)
	{
		Node mobNode = mobScene.Instantiate();

		if (mobNode is Node3D node3D)
		{
			node3D.GlobalTransform = targetTransform;
		}

		if (mobNode is MobController mobController)
		{
			mobController.Init(PlayerReference);
		}
		
		GetTree().CurrentScene.AddChild(mobNode);
	}

	private void OnCooldownExpired()
	{
		SpawnMob(GlobalTransform);
	}
}
