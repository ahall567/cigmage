using Godot;
using System;
using System.Collections.Generic;
using CigMage.Extensions;

namespace CigMage.StateMachine;

/// <summary>
/// A generic, reusable state machine that manages a set of <see cref="State{T}"/> nodes
/// as children in the scene tree. The type parameter <typeparamref name="T"/> should be
/// an enum defining all possible states for a given entity (e.g. PlayerState, MobState).
/// </summary>
/// <remarks>
/// This class is not a GlobalClass and cannot be used directly in the editor.
/// Create a concrete subclass for each entity type (e.g. PlayerStateMachine : StateMachine&lt;PlayerState&gt;).
/// State nodes must be direct children of this node in the scene tree.
/// </remarks>
/// <typeparam name="T">An enum type whose values represent the available states.</typeparam>
public partial class StateMachine<T> : Node where T: Enum
{
    /// <summary>The parent node this state machine belongs to.</summary>
    [Export] public Node3D Parent;

    /// <summary>
    /// The state to enter when the scene is ready. Defaults to <c>default(T)</c>,
    /// which resolves to the first value defined in the enum.
    /// Override in a concrete subclass to set a different initial state via the inspector.
    /// </summary>
    protected T InitialState;

    /// <summary>Maps each enum value to its corresponding <see cref="State{T}"/> node.</summary>
    protected readonly Dictionary<T, State<T>> States = new();
    private State<T> _currentState;
    public State<T> CurrentState => _currentState;

    // --- GODOT OVERRIDES ---
    /// <summary>
    /// Discovers all child <see cref="State{T}"/> nodes, initializes them,
    /// registers them in the state dictionary, and enters the initial state.
    /// </summary>
    public override void _Ready()
    {
        foreach (State<T> state in this.GetChildrenByType<State<T>>())
        {
            state.StateMachine = this;
            state.Parent = Parent;
            state.Init();
            // state.Exit();
            States[state.StateKey] = state;
        }
        
        _currentState = States[InitialState];
        _currentState.Enter();
    }
    
    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState.HandleInput(@event);
    }

    public override void _Process(double delta)
    {
        _currentState.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState.PhysicsUpdate(delta);
    }

    // --- PUBLIC API ---
    
    /// <summary>
    /// Transitions to the state associated with the given <paramref name="stateKey"/>.
    /// Calls <see cref="State{T}.Exit"/> on the current state and <see cref="State{T}.Enter"/>
    /// on the new state. No-ops with an error log if the key is not registered.
    /// </summary>
    /// <param name="stateKey">The enum value identifying the target state.</param>
    public void ChangeState(T stateKey)
    {
        if (!States.TryGetValue(stateKey, out State<T> newState))
        {
            GD.PrintErr($"StateMachine.ChangeState: State {stateKey} not found!");
            return;
        }

        if (!_currentState.CanExit) return;
        _currentState.Exit();
        newState.Enter();
        _currentState = newState;
    }

    /// <summary>
    /// Gets a reference to the child state associated with the given <paramref name="stateKey"/>.
    /// No-ops with an error log if the key is not registered.
    /// </summary>
    /// <param name="stateKey">The enum value identifying the target state.</param>
    /// <returns>The target state.</returns>
    public State<T> GetState(T stateKey)
    {
        States.TryGetValue(stateKey, out State<T> resultState);
        if (resultState == null)
        {
            GD.PrintErr($"StateMachine.GetState: State {stateKey} not found!");
        }

        return resultState;
    }
}