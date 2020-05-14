using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using WPF_MostraArte.Model;

namespace WPF_MostraArte
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List <Opera> Opere;
        int cnt;
        int index;
        
        
        public MainWindow()
        {
            InitializeComponent();
            Opere = new List<Opera>();
            CaricaLista();
            lbl_Testo.Visibility = Visibility.Hidden;
            btn_Visualizza.Visibility = Visibility.Hidden;
            lst_Informazioni.Visibility = Visibility.Hidden;

        }

        private void CaricaLista()
        {
            string path = @"opere d'arte.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlopere = xmlDoc.Element("opere");
            var xmlopera = xmlopere.Elements("opera");


            foreach(var item in xmlopera)
            {
                XElement xmlNome = item.Element("nome");
                XElement xmlArtista = item.Element("artista");
                XElement xmlLuogo = item.Element("luogo");
                XElement xmlData = item.Element("data");
                XElement xmlGrandezza = item.Element("grandezza");
                XElement xmlTecnica = item.Element("tecnica");

                Opera opera = new Opera();

                opera.nome = xmlNome.Value;
                opera.artista = xmlArtista.Value;
                opera.luogo = xmlLuogo.Value;
                opera.data = xmlData.Value;
                opera.grandezza = xmlGrandezza.Value;
                opera.tecnica = xmlTecnica.Value;

                Opere.Add(opera);
            }

        }

        private void btn_Cerca_Click(object sender, RoutedEventArgs e)
        {
            
            lst_Risultato.Items.Clear();

            if (txt_Nome.Text == "")
            {
                MessageBox.Show("Inserisci il nome dell'opera");
            }
            else
            {
                cnt = 0;
                foreach (var item in Opere)
                {
                    index = 0;
                    index = item.nome.ToLower().IndexOf(txt_Nome.Text.ToLower());
                    if (index >= 0)
                    {
                        lst_Risultato.Items.Add(item.nome);
                        cnt++;
                    }
                }

                if (cnt == 0)
                {
                    lbl_Risultato.Content = $"Nessun risultato";
                    lbl_Testo.Visibility = Visibility.Hidden;
                    btn_Visualizza.Visibility = Visibility.Hidden;
                }
                else if (cnt == 1)
                {
                    lbl_Risultato.Content = $"1 risultato";
                    lbl_Testo.Visibility = Visibility.Visible;
                    btn_Visualizza.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl_Risultato.Content = $"{cnt} risultati";
                    lbl_Testo.Visibility = Visibility.Visible;
                    btn_Visualizza.Visibility = Visibility.Visible;
                }
            }

        }

        private void txt_Nome_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Visualizza_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var imgBrush = new ImageBrush();

                imgBrush.ImageSource = new BitmapImage(new Uri(@$"{lst_Risultato.SelectedItem.ToString().Replace("'","")}.jpg", UriKind.Relative));
                grd_Opera.Background = imgBrush;

                lst_Informazioni.Items.Clear();
                lst_Informazioni.Visibility = Visibility.Visible;
                foreach (var item in Opere)

                {
                    if (lst_Risultato.SelectedItem.ToString() == item.nome)
                    {
                        lst_Informazioni.Items.Add($"Nome: {item.nome}");
                        lst_Informazioni.Items.Add($"Artista: {item.artista}");
                        lst_Informazioni.Items.Add($"Luogo: {item.luogo}");
                        lst_Informazioni.Items.Add($"Data: {item.data}");
                        lst_Informazioni.Items.Add($"Grandezza: {item.grandezza}");
                        lst_Informazioni.Items.Add($"Tecnica: {item.tecnica}");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Seleziona l'opera");


            }
            
                
            
        }

        private void btn_Carica_Click(object sender, RoutedEventArgs e)
        {
            lst_Risultato.Items.Clear();
            txt_Nome.Text = " ";
            lbl_Risultato.Visibility = Visibility.Visible;
            lbl_Testo.Visibility = Visibility.Visible;
            btn_Visualizza.Visibility = Visibility.Visible;
            cnt = 0;
            foreach(var item in Opere)
            {
                lst_Risultato.Items.Add(item.nome);
                cnt++;
            }
            lbl_Risultato.Content = $"{cnt} risultati";
        }
    }
}
