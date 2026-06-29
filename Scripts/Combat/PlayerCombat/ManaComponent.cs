using Godot;
using System;

namespace CigMage.Combat.PlayerCombat;

[GlobalClass]
public partial class ManaComponent : Node
{
    [Export] private float _maximumValue;

    private float _currentValue;
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            float clampedValue = Mathf.Clamp(value, 0, _maximumValue);

            if (_currentValue != clampedValue)
            {
                _currentValue = clampedValue;
                
                ManaChanged?.Invoke(_currentValue, _maximumValue);
                GD.Print($"Mana: {_currentValue}");
            }
        }
    }
    
    // --- EVENTS ---
    public event Action<float, float> ManaChanged;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        CurrentValue = _maximumValue;
    }

    // --- PUBLIC API ---
    public void Decrease(float amount)
    {
        CurrentValue -= amount;
    }

    public void Increase(float amount)
    {
        CurrentValue += amount;
    }
}
