#if TOOLS
namespace F00F.JointTest
{
    public partial class Root
    {
        public override void _Notification(int what)
        {
            if (Editor.OnPreSave(what))
            {
                Editor.DoPreSaveReset(Camera, Camera.PropertyName._input);
                Editor.DoPreSaveReset(Camera, Camera.PropertyName._config);
                return;
            }

            if (Editor.OnPostSave(what))
                Editor.DoPostSaveRestore();
        }
    }
}
#endif
