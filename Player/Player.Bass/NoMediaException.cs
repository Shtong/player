using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Bass
{
    /// <summary>
    /// Error thrown when you try to use a <see cref="MediaPlayer"/> while it does not have a media loaded.
    /// </summary>
    [Serializable]
    public class NoMediaException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="NoMediaException"/>.
        /// </summary>
        public NoMediaException()
            : base("No media is loaded in that player.")
        { }


        /// <summary>
        /// Initializes a new instance of the <see cref="NoMediaException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected NoMediaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
