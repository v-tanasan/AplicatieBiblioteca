using System.Text;
using System.Windows;
using System.Windows.Controls;
using LibrarieModele;
using NivelStocareDate;

namespace NivelUIWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IStocareData adminBiblioteca = StocareFactory.GetAdministratorStocare();
            List<Carte> carti = adminBiblioteca.GetCarti();
            //lblNrStudenti.Content = $"Numar studenti: {studenti.Count}";
            //lblStudenti.Content = "Studenti:\n" + string.Join("\n", studenti.Select(s => $"{s.IdStudent}: {s.Nume} {s.Prenume}"));
            dataGridCarti.ItemsSource = carti;
        }
    }
}