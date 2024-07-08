using Godot;

namespace F00F.JointTest
{
    [Tool]
    public partial class Car : RigidBody3D
    {
        public bool Active { get; set; }

        private RigidBody3D WheelRL => GetNode<RigidBody3D>("WheelRL");
        private RigidBody3D WheelRR => GetNode<RigidBody3D>("WheelRR");
        private RigidBody3D WheelFL => GetNode<RigidBody3D>("WheelFL");
        private RigidBody3D WheelFR => GetNode<RigidBody3D>("WheelFR");

        private Generic6DofJoint3D JointRL => GetNode<Generic6DofJoint3D>("JointRL");
        private Generic6DofJoint3D JointRR => GetNode<Generic6DofJoint3D>("JointRR");
        private Generic6DofJoint3D JointFL => GetNode<Generic6DofJoint3D>("JointFL");
        private Generic6DofJoint3D JointFR => GetNode<Generic6DofJoint3D>("JointFR");

        private RigidBody3D[] DriveWheels => [WheelRL, WheelRR];
        private RigidBody3D[] FrontWheels => [WheelFL, WheelFR];
        private RigidBody3D[] Wheels => [WheelRL, WheelRR, WheelFL, WheelFR];

        private Generic6DofJoint3D[] DriveJoints => [JointRL, JointRR];
        private Generic6DofJoint3D[] FrontJoints => [JointFL, JointFR];
        private Generic6DofJoint3D[] Joints => [JointRL, JointRR, JointFL, JointFR];

        private float MaxSpeed = 1000;

        public override void _Ready()
        {
            //DriveJoints.ForEach(x => x.SetFlagX(Flag.EnableMotor, true));
            //DriveJoints.ForEach(x => x.SetParamX(Param.AngularMotorForceLimit, 0));
            //DriveJoints.ForEach(x => x.SetParamX(Param.AngularMotorTargetVelocity, MaxSpeed));
        }

        public override void _PhysicsProcess(double delta)
        {
            //var drive = Drive();
            //var steer = Steer();

            //GD.Print($"Drive: {drive}, Steer: {steer}");

            //DriveJoints.ForEach(ApplyDrive);
            //FrontJoints.ForEach(ApplySteer);

            //float Drive() => Input.GetAxis(MyInput.Reverse, MyInput.Forward);
            //float Steer() => Input.GetAxis(MyInput.SteerLeft, MyInput.SteerRight);

            //void ApplyDrive(Generic6DofJoint3D joint)
            //    => joint.SetParamX(Param.AngularMotorForceLimit, drive);

            //void ApplySteer(Generic6DofJoint3D joint)
            //{

            //}
        }
    }
}
