using Godot;

namespace F00F.JointTest
{
    [Tool]
    public partial class Root : Game3D
    {
        public Camera Camera => GetNode<Camera>("Camera");

        public override void _Ready()
        {
            Camera.Select += OnTargetSelected;

            void OnTargetSelected(CollisionObject3D target)
            {
                if (target is RigidBody3D)
                {
                    target = target.GetParentOrNull<RigidBody3D>() ?? target;
                    Camera.Target = target;
                    return;
                }

                Camera.Target = null;
            }
        }

        public override void _UnhandledKeyInput(InputEvent e)
        {
            if (this.Handle(Input.IsActionJustPressed(MyInput.Quit), Quit)) return;

            void Quit()
            {
                GetTree().Quit();
                GD.Print("BYE");
            }
        }
    }
}
