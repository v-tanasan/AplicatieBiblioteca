namespace LibrarieModele
{
    public class Cititor
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const int ID = 0;
        private const int NUME = 1;
        private const int CNP = 2;
        private const int EMAIL = 3;
        private const int DATAINREGISTRARE = 4;

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Cnp { get; set; }
        public string Email { get; set; }
        public DateTime DataInregistrare { get; set; }

        // Formatare pentru UI
        public string DataInregistrareFormatata
        {
            get
            {
                return DataInregistrare.ToString("dd.MM.yyyy");
            }
        }

        public Cititor()
        {
            Id = 0;
            Nume = string.Empty;
            Cnp = string.Empty;
            Email = string.Empty;
            DataInregistrare = DateTime.MinValue;
        }

        public Cititor(int id, string nume, string cnp, string email, DateTime data_inregistrare)
        {
            Id = id;
            Nume = nume;
            Cnp = cnp;
            Email = email;
            DataInregistrare = data_inregistrare;
        }

        //constructor cu un singur parametru de tip string care reprezinta o linie dintr-un fisier text
        public Cititor(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier
            //prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            this.Id = Convert.ToInt32(dateFisier[ID]);
            this.Nume = dateFisier[NUME];
            this.Cnp = dateFisier[CNP];
            this.Email = dateFisier[EMAIL];
            this.DataInregistrare = DateTime.Parse(dateFisier[DATAINREGISTRARE]);

        }

        public string ConversieLaSirPentruFisier()
        {
            string obiectCititorPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                SEPARATOR_PRINCIPAL_FISIER,
                Id.ToString(),
                (Nume ?? " NECUNOSCUT "),
                (Cnp ?? " NECUNOSCUT "),
                (Email ?? " NECUNOSCUT "),
                (DataInregistrare.ToString("o"))
                );

            return obiectCititorPentruFisier;
        }
    }
}
