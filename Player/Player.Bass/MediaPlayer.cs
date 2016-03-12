using Player.Bass.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Bass
{
    /// <summary>
    /// Determines the different possible playing states of a <see cref="MediaPlayer"/>.
    /// </summary>
    /// <seealso cref="MediaPlayer.State"/>
    public enum MediaPlayerState
    {
        // Important note : The enumeration values must match those of BassActive for fast convertion
        /// <summary>
        /// The media player is stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// The media player is playing
        /// </summary>
        Playing,
        /// <summary>
        /// The media player is waiting for more data, and will resume play as soon as possible.
        /// </summary>
        Waiting,
        /// <summary>
        /// The media player is paused
        /// </summary>
        Paused,
    }

    /// <summary>
    /// Plays music.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public sealed class MediaPlayer : IDisposable
    {
        private IntPtr _hWnd;
        private IntPtr _currentStream;
        private bool _disposed;
        private bool _initialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPlayer"/> class.
        /// </summary>
        public MediaPlayer() : this(IntPtr.Zero) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPlayer"/> class.
        /// </summary>
        /// <param name="hWnd">A window handle associated with the music source.</param>
        public MediaPlayer(IntPtr hWnd)
        {
            _hWnd = hWnd;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MediaPlayer"/> class.
        /// </summary>
        ~MediaPlayer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a media file loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a media loaded; otherwise, <c>false</c>.
        /// </value>
        public bool HasMedia
        {
            get { return _currentStream != IntPtr.Zero; }
        }

        /// <summary>
        /// Loads the specified media file.
        /// </summary>
        /// <param name="fileName">Name of the file to load.</param>
        public void Load(string fileName)
        {
            CheckNotDisposed();
            InitializeBass();
            
            if(_currentStream != IntPtr.Zero)
            {
                // Unload previous stream
                BassException.CheckBoolResult(NativeMethods.BASS_StreamFree(_currentStream));
                _currentStream = IntPtr.Zero;
            }

            _currentStream = BassException.CheckHandleResult(NativeMethods.BASS_StreamCreateFile(false, fileName, 0, 0, BassFlags.Unicode));
        }

        /// <summary>
        /// Starts or resumes the currently loaded media.
        /// </summary>
        public void Play()
        {
            CheckNotDisposed();
            CheckMediaLoaded();
            BassException.CheckBoolResult(NativeMethods.BASS_ChannelPlay(_currentStream, false));
        }

        /// <summary>
        /// Pauses the currently loaded media
        /// </summary>
        public void Pause()
        {
            CheckNotDisposed();
            CheckMediaLoaded();
            BassException.CheckBoolResult(NativeMethods.BASS_ChannelPause(_currentStream));
        }

        /// <summary>
        /// Stops playing the currently loaded media
        /// </summary>
        public void Stop()
        {
            CheckNotDisposed();
            CheckMediaLoaded();
            BassException.CheckBoolResult(NativeMethods.BASS_ChannelStop(_currentStream));
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// A value between <c>0</c> (muted) and <c>1</c> (full volume).
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Volume must be a value between 0 and 1</exception>
        public float Volume
        {
            get
            {
                InitializeBass();
                return BassException.CheckFloatResult(NativeMethods.BASS_GetVolume());
            }
            set
            {
                InitializeBass();
                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Volume must be a value between 0 and 1");
                BassException.CheckBoolResult(NativeMethods.BASS_SetVolume(value));
            }
        }

        /// <summary>
        /// Gets the playing state of this instance.
        /// </summary>
        public MediaPlayerState State
        {
            get
            {
                if (_currentStream == IntPtr.Zero)
                    return MediaPlayerState.Stopped;

                return (MediaPlayerState)NativeMethods.BASS_ChannelIsActive(_currentStream);

            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            BassException.CheckBoolResult(NativeMethods.BASS_Free());

            _disposed = true;
        }

        private void CheckNotDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        private void CheckMediaLoaded()
        {
            if (!HasMedia)
                throw new NoMediaException();
        }

        private void InitializeBass()
        {
            if(!_initialized)
            {
                BassException.CheckBoolResult(NativeMethods.BASS_Init(-1, 44100, BassDeviceFlags.Default, _hWnd));
                _initialized = true;
            }
        }
    }
}
