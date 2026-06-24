using Godot;
using System;

namespace FpsTest.StateMachine;

/// <summary>
/// Abstract base class for all states managed by a <see cref="StateMachine{T}"/>.
/// Each concrete state must declare a <see cref="StateKey"/> matching an enum value,
/// which is used by the state machine to look it up in its internal dictionary.
/// </summary>
/// <typeparam name="T">An enum type whose values represent the available states.</typeparam>
public abstract partial class State<T> : Node where T: Enum
{
    /// <summary>Reference to the owning state machine. Assigned during <see cref="StateMachine{T}._Ready"/>.</summary>
    public StateMachine<T> StateMachine;
    
    /// <summary>Reference to the parent entity. Assigned during <see cref="StateMachine{T}._Ready"/>.</summary>
    [Export] public Node3D Parent;
    
    /// <summary>
    /// The enum value that uniquely identifies this state within the state machine.
    /// Must be implemented by each concrete state class.
    /// </summary>
    public abstract T StateKey { get; }

    [Export] public bool CanExit = true;
    
    /// <summary>Called once during <see cref="StateMachine{T}._Ready"/> for one-time setup logic.</summary>
    public virtual void Init() {}

    /// <summary>Called when this state becomes active. Use for setup, animation triggers, etc.</summary>
    public virtual void Enter() {}
    
    /// <summary>Called when this state is deactivated. Use for cleanup logic.</summary>
    public virtual void Exit() {}
    
    /// <summary>Called every frame while this state is active. Equivalent to <c>_Process</c>.</summary>
    public virtual void Update(double delta) {}
    
    /// <summary>Called every physics frame while this state is active. Equivalent to <c>_PhysicsProcess</c>.</summary>
    public virtual void PhysicsUpdate(double delta) {}
    
    /// <summary>Called every frame to handle input while this state is active. Trigger state transitions here.</summary>
    public virtual void HandleInput(InputEvent @event) {}
}
