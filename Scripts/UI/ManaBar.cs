using Godot;

namespace CigMage.UI;

public partial class ManaBar : ProgressBar
{
	private Tween _tween;
	
	// --- GODOT OVERRIDES ---
	public override void _Ready()
	{
		EventBus.Instance.ManaValueChanged += OnManaValueChanged;

		Value = 100;
	}

	public override void _ExitTree()
	{
		EventBus.Instance.ManaValueChanged -= OnManaValueChanged;
	}

	private void OnManaValueChanged(float newValue, float maxValue)
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
