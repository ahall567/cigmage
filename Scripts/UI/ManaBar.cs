using Godot;
using System;

namespace CigMage.UI;

public partial class ManaBar : ProgressBar
{
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
		Value = newPercentage;
	}
}
