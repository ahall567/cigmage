using Godot;

namespace CigMage.Combat.PlayerCombat;

[GlobalClass]
public partial class ManaComponent : Node
{
    [Export] private float _maximumValue;

    private float _currentValue;
    public float CurrentValue
    {
        get => _currentValue;
        private set
        {
            float clampedValue = Mathf.Clamp(value, 0, _maximumValue);

            if (_currentValue == clampedValue) return;
            _currentValue = clampedValue;
            
            EventBus.Instance.PublishManaValueChanged(_currentValue, _maximumValue);
        }
    }
    
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
