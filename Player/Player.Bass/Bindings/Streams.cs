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

        /// <summary>
        /// Frees a sample stream's resources, including any sync/DSP/FX it has.
        /// </summary>
        /// <param name="handle">The stream handle.</param>
        /// <returns>If successful, <c>true</c> is returned, else <c>false</c> is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_StreamFree(IntPtr handle);
    }

    [Flags]
    internal enum BassFlags : uint
    {
        Default = 0,
        /// <summary>
        /// The sample floatUse 32-bit floating-point sample data
        /// </summary>
        SampleFloat         = 256,
        SampleMono          = 2,
        SampleSoftware      = 16,
        Sample3D            = 8,
        SampleLoop          = 4,
        SampleFx            = 128,
        StreamPrescan       = 0x20000,
        StreamAutofree      = 0x40000,
        StreamDecode        = 0x200000,
        SpeakerFront        = 0x1000000,
        SpeakerRear         = 0x2000000,
        SpeakerCenLfe       = 0x3000000,
        SpeakerRear2        = 0x4000000,
        SpeakerLeft         = 0x10000000,
        SpeakerRight        = 0x20000000,
        SpeakerFrontLeft    = SpeakerFront | SpeakerLeft,
        SpeakerFrontRight   = SpeakerFront | SpeakerRight,
        SpeakerRearLeft     = SpeakerRear | SpeakerLeft,
        SpeakerRearRight    = SpeakerRear | SpeakerRight,
        SpeakerCenter       = SpeakerCenLfe | SpeakerLeft,
        SpeakerLfe          = SpeakerCenLfe | SpeakerRight,
        SpeakerRear2Left    = SpeakerRear2 | SpeakerLeft,
        SpeakerRear2Right   = SpeakerRear2 | SpeakerRight,
        AsyncFile           = 0x40000000,
        Unicode             = 0x80000000,
    }
}
