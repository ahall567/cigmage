using Godot;
using CigMage.Extensions;

namespace CigMage.PlayerCharacter;

public partial class PlayerHead : Node3D
{
    public const float CamSensitivity = 0.006f;

    private Camera3D _cam;

    public override void _Ready()
    {
        _cam = this.GetChildByType<Camera3D>();
        _cam.Fov = 90;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion m) {
            RotateY(-m.Relative.X * CamSensitivity);
            _cam.RotateX(-m.Relative.Y * CamSensitivity);

            Vector3 camRot = _cam.Rotation;
            camRot.X = Mathf.Clamp(camRot.X,
                Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
            _cam.Rotation = camRot;
        }
    }
}
