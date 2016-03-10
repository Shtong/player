using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Player.Bass.Bindings
{
    internal partial class NativeMethods
    {
        [DllImport(BASS_DLL_NAME, CharSet = CharSet.Unicode)]
        internal static extern IntPtr BASS_PluginLoad(string file, BassFlags flags);

        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_PluginFree(IntPtr handle);
    }
}
