using Godot;
using System;
using CigMage.Extensions;
using CigMage.Combat.PlayerCombat;

namespace CigMage.PlayerCharacter.Cig;

public partial class CigBase : Node3D
{
    [Export] private PlayerCombatController _playerCombatController;
    [Export] private float _refillRate;
    [Export] private float _maxLifetime;

    public Label3D StateLabel;

    public event Action Emptied;

    private float _currentLifetime;
    public float CurrentLifetime
    {
        get => _currentLifetime;
        private set
        {
            float clampedValue = Mathf.Clamp(value, 0, _maxLifetime);
            if (Mathf.IsEqualApprox(_currentLifetime, clampedValue)) return;

            bool wasEmpty = IsEmpty;
            _currentLifetime = clampedValue;

            if (!wasEmpty && IsEmpty)
            {
                GD.Print("Cigarette is empty.");
                Emptied?.Invoke();
            }
        }
    }

    public bool IsEmpty => CurrentLifetime <= 0;
    
    // --- PUBLIC API ---
    public void RegenMana(double delta)
    {
        if (IsEmpty) return;

        float manaToAdd = Mathf.Min(_refillRate * (float)delta, _currentLifetime);
        _playerCombatController.ManaComponent.Increase(manaToAdd);
        CurrentLifetime -= manaToAdd;
        GD.Print($"Cig amount: {CurrentLifetime}");
    }

    public void Relight()
    {
        CurrentLifetime = _maxLifetime;
    }

    // --- GODOT OVERRIDES ---
    public override void _EnterTree()
    {
        StateLabel = this.GetChildByType<Label3D>();
    }

    public override void _Ready()
    {
        _currentLifetime = _maxLifetime;
    }
}
