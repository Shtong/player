using Player.Bass.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Bass
{
    /// <summary>
    /// Manages the BASS plugins.
    /// </summary>
    public class PluginManager : IDisposable
    {
        private Dictionary<string, IntPtr> _registeredPlugins = new Dictionary<string, IntPtr>();

        /// <summary>
        /// Creates a new instance of <see cref="PluginManager"/>
        /// </summary>
        /// <param name="baseDirectory">The directory where the plugin binaries can be found. Defaults to the current working directory.</param>
        public PluginManager(string baseDirectory = null)
        {
            if (String.IsNullOrWhiteSpace(baseDirectory))
                BaseDirectory = Environment.CurrentDirectory;
            else
                BaseDirectory = baseDirectory;

        }

        /// <summary>
        /// Registers the specified plugin.
        /// </summary>
        /// <param name="pluginName">The plugin name (do not provide any extension)</param>
        public void Register(string pluginName)
        {
            string fullPath = Path.Combine(BaseDirectory, pluginName + ".dll");
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Could not find the plugin file.", fullPath);
            IntPtr pluginHandle = BassException.CheckHandleResult(NativeMethods.BASS_PluginLoad(fullPath, BassFlags.Unicode));
            _registeredPlugins.Add(fullPath, pluginHandle);
        }

        /// <summary>
        /// Gets the directory where this instance looks for plugins
        /// </summary>
        public string BaseDirectory { get; private set; }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Disposes the current instance and unloads all associated plugins
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach(var pair in _registeredPlugins)
                    {
                        if (!NativeMethods.BASS_PluginFree(pair.Value))
                            // No need for an exception if unloading fails, although we'll notify the developper
                            Debug.Fail($"Could not free plugin \"{pair.Key}\" with handle {pair.Value}");

                    }
                }
                
                _registeredPlugins = null;
                disposedValue = true;
            }
        }
        
        /// <summary>
        /// Disposes the current instance and unloads all associated plugins
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion


    }
}
