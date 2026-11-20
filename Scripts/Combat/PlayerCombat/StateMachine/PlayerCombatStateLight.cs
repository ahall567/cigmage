using Godot;
using System;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateLight : PlayerCombatStateBase
{
	public override PlayerCombatState StateKey => PlayerCombatState.Light;
	
	
}
