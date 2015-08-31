using System;
using System.Collections.Generic;
using System.Reflection;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Media;

namespace mlidynamics.iot.sr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> ColorsList;
        SpeechRecognizer speechRecognizer;
        public MainWindow()
        {
            InitializeComponent();
            ColorsList = new List<string>();
            speechRecognizer = new SpeechRecognizer();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Choices colors = new Choices();
            colors.Add(new string[] { "red", "green", "blue" });

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            // Create the Grammar instance.
            Grammar g = new Grammar(gb);

            
            speechRecognizer.LoadGrammar(g);
            speechRecognizer.Enabled = true;
            speechRecognizer.SpeechRecognized += speechRecognizer_SpeechRecognized;
        }

        void speechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (ColorsList.Contains(e.Result.Text))
            {
                var color = (Color)ColorConverter.ConvertFromString(e.Result.Text);
                ColorCanvas.Background = new SolidColorBrush(color);
                ColorslistBox.Items.Insert(0, e.Result.Text);
            }
        }
    }
}
