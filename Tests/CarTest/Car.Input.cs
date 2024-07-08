using Godot;

namespace F00F.JointTest
{
    public partial class Car
    {
        private class MyInput : F00F.MyInput
        {
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
            public static readonly StringName Forward;
            public static readonly StringName Reverse;
            public static readonly StringName SteerLeft;
            public static readonly StringName SteerRight;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
            internal static class Defaults
            {
                public static readonly Key[] Forward = [Key.W, Key.Up];
                public static readonly Key[] Reverse = [Key.S, Key.Down];
                public static readonly Key[] SteerLeft = [Key.A, Key.Left];
                public static readonly Key[] SteerRight = [Key.D, Key.Right];
            }

            static MyInput() => Init<MyInput>();
            private MyInput() { }
        }
    }
}
