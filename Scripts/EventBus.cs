using Godot;
using System;
using System.Dynamic;

public partial class EventBus : Node
{
    public static EventBus Instance { get; private set; }
    
    #region Events
    // --- GAME STATE ---
    public event Action<bool> GamePaused;
    
    // --- PLAYER ---
    public event Action<float, float> ManaValueChanged;
    public event Action<float, float> PlayerHealthValueChanged;
    
    #endregion
    
    // --- PUBLIC API ---
    // Game State
    public void PublishGamePaused(bool gamePaused) => GamePaused?.Invoke(gamePaused);
    
    // Player
    public void PublishManaValueChanged(float newValue, float maxValue)
        => ManaValueChanged?.Invoke(newValue, maxValue);

    public void PublishPlayerHealthValueChanged(float newValue, float maxValue)
        => PlayerHealthValueChanged?.Invoke(newValue, maxValue);
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        Instance = this;
    }
}
