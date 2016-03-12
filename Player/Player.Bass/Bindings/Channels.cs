using System;
using System.Runtime.InteropServices;

namespace Player.Bass.Bindings
{
    internal static partial class NativeMethods
    {
        /// <summary>
        /// Starts (or resumes) playback of a sample, stream, MOD music, or recording.
        /// </summary>
        /// <param name="handle">The channel handle... a HCHANNEL, HMUSIC, HSTREAM, or HRECORD.</param>
        /// <param name="restart">
        /// Restart playback from the beginning? 
        /// If handle is a user stream (created with <see cref="BASS_StreamCreateFile(bool, string, long, long, BassFlags)"/>), its current buffer contents are cleared.
        /// If it is a MOD music, its BPM/etc are reset to their initial values.
        /// </param>
        /// <returns>If successful, <c>true</c> is returned, else <c>false</c> is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_ChannelPlay(SafeStreamHandle handle, bool restart);

        /// <summary>
        /// Pauses a sample, stream, MOD music, or recording.
        /// </summary>
        /// <param name="handle">The channel handle... a HCHANNEL, HMUSIC, HSTREAM, or HRECORD.</param>
        /// <returns>If successful, <c>true</c> is returned, else <c>false</c> is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_ChannelPause(SafeStreamHandle handle);

        /// <summary>
        /// Stops a sample, stream, MOD music, or recording.
        /// </summary>
        /// <param name="handle">The channel handle... a HCHANNEL, HMUSIC, HSTREAM, or HRECORD.</param>
        /// <returns>If successful, <c>true</c> is returned, else <c>false</c> is returned.</returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern bool BASS_ChannelStop(SafeStreamHandle handle);

        /// <summary>
        /// Checks if a sample, stream, or MOD music is active (playing) or stalled. Can also check if a recording is in progress.
        /// </summary>
        /// <param name="handle">The channel handle... a HCHANNEL, HMUSIC, HSTREAM, or HRECORD.</param>
        /// <returns></returns>
        [DllImport(BASS_DLL_NAME)]
        internal static extern BassActive BASS_ChannelIsActive(SafeStreamHandle handle);
    }

    internal enum BassActive
    {
        /// <summary>
        /// The channel is not active, or handle is not a valid channel.
        /// </summary>
        Stopped,
        /// <summary>
        /// The channel is playing (or recording).
        /// </summary>
        Playing,
        /// <summary>
        /// Playback of the stream has been stalled due to a lack of sample data. The playback will automatically resume once there is sufficient data to do so.
        /// </summary>
        Stalled,
        /// <summary>
        /// The channel is paused.
        /// </summary>
        Paused,
    }
}
