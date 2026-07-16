using Godot;
using System;
using System.Threading.Tasks;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateExhale : CigStateBase
{
    public override CigState StateKey => CigState.Exhale;
    
    // --- EVENTS ---
    public event Action ExhaleCompleted;
    
    // --- STATE METHODS ---
    public override async void Enter()
    {
        base.Enter();

        await WaitTime();
        ExhaleCompleted?.Invoke();
    }
    
    // --- INTERNAL LOGIC ---
    // TODO: Remove coroutine once AnimationPlayer is implemented.
    private async Task WaitTime()
    {
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
    }
}
