using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Player.Bass.Bindings
{
    internal static partial class NativeMethods
    {
        [DllImport(BASS_DLL_NAME, CharSet = CharSet.Unicode)]
        internal static extern IntPtr BASS_StreamCreateFile(bool mem, string file, long offest, long length, BassFlags flags);
    }

    internal enum BassFlags
    {
        /// <summary>
        /// The sample floatUse 32-bit floating-point sample data
        /// </summary>
        SampleFloat,
        SampleMono,
        SampleSoftware,
        Sample3D,
        SampleLoop,
        SampleFx,
        StreamPrescan,
        StreamAutofree,
        StreamDecode,
        SpeakerFront,
        SpeakerRear,
        SpeakerCenLfe,
        SpeakerRear2,
        SpeakerFrontLeft,
        SpeakerFrontRight,
        SpeakerRearLeft,
        SpeakerRearRight,
        SpeakerCenter,
        SpeakerLfe,
        SpeakerRear2Left,
        SpeakerRear2Right,
        AsyncFile,
        Unicode,
    }
}
