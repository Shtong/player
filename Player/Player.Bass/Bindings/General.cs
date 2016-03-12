using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Player.Bass.Bindings
{
    /// <summary>
    /// BASS bindings: Initialization, info, etc...
    /// </summary>
    internal static partial class NativeMethods
    {
        internal const string BASS_DLL_NAME = "bass.dll";

        /// <summary>
        /// Retrieves the error code for the most recent BASS function call in the current thread.
        /// </summary>
        /// <returns>If no error occurred during the last BASS function call then BASS_OK is returned, else one of the BASS_ERROR values is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern int BASS_ErrorGetCode();

        /// <summary>
        /// Frees all resources used by the output device, including all its samples, streams and MOD musics.
        /// </summary>
        /// <returns>If successful, then <c>true</c> is returned, else <c>false</c> is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_Free();

        /// <summary>
        /// Retrieves the current master volume level.
        /// </summary>
        /// <returns>If successful, the volume level is returned, else -1 is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern float BASS_GetVolume();

        /// <summary>
        /// Initializes an output device.
        /// </summary>
        /// <param name="device">The device to use... -1 = default device, 0 = no sound, 1 = first real output device.</param>
        /// <param name="freq">Output sample rate.</param>
        /// <param name="flags">A combination of optionnal flags</param>
        /// <param name="hWnd">The application's main window... 0 = the desktop window (use this for console applications).</param>
        /// <param name="clsid">Class identifier of the object to create, that will be used to initialize DirectSound... NULL = use default.</param>
        /// <returns>If the device was successfully initialized, <c>true</c> is returned, else <c>false</c> is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_Init(int device, int freq, BassDeviceFlags flags, IntPtr hWnd, IntPtr clsid = default(IntPtr));

        // <summary>
        // Stops the output, pausing all musics/samples/streams on it.
        // </summary>
        // <returns>If successful, then <c>true</c> is returned, else <c>false</c> is returned.</returns>
        //[DllImport(BASS_DLL_NAME)]
        //internal static extern bool BASS_Pause();

        /// <summary>
        /// Sets the output master volume.
        /// </summary>
        /// <param name="volume">The volume level... 0 (silent) to 1 (max).</param>
        /// <returns>If successful, then <c>true</c> is returned, else <c>false</c> is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_SetVolume(float volume);

        // <summary>
        // Starts (or resumes) the output.
        // </summary>
        // <returns>If successful, then <c>true</c> is returned, else <c>false</c> is returned.</returns>
        //[DllImport(BASS_DLL_NAME)]
        //internal static extern bool BASS_Start();

        // <summary>
        // Stops the output, stopping all musics/samples/streams on it.
        // </summary>
        // <returns>If successful, then <c>true</c> is returned, else <c>false</c> is returned.</returns>
        //[DllImport(BASS_DLL_NAME)]
        //internal static extern bool BASS_Stop();
    }

    [Flags]
    internal enum BassDeviceFlags
    {
        Default = 0,
        /// <summary>
        /// Use 8-bit resolution, else 16-bit
        /// </summary>
        Use8Bits = 1,
        /// <summary>
        /// Use mono, else stereo.
        /// </summary>
        Mono = 2,
        /// <summary>
        /// Enable 3D functionality
        /// </summary>
        Enable3D = 4,
        /// <summary>
        /// Calculates the latency of the device, that is the delay between requesting a sound to play and it actually being heard. 
        /// A recommended minimum buffer length is also calculated. Both values are retrievable in the BASS_INFO structure (latency and minbuf members).
        /// These calculations can increase the time taken by this function by 1-3 seconds. 
        /// </summary>
        Latency = 256,
        /// <summary>
        /// Use the Windows control panel setting to detect the number of speakers. 
        /// Soundcards generally have their own control panel to set the speaker config, so the Windows control panel setting may not be accurate unless it matches that. 
        /// This flag has no effect on Vista, as the speakers are already accurately detected.
        /// </summary>
        CpSpeakers = 1024,
        /// <summary>
        /// Force the enabling of speaker assignment. With some devices/drivers, the number of speakers BASS detects may be 2, when the device in fact supports more than 2 speakers. 
        /// This flag forces the enabling of assignment to 8 possible speakers. This flag has no effect with non-WDM drivers.
        /// </summary>
        Speakers = 2048,
        /// <summary>
        /// Ignore speaker arrangement. This flag tells BASS not to make any special consideration for speaker arrangements when using the SPEAKER flags, 
        /// eg. swapping the CENLFE and REAR speaker channels in 5/7.1 speaker output. This flag should be used with plain multi-channel (rather than 5/7.1) devices.
        /// </summary>
        NoSpeaker = 4096,
        /// <summary>
        /// Set the device's output rate to freq, otherwise leave it as it is.
        /// </summary>
        Freq = 16384,
        /// <summary>
        /// Initialize the device using the ALSA "dmix" plugin, else initialize the device for exclusive access.
        /// </summary>
        DMix = 8192,
    }
}
