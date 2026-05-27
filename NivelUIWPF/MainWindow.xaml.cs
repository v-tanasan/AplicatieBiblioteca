using System.Text;
using System.Windows;
using System.Windows.Controls;
using LibrarieModele;
using NivelStocareDate;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NivelUIWPF
{

    public partial class MainWindow : Window
    {
        List<Carte> carti = new List<Carte>();
        List<Carte> cartiDisponibile = new List<Carte>();
        List<Carte> cartiIndisponibile = new List<Carte>();
        IStocareData adminBiblioteca;

        List<Cititor> cititori = new List<Cititor>();
        IStocareCititori adminCititori;

        List<Imprumut> imprumuturi = new List<Imprumut>();
        IStocareImprumuturi adminImprumuturi;


        public MainWindow()
        {
            InitializeComponent();
            adminBiblioteca = StocareFactory.GetAdministratorStocare();
            carti = adminBiblioteca.GetCarti();
            cartiDisponibile = adminBiblioteca.GetCarti().Where(c => c.Status == "disponibila").ToList();
            cartiIndisponibile = adminBiblioteca.GetCarti().Where(c => c.Status == "indisponibila").ToList();

            adminCititori = StocareFactory.GetAdministratorStocareCititori();
            cititori = adminCititori.GetCititori();

            adminImprumuturi = StocareFactory.GetAdministratorStocareImprumuturi();
            imprumuturi = adminImprumuturi.GetImprumuturi();

            refreshCartiAllTabs();
            //dataGridCarti.ItemsSource = carti;
            //dataGridCautare.ItemsSource = carti;
            cmbCarte.ItemsSource = carti;
            cmbCarte.ItemsSource = cartiDisponibile;
            cmbCarteReturnare.ItemsSource = cartiIndisponibile;
            cmbCititorReturnare.ItemsSource = cititori;
            dgCititori.ItemsSource = cititori;
            cmbCititor.ItemsSource = cititori;
            //dataGridImprumuturi.ItemsSource = imprumuturi;
        }

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            string mod_cautare = ((ComboBoxItem)cmbSearchType.SelectedItem).Content.ToString();
            string sirCautat = txtSearch.Text.Trim();
            List<Carte> rezultatCautare = new List<Carte>();

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                if (mod_cautare == "Carte")
                {
                    txtEroareCautare.Text = "Introduceti titlul cautat !";
                    return;
                }

                if (mod_cautare == "Autor")
                {
                    txtEroareCautare.Text = "Introduceti autorul cautat !";
                    return;
                }
            }

            if (mod_cautare == "Carte")
            {
                rezultatCautare = carti.Where(c => c.Titlu.Contains(sirCautat, StringComparison.OrdinalIgnoreCase)).ToList();
            }else if (mod_cautare == "Autor")
            {
                rezultatCautare = carti.Where(c => c.Autor.Contains(sirCautat, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            dataGridCautare.ItemsSource = null;
            dataGridCautare.ItemsSource = rezultatCautare;
            txtEroareCautare.Text = string.Empty;
        }

        private void resetare()
        {
            txtTitlu.Text = string.Empty;
            txtAutor.Text = string.Empty;
            dataGridCarti.ItemsSource = null;
            dataGridCarti.ItemsSource = carti;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            txtEroareCautare.Text = string.Empty;
            cmbSearchType.SelectedIndex = 0;
            dataGridCautare.ItemsSource = null;
            dataGridCautare.ItemsSource = carti;
        }

        private void btnAdaugaCarte_Click(object sender, RoutedEventArgs e)
        {
            string titlu = txtTitlu.Text.Trim();
            string autor = txtAutor.Text.Trim();

            if (string.IsNullOrEmpty(titlu))
            {
                txtEroare.Text = "Introduceti titlul cartii !";
                return;
            }

            if (string.IsNullOrEmpty(autor))
            {
                txtEroare.Text = "Introduceti autorul cartii !";
                return;
            }

            bool exista = carti.Any(c => c.Titlu.Equals(titlu, StringComparison.OrdinalIgnoreCase) && c.Autor.Equals(autor, StringComparison.OrdinalIgnoreCase));
            if (exista)
            {
                txtEroare.Text = "Exista deja titlul acesta, pentru acest autor !";
                return;
            }

            Carte carteNoua = new Carte(0, titlu, autor, "disponibila");
            adminBiblioteca.AddCarte(carteNoua);

            resetare();
            refreshCombo_Imprumuturi();
            refreshCartiAllTabs();

            txtEroare.Text = "Carte adaugata cu succes !";
        }

        private void btnAfiseaza_Click(object sender, RoutedEventArgs e)
        {
            resetare();
            txtEroare.Text = string.Empty;
            carti = adminBiblioteca.GetCarti();
            dataGridCarti.ItemsSource = null;
            dataGridCarti.ItemsSource = carti;
        }

        private void btnUltima_Click(object sender, RoutedEventArgs e)
        {
            resetare();
            txtEroare.Text = string.Empty;

            Carte ultimaCarte = carti.LastOrDefault();

            if (ultimaCarte != null)
            {
                dataGridCarti.ItemsSource = null;
                dataGridCarti.ItemsSource = new List<Carte> { ultimaCarte };
            }
        }

        private void btnInregistrare_Click(object sender, RoutedEventArgs e)
        {
            string numeCititor = txtNumeCititor.Text.Trim();
            string cnpCititor = txtCnpCititor.Text.Trim();
            string emailCititor = txtEmailCititor.Text.Trim();
            DateTime data_inreg = pkrInregistrare.SelectedDate ?? DateTime.Now;

            if (string.IsNullOrEmpty(numeCititor))
            {
                txtEroareInregistrare.Text = "Introduceti numele cititorului !";
                return;
            }

            if (string.IsNullOrEmpty(cnpCititor))
            {
                txtEroareInregistrare.Text = "Introduceti CNP-ul cititorului !";
                return;
            }

            // validez E-mailul
            if (!emailValid(emailCititor))
            {
                txtEroareInregistrare.Text = "Email invalid!";
                return;
            }

            // validez CNP-ul
            if (!cnpValid(cnpCititor))
            {
                txtEroareInregistrare.Text = "CNP-ul trebuie sa contina exact 13 cifre !";
                return;
            }

            if (string.IsNullOrEmpty(emailCititor))
            {
                txtEroareInregistrare.Text = "Introduceti e-mailul cititorului !";
                return;
            }

            Cititor cititorNou = new Cititor(0, numeCititor, cnpCititor, emailCititor, data_inreg);
            adminCititori.AddCititor(cititorNou);

            txtNumeCititor.Text = string.Empty;
            txtCnpCititor.Text = string.Empty;
            txtEmailCititor.Text = string.Empty;
            cititori = adminCititori.GetCititori();
            dgCititori.ItemsSource = null;
            dgCititori.ItemsSource = cititori;
            cmbCititor.ItemsSource = cititori;
            pkrInregistrare.SelectedDate = null;
            txtEroareInregistrare.Text = "Cititor salvat cu succes !";
        }

        private void btnImprumuta_Click(object sender, RoutedEventArgs e)
        {
            Carte carteSelectata = (Carte)cmbCarte.SelectedItem;
            Cititor cititorSelectat = (Cititor)cmbCititor.SelectedItem;

            if (carteSelectata == null)
            {
                txtEroareInregistrare.Text = "Selectati cartea pentru imprumut !";
                return;
            }

            if (cititorSelectat == null)
            {
                txtEroareInregistrare.Text = "Selectati cititorul caruia imprumutati cartea !";
                return;
            }

            carteSelectata.Status = "indisponibila";
            adminBiblioteca.UpdateCarte(carteSelectata);

            Imprumut imprumutNou = new Imprumut(0, carteSelectata.Id, cititorSelectat.Id, DateTime.Now, null);
            adminImprumuturi.AddImprumut(imprumutNou);

            resetImprumuturi();
            resetReturnari();
            refreshCartiAllTabs();
            refreshCombo_Imprumuturi();
            refreshCombo_Returnari();

            txtEroareInregistrare.Text = "Cartea a fost imprumutata !";
        }

        private void btnReturneaza_Click(object sender, RoutedEventArgs e)
        {
            Carte carteReturnata = (Carte)cmbCarteReturnare.SelectedItem;
            Cititor cititorReturnare = (Cititor)cmbCititorReturnare.SelectedItem;

            if (carteReturnata == null)
            {
                txtEroareInregistrare.Text = "Selectati cartea pentru restituit !";
                return;
            }

            
            if (cititorReturnare == null)
            {
                txtEroareInregistrare.Text = "Selectati cititorul care restituie cartea !";
                return;
            }
            
            
            // caut imprumutul activ pentru cartea selectata
            Imprumut imprumutActiv = imprumuturi.FirstOrDefault(i => i.IdCarte == carteReturnata.Id && i.DataReturnare == null);

            if (imprumutActiv == null)
            {
                txtEroareInregistrare.Text = "Nu exista un imprumut activ pentru aceasta carte !";
                return;
            }

            // actualizare imprumut cu data returnarii
            imprumutActiv.DataReturnare = DateTime.Now;
            adminImprumuturi.UpdateImprumut(imprumutActiv);

            carteReturnata.Status = "disponibila";
            adminBiblioteca.UpdateCarte(carteReturnata);

            resetImprumuturi();
            resetReturnari();
            refreshCartiAllTabs();
            refreshCombo_Imprumuturi();
            refreshCombo_Returnari();

            txtEroareInregistrare.Text = "Cartea a fost returnata !";
        }

        private void resetImprumuturi()
        {
            cmbCarte.SelectedIndex = -1;
            cmbCititor.SelectedIndex = -1;
        }

        private void resetReturnari()
        {
            cmbCarteReturnare.SelectedIndex = -1;
            cmbCititorReturnare.SelectedIndex = -1;
        }

        private void refreshCartiAllTabs()
        {
            carti = adminBiblioteca.GetCarti();
            imprumuturi = adminImprumuturi.GetImprumuturi();

            // COMPLETARE NUME SI TITLU PENTRU AFISARE
            foreach (Imprumut imprumut in imprumuturi)
            {
                Carte carte = carti.FirstOrDefault(c => c.Id == imprumut.IdCarte);

                if (carte != null)
                {
                    imprumut.TitluCarteAfisat = carte.Titlu;
                }

                Cititor cititor = cititori.FirstOrDefault(c => c.Id == imprumut.IdCititor);

                if (cititor != null)
                {
                    imprumut.NumeCititorAfisat = cititor.Nume;
                }
            }

            // refresh grid date in tab-ul (principal) Carti
            dataGridCarti.ItemsSource = null;
            dataGridCarti.ItemsSource = carti;

            // refresh grid date in tab-ul Imprumuturi
            dataGridImprumuturi.ItemsSource = null;
            dataGridImprumuturi.ItemsSource = imprumuturi;

            // refresh grid date in tab-ul Cautare
            dataGridCautare.ItemsSource = null;
            dataGridCautare.ItemsSource = carti;
        }

        private void refreshCombo_Imprumuturi()
        {
            cartiDisponibile = adminBiblioteca.GetCarti().Where(c => c.Status == "disponibila").ToList();
            cmbCarte.ItemsSource = cartiDisponibile;
        }

        private void refreshCombo_Returnari()
        {
            cartiIndisponibile = adminBiblioteca.GetCarti().Where(c => c.Status == "indisponibila").ToList();
            cmbCarteReturnare.ItemsSource = cartiIndisponibile;
        }

        private void btnMeniuOperatii_Click(object sender, RoutedEventArgs e)
        {
            tabPrincipal.SelectedIndex = 1;
        }

        private void btnMeniuCarti_Click(object sender, RoutedEventArgs e)
        {
            tabPrincipal.SelectedIndex = 3;
        }

        private void btnMeniuImprumuturi_Click(object sender, RoutedEventArgs e)
        {
            tabPrincipal.SelectedIndex = 2;
        }

        private void cmbCarteReturnare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Carte carteSelectata = (Carte)cmbCarteReturnare.SelectedItem;

            if (carteSelectata == null)
            {
                return;
            }

            // caut imprumutul activ
            Imprumut imprumutActiv = imprumuturi.FirstOrDefault(i => i.IdCarte == carteSelectata.Id && i.DataReturnare == null);

            if (imprumutActiv == null)
            {
                cmbCititorReturnare.SelectedItem = null;
                return;
            }

            // caut cititorul care a imprumutat cartea
            Cititor cititor = cititori.FirstOrDefault(c => c.Id == imprumutActiv.IdCititor);

            // selectam automat cititorul in ComboBox
            cmbCititorReturnare.SelectedIndex = -1;
            cmbCititorReturnare.SelectedItem = cititor;
        }

        // validare E-mail
        private bool emailValid(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // validare CNP
        private bool cnpValid(string cnp)
        {
            return cnp.Length == 13 && cnp.All(char.IsDigit);
        }

        private void dgCititori_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Cititor cititorModificat = e.Row.Item as Cititor;

            if (cititorModificat == null)
                return;

            adminCititori.UpdateCititor(cititorModificat);
        }

        private void btnDeleteCarte_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn == null)
                return;

            Carte carteSelectata = btn.DataContext as Carte;

            if (carteSelectata == null)
                return;

            if (carteSelectata.Status == "indisponibila")
            {
                txtEroare.Text = "Nu poti sterge o carte imprumutata!";
                return;
            }

            adminBiblioteca.RemoveCarte(carteSelectata.Id);

            carti = adminBiblioteca.GetCarti();

            dataGridCarti.ItemsSource = null;
            dataGridCarti.ItemsSource = carti;

            refreshCombo_Imprumuturi();
            refreshCartiAllTabs();

            txtEroare.Text = "Cartea a fost stearsa!";
        }
    }
}