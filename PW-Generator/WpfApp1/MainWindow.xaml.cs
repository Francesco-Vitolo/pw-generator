using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
        string password = "";
        List<int> illegalChars = new List<int>() { 96, 58, 59, 39, 44 }; // ASCII - Tabelle
        DateTime date = DateTime.Now;

        const int maxlength = 100;
        const int minlength = 12;

        public MainWindow()
        {
            InitializeComponent();
            Datum.Text = date.ToString();
        }

        private void Button_Click_Generate(object sender, RoutedEventArgs e)
        {
            Alert.Visibility = Visibility.Hidden;
            int length = 0;
                        
            password = ""; //reset
            if (GeneratePrüfen(ref length))
            {
                for (int i = 0; i < length; i++)
                {
                    int zahl = r.Next(33, 126); //Unicode Zeichen ! bis ~ (ohne illegalChars)

                    if (illegalChars.Contains(zahl))
                    {
                        i--;
                    }
                    else //Umwandeln von ASCII in Buchstabe
                    {
                        char character = (char)zahl;
                        string text = character.ToString();
                        password += text;
                    }

                }
            }
            PasswortFeld.Text = password;
        }
        private bool GeneratePrüfen(ref int length)
        {
            if (!int.TryParse(LängeEingeben.Text, out length)) //Prüfen ob Eingabe --> int
            {
                AlertNachricht("Bitte Zahl eingeben");
                return false;
            }
            else if (length > maxlength)
            {
                AlertNachricht($"Nur {maxlength} Zeichen erlaubt");
                return false;
            }
            else if(length < minlength)
            {
                AlertNachricht($"Minimum {minlength} Zeichen");
                return false;
            }
            return true;
        }

        private void Button_Click_Copy(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(password);
            AlertNachricht("Passwort wurde kopiert");
        }

        private void AlertNachricht(string s)
        {
            Alert.Visibility = Visibility.Visible;
            Alert.Text = s;
        }

    
        private void Button_Click_Hide(object sender, RoutedEventArgs e)
        {
            if (PasswortFeld.Text.Contains("\u2022")) // •
            {
                PasswortFeld.Text = password;
            }
            else
            {
                string hidden = "";
                for (int i = 0; i < password.Length; i++)
                {
                    hidden += "\u2022";
                }
                PasswortFeld.Text = hidden;
            }
        }

        private void Button_Click_Youtube(object sender, RoutedEventArgs e)
        {
            Process.Start("chrome.exe", "https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }

        private void Button_Click_DarkWhite(object sender, RoutedEventArgs e)
        {
            if (Background != Brushes.White)
                Background = Brushes.White;
            else
                Background = new SolidColorBrush(Color.FromRgb(72,76,84)); //#484C54
        }

    }
}
