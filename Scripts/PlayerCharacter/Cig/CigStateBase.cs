using CigMage.StateMachine;

namespace CigMage.PlayerCharacter.Cig;

public abstract partial class CigStateBase : State<CigState>
{
    protected CigBase CigBaseParent => Parent as CigBase;
    protected CigStateMachine CigStateMachine => StateMachine as CigStateMachine;
    
    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        CigBaseParent.StateLabel.Text = $"CigState: {StateKey.ToString()}";
    }
}
