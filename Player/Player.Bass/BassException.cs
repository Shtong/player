using Player.Bass.Bindings;
using System;

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
        protected BassException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }

        /// <summary>
        /// Gets the BASS error code for this exception.
        /// </summary>
        public ErrorCode Code { get; private set; }

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
    }
}
