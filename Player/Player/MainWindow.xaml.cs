using Player.Bass;
using System.Windows;
using System.Windows.Interop;

namespace Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PluginManager _plugins;
        private MediaPlayer _player;

        public MainWindow()
        {
            InitializeComponent();
            WindowInteropHelper helper = new WindowInteropHelper(this);
            _player = new MediaPlayer(helper.Handle);
            _plugins = new PluginManager();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the FLAC plugin
            _plugins.Register("bassflac");

            // Start playing something
            _player.Load(@"D:\Soulseek\Aphrodite\(2002) Aftershock\09 Aphrodite - Ganja Man  [f. Deadly Hunta].flac");
            _player.Volume = 1;
            _player.Play();
        }
    }
}
