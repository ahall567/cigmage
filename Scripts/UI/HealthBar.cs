using Godot;

namespace CigMage.Ui;

public partial class HealthBar : ProgressBar
{
    private Tween _tween;
	
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        EventBus.Instance.PlayerHealthValueChanged += OnPlayerHealthValueChanged;

        Value = 100;
    }

    public override void _ExitTree()
    {
        EventBus.Instance.PlayerHealthValueChanged -= OnPlayerHealthValueChanged;
    }

    private void OnPlayerHealthValueChanged(float newValue, float maxValue)
    {
        float newPercentage = (newValue / maxValue) * 100;

        if (_tween != null)
        {
            _tween.Kill();
        }
        _tween = CreateTween();
        _tween.TweenProperty(this, "value", newPercentage, 0.25f);
    }
}