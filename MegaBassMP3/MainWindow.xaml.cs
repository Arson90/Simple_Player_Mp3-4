using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MegaBassMP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Pause();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Stop();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog otworz = new OpenFileDialog(); //tworzenie obiektu
            otworz.AddExtension = true; //dodaje rozszerzenie pliku
            otworz.DefaultExt = "*.*"; //ustawia domyślne rozszerzenie pliku
            otworz.Filter = "Media Files (*.avi *.mp3 *.wav)|*.avi;*.mp3;*.wav"; //ustawia rozszerzenie do wyboru w okienku
            otworz.ShowDialog(); //otwiera okno dialogowe z katalogami

            try { mediaElement1.Source = new Uri(otworz.FileName); } //pobiera element(w tym przypadku plik mp3)
            catch { new NullReferenceException("Błąd"); } //wyjątek podczas zgłaszania błędu
           
            label4.Content = otworz.FileName; //wyswietla nazwe pliku w etykiecie
            
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer(); //nasz czasomierz
            dispatcherTimer.Tick += new EventHandler(timer_Tick); //występuje, gry upłynie interwał czasomierz(zdarzenie między dwoma punktami)
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1); //ustawienie na 1 sekunde
            dispatcherTimer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            slider.Value = mediaElement1.Position.TotalSeconds;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan timeS = TimeSpan.FromSeconds(e.NewValue);
            mediaElement1.Position = timeS;
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Volume = slider1.Value; 
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaElement1.NaturalDuration.HasTimeSpan)
            {
                  TimeSpan timeS = TimeSpan.FromMilliseconds(mediaElement1.NaturalDuration.TimeSpan.TotalMilliseconds);
                  slider.Maximum = timeS.TotalSeconds;
            }
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Balance = slider2.Value;
        }

    }
}
