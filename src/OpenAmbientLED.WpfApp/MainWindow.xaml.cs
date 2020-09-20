using Microsoft.Win32;
using OpenAmbientLED.Controllers;
using OpenAmbientLED.Enums;
using OpenAmbientLED.Interfaces;
using OpenAmbientLED.WpfApp.Enums;
using OpenAmbientLED.WpfApp.Extensions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace OpenAmbientLED.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly IRgbLedController rgbLed;
        private readonly ILedController audioLed;

        public bool IsRgbLedAvailable { get; }
        public bool IsAudioLedAvailable { get; }
        public bool StartMinimized
        {
            get => App.Configuration.StartMinimized;
            set => App.Configuration.StartMinimized = value;
        }
        public bool ShowColorTool { get => IsColorToolSupported(); }

        public AudioLedMode SelectedAudioLedMode
        {
            get => App.Configuration.AudioLedMode;
            set
            {
                App.Configuration.AudioLedMode = value;
                audioLed.SetMode((LedMode)value);
            }
        }

        public RgbLedMode SelectedRgbLedMode
        {
            get => App.Configuration.RgbLedMode;
            set
            {
                App.Configuration.RgbLedMode = value;
                rgbLed.SetMode((LedMode)value);

                if (IsColorToolSupported())
                    SetColor(App.Configuration.Color);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowColorTool)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            WindowState = WindowState.Minimized;
        }
        protected override void OnActivated(EventArgs e)
            => TaskbarIcon.Visibility = Visibility.Collapsed;

        protected override void OnDeactivated(EventArgs e)
            => TaskbarIcon.Visibility = Visibility.Visible;

        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    LoadConfiguration();
                    break;
            }
        }

        public MainWindow()
        {
            rgbLed = RgbLedController.Create();
            IsRgbLedAvailable = rgbLed != null;

            audioLed = AudioLedController.Create();
            IsAudioLedAvailable = audioLed != null;

            if (!IsAudioLedAvailable && !IsRgbLedAvailable)
            {
                MessageBox.Show("Not supported!");
                Application.Current.Shutdown();
                return;
            }

            SystemEvents.PowerModeChanged += OnPowerModeChanged;

            DataContext = this;
            InitializeComponent();

            LoadConfiguration();
        }

        private bool IsColorToolSupported()
        {
            if (!IsRgbLedAvailable)
                return false;

            switch (SelectedRgbLedMode)
            {
                case RgbLedMode.Off:
                case RgbLedMode.ColorCycle:
                case RgbLedMode.Random:
                    return false;
            }

            return true;
        }

        private void LoadConfiguration()
        {
            if (IsAudioLedAvailable)
                SelectedAudioLedMode = App.Configuration.AudioLedMode;

            if (IsRgbLedAvailable)
            {
                SelectedRgbLedMode = App.Configuration.RgbLedMode;
                SetColor(App.Configuration.Color);
            }

            if (StartMinimized)
                Hide();
        }

        private void SetColor(Color color)
            => rgbLed.SetColor(color.GetHex());

        private void Open(object sender, RoutedEventArgs e)
            => Show();

        private void Hide(object sender, RoutedEventArgs e)
            => Hide();

        private void Shutdown(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
