using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace Player.Bass.Bindings
{
    internal sealed class SafePluginHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal SafePluginHandle() : base(true) { }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.BASS_PluginFree(handle);
        }
    }
}
