using CigMage.Combat;
using CigMage.Extensions;
using Godot;

namespace CigMage.Spells;

public partial class ProjectileEntity : RigidBody3D
{
	[Export] private float _forceAmount;

	private CombatHandler _combatHandler;

	// --- GODOT OVERRIDES ---
	public override void _Ready()
	{
		_combatHandler = this.GetChildByType<CombatHandler>();
		_combatHandler.SetAttackingStatus(false);
		Launch();
	}

	// --- INTERNAL LOGIC ---
	private void Launch()
	{
		ApplyCentralImpulse(GlobalTransform.Basis.Z.Normalized() * _forceAmount);
	}

	private void ActivateAttack()
	{
		_combatHandler.SetAttackingStatus(true);
	}

	// --- EVENT HANDLERS ---
	public void OnBodyEntered(Node body)
	{
		ActivateAttack();
		QueueFree();
	}
}
