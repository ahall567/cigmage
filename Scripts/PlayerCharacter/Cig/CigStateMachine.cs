using Godot;
using System;
using CigMage.StateMachine;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateMachine : StateMachine<CigState>
{
    private CigState _initialState = CigState.Idle;

    public event Action WindupCompleted;
    public event Action ExhaleCompleted;

    public override void _Ready()
    {
        InitialState = _initialState;
        base._Ready();

        SubscribeToEvents();
    }

    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }
    
    // --- INTERNAL LOGIC ---
    private void SubscribeToEvents()
    {
        if (States[CigState.Windup] is CigStateWindup windupState) windupState.WindupCompleted += OnWindupCompleted;
        if (States[CigState.Exhale] is CigStateExhale exhaleState) exhaleState.ExhaleCompleted += OnExhaleCompleted;
    }

    private void UnsubscribeFromEvents()
    {
        if (States[CigState.Windup] is CigStateWindup windupState) windupState.WindupCompleted -= OnWindupCompleted;
        if (States[CigState.Exhale] is CigStateExhale exhaleState) exhaleState.ExhaleCompleted -= OnExhaleCompleted;
    }
    
    // --- EVENT HANDLERS ---
    private void OnWindupCompleted()
    {
        WindupCompleted?.Invoke();
    }

    private void OnExhaleCompleted()
    {
        ExhaleCompleted?.Invoke();
    }
}
