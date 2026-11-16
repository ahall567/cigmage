using CigMage.Extensions;
using Godot;
using CigMage.Mobs;
using CigMage.PlayerCharacter;

namespace CigMage.Environment;

public partial class MobSpawnController : Node3D
{
	[Export] public Player PlayerReference;
	[Export] public PackedScene mobScene;
	[Export] public PackedScene consumableScene;

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
			mobController.MobDied += OnMobDied;
		}
		
		GetTree().CurrentScene.AddChild(mobNode);
	}

	// --- EVENT HANDLERS ---
	private void OnCooldownExpired()
	{
		SpawnMob(GlobalTransform);
	}

	private void OnMobDied(MobController mobController)
	{
		Node consumableNode = consumableScene.Instantiate();

		if (consumableNode is Node3D node3D)
		{
			node3D.GlobalTransform = mobController.GlobalTransform;
		}
		
		GetTree().CurrentScene.AddChild(consumableNode);

		mobController.MobDied -= OnMobDied;
	}
}
