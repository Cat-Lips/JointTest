using Godot;
using static Godot.Generic6DofJoint3D;

namespace F00F.JointTest
{
    [Tool]
    public partial class Shapes : RigidBody3D
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

        private const float MaxSteer = Const.Deg30;
        private const float SteerSpeed = 3;
        private float Drive;
        private float Steer;

        public override void _Ready()
        {
            InitSteering();

            void InitSteering()
            {
                foreach (var joint in FrontJoints)
                {
                    joint.SetFlagY(Flag.EnableMotor, true);
                    joint.SetFlagY(Flag.EnableAngularLimit, true);
                    joint.SetParamY(Param.AngularUpperLimit, MaxSteer);
                    joint.SetParamY(Param.AngularLowerLimit, -MaxSteer);
                }
            }
        }

        public override void _PhysicsProcess(double _delta)
        {
            var delta = (float)_delta;
            var velocity = LinearVelocity;
            var driveInput = Active ? DriveInput() : 0;
            var steerInput = Active ? SteerInput() : 0;

            SetSteer();
            var drive = GetDrive();

            float GetSteer()
                => Steer = Mathf.MoveToward(Steer, MaxSteer * steerInput, SteerSpeed * delta);

            float GetDrive()
            {
                if (driveInput is 0) ApplyDeceleration();
                else if (driveInput > 0) ApplyAcceleration();
                else if (this.IsMovingForward(velocity)) ApplyBrake();
                else ApplyReverse();

                void ApplyDeceleration()
                {
                    Brake = 0;
                    MaxDrive = 0;
                    Drive = Mathf.MoveToward(Drive, MaxDrive, Config.Deceleration * delta);
                }

                void ApplyAcceleration()
                {
                    Brake = 0;
                    var turbo = Input.Turbo();
                    MaxDrive = turbo ? Config.MaxTurbo : Config.MaxSpeed;
                    var acceleration = driveInput * (turbo ? Config.Turbo : Config.Acceleration);
                    Drive = Mathf.MoveToward(Drive, MaxDrive, acceleration * delta);
                }

                void ApplyBrake()
                {
                    Brake = driveInput * Config.Brake;
                    MaxDrive = 0;
                    Drive = 0;
                }

                void ApplyReverse()
                {
                    Brake = 0;
                    MaxDrive = -Config.MaxReverse;
                    var acceleration = driveInput * Config.Reverse;
                    Drive = Mathf.MoveToward(Drive, MaxDrive, acceleration * delta);
                }
            }

            float DriveInput() => Input.GetAxis(MyInput.Reverse, MyInput.Forward);
            float SteerInput() => Input.GetAxis(MyInput.SteerLeft, MyInput.SteerRight);
        }
    }
}
