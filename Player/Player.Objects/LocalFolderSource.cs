using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Objects
{
    public class LocalFolderSource
    {
        public FileSystemWatcher _watcher;

        public LocalFolderSource(string rootDir)
        {
            if (rootDir == null)
                throw new ArgumentNullException(nameof(rootDir));
            if (!Directory.Exists(rootDir))
                throw new DirectoryNotFoundException($"The directory \"{rootDir}\" does not exist");

            RootDir = rootDir;
            InitializeItems();
            //InitializeWatcher();
        }
        
        private void InitializeItems()
        {
            foreach(string path in Directory.EnumerateFiles(RootDir, "*.mp3|*.flac", SearchOption.AllDirectories))
            {
                PlayerItem item = TryLoadItem(path);
                if (item != null)
                    Items.Add(item);
            }
        }

        /*
        private void InitializeWatcher()
        {
            _watcher = new FileSystemWatcher(RootDir);
            _watcher.Changed += OnEntryChanged;
            _watcher.Created += OnEntryCreated;
            _watcher.Deleted += OnEntryDeleted;
            _watcher.Renamed += OnEntryRenamed;
            _watcher.Error += OnWatcherError;
            _watcher.EnableRaisingEvents = true;
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnEntryRenamed(object sender, RenamedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnEntryDeleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnEntryCreated(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnEntryChanged(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }
        */

        private PlayerItem TryLoadItem(string path)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<PlayerItem> Items { get; } = new ObservableCollection<PlayerItem>();

        public string RootDir { get; private set; }
    }
}
