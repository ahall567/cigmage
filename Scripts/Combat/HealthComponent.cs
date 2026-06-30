using Godot;
using System;

namespace CigMage.Combat;

[GlobalClass]
public partial class HealthComponent : Node
{
    [Export] private float _maxHealth;

    private float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            float clampedValue = Mathf.Clamp(value, 0, _maxHealth);

            if (_currentHealth != clampedValue)
            {
                _currentHealth = clampedValue;
                
                HealthChanged?.Invoke(_currentHealth, _maxHealth);
                GD.Print($"Health: {_currentHealth}");

                if (_currentHealth <= 0) HealthZero?.Invoke();
            }
        }
    }
    
    // --- EVENTS ---
    public event Action<float, float> HealthChanged;
    public event Action HealthZero;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        CurrentHealth = _maxHealth;
    }
    
    // --- PUBLIC API ---
    public void Increase(float amount) { CurrentHealth += amount; }
    public void Decrease(float amount) { CurrentHealth -= amount; }

}
