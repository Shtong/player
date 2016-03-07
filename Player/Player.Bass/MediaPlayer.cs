using Player.Bass.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Bass
{
    public sealed class MediaPlayer : IDisposable
    {
        private IntPtr _hWnd;
        private bool _disposed;

        public MediaPlayer()
            : this(IntPtr.Zero)
        {

        }

        public MediaPlayer(IntPtr hWnd)
        {
            _hWnd = hWnd;
            BassException.CheckBoolResult(NativeMethods.BASS_Init(-1, 0, BassDeviceFlags.Default, _hWnd, Guid.Empty));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MediaPlayer"/> class.
        /// </summary>
        ~MediaPlayer()
        {
            Dispose(false);
        }

        public bool HasMedia { get; private set; }

        public void Load(string fileName)
        {
            CheckNotDisposed();



            throw new NotImplementedException();
        }

        public void Play()
        {
            CheckNotDisposed();
            BassException.CheckBoolResult(NativeMethods.BASS_Start());
        }

        public void Pause()
        {
            CheckNotDisposed();
            BassException.CheckBoolResult(NativeMethods.BASS_Pause());
        }

        public void Stop()
        {
            CheckNotDisposed();
            BassException.CheckBoolResult(NativeMethods.BASS_Stop());
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
                return BassException.CheckFloatResult(NativeMethods.BASS_GetVolume());
            }
            set
            {
                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Volume must be a value between 0 and 1");
                BassException.CheckBoolResult(NativeMethods.BASS_SetVolume(value));
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
    }
}
