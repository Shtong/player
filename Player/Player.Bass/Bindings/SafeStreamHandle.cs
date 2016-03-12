using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace Player.Bass.Bindings
{
    internal sealed class SafeStreamHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal SafeStreamHandle() : base(true) { }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.BASS_StreamFree(handle);
        }
    }
}
