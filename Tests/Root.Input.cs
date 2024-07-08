using Godot;

namespace F00F.JointTest
{
    public partial class Root
    {
        private class MyInput : F00F.MyInput
        {
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
            public static readonly StringName Quit;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
            internal static class Defaults
            {
                public static readonly Key Quit = Key.End;
            }

            static MyInput() => Init<MyInput>();
            private MyInput() { }
        }
    }
}
