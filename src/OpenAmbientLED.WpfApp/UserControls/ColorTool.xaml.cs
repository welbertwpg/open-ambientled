using OpenAmbientLED.WpfApp.Models;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenAmbientLED.WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for ColorTool.xaml
    /// </summary>
    public partial class ColorTool : UserControl
    {
        public ColorTool()
        {
            InitializeComponent();
            DataContext = new ColorToolViewModel(OnColorChanged);
        }

        public event ColorChangedEventHandler ColorChanged;

        public void OnColorChanged(Color color)
            => this.ColorChanged.Invoke(color);
    }
}
