using Godot;
using CigMage.Extensions;
using CigMage.Combat;

namespace CigMage.Mobs;

public partial class MobController : CharacterBody3D
{
    private CombatHandler _combatHandler;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        _combatHandler = this.GetChildByType<CombatHandler>();
        SubscribeToEvents();
    }

    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }

    // --- INTERNAL LOGIC ---
    private void SubscribeToEvents()
    {
        _combatHandler.HealthZero += OnHealthZero;
    }

    private void UnsubscribeFromEvents()
    {
        _combatHandler.HealthZero -= OnHealthZero;
    }
    
    // --- EVENT HANDLERS ---
    private void OnHealthZero()
    {
        QueueFree();
    }
}
