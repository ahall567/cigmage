using Godot;
using CigMage.Extensions;
using CigMage.Combat.PlayerCombat;

namespace CigMage.PlayerCharacter.Cig;

public partial class CigBase : Node3D
{
    [Export] private PlayerCombatController _playerCombatController;
    [Export] private float _refillRate;
    [Export] private float _totalLifetime;

    public Label3D StateLabel;

    private bool _isEmpty;
    public bool IsEmpty => _isEmpty;
    
    // --- PUBLIC API ---
    public void RegenMana(double delta)
    {
        if (_isEmpty) return;
        
        float manaToAdd = _refillRate * (float)delta;
        _playerCombatController.ManaComponent.Increase(manaToAdd);
        _totalLifetime -= manaToAdd;
        GD.Print($"Cig amount: {_totalLifetime}");
        if (_totalLifetime <= 0)
        {
            GD.Print("Cigarette is empty.");
            _isEmpty = true;
        }
    }

    // --- GODOT OVERRIDES ---
    public override void _EnterTree()
    {
        StateLabel = this.GetChildByType<Label3D>();
    }
    
}
