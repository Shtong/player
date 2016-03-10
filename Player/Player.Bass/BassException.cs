using Player.Bass.Bindings;
using System;
using System.Runtime.Serialization;

namespace Player.Bass
{
    /// <summary>
    /// Exception thrown when an error is encountered within the BASS library.
    /// </summary>
    [Serializable]
    public class BassException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BassException"/> class,
        /// with the last encountered BASS error code.
        /// </summary>
        public BassException() : this(null, GetLastError()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BassException"/> class,
        /// with the last encountered BASS error code.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BassException(string message) : this(message, GetLastError()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BassException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="code">A BASS error code.</param>
        public BassException(string message, ErrorCode code)
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BassException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected BassException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            Code = (ErrorCode)info.GetInt32("Code");
        }

        /// <summary>
        /// Gets the BASS error code for this exception.
        /// </summary>
        public ErrorCode Code { get; private set; }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("Code", (int)Code);
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Gets the last encountered BASS error code for the current thread.
        /// </summary>
        /// <returns>An error code.</returns>
        public static ErrorCode GetLastError()
        {
            return (ErrorCode)NativeMethods.BASS_ErrorGetCode();
        }

        internal static float CheckFloatResult(float result)
        {
            if (result == -1)
                throw new BassException();
            return result;
        }

        internal static void CheckBoolResult(bool result)
        {
            if (!result)
                throw new BassException();
        }

        internal static IntPtr CheckHandleResult(IntPtr result)
        {
            if (result == IntPtr.Zero)
                throw new BassException();
            return result;
        }
    }
}
