using Godot;
using System;
using System.Threading.Tasks;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateWindup : CigStateBase
{
    public override CigState StateKey => CigState.Windup;
    
    // --- EVENTS ---
    public event Action WindupCompleted;
    
    // --- STATE METHODS ---
    public override async void Enter()
    {
        base.Enter();

        await WaitTime();
        WindupCompleted?.Invoke();
    }
    
    // --- INTERNAL LOGIC ---
    // TODO: Remove coroutine once AnimationPlayer is implemented.
    private async Task WaitTime()
    {
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
    }
}
